# Script to stop both backend and frontend applications
param(
    [switch]$Force,
    [switch]$BackendOnly,
    [switch]$FrontendOnly
)

$ErrorActionPreference = "Stop"

$ScriptsPath = Split-Path -Parent $MyInvocation.MyCommand.Definition

Write-Host "=== StoreCenter Application Shutdown ===" -ForegroundColor Magenta
Write-Host ""

# Function to run script and capture result
function Invoke-StopScript {
    param(
        [string]$ScriptPath,
        [string]$AppName,
        [bool]$ForceStop = $false
    )
    
    Write-Host "Stopping $AppName..." -ForegroundColor Yellow
    
    try {
        if ($ForceStop) {
            & $ScriptPath -Force
        } else {
            & $ScriptPath
        }
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✅ $AppName stopped successfully" -ForegroundColor Green
            return $true
        } else {
            Write-Host "⚠️ Issues stopping $AppName (may not have been running)" -ForegroundColor Yellow
            return $true  # Consider this success since the goal is to stop it
        }
    }
    catch {
        Write-Host "❌ Error stopping $AppName`: $($_.Exception.Message)" -ForegroundColor Red
        return $false
    }
}

$backendSuccess = $true
$frontendSuccess = $true

# Stop Frontend first (it's usually faster to stop)
if (-not $BackendOnly) {
    Write-Host "--- Stopping Frontend Application ---" -ForegroundColor Cyan
    $frontendScript = Join-Path $ScriptsPath "stop-frontend.ps1"
    
    if (Test-Path $frontendScript) {
        $frontendSuccess = Invoke-StopScript -ScriptPath $frontendScript -AppName "Frontend" -ForceStop $Force
    } else {
        Write-Host "❌ Frontend stop script not found: $frontendScript" -ForegroundColor Red
        $frontendSuccess = $false
    }
    
    Write-Host ""
}

# Stop Backend
if (-not $FrontendOnly) {
    Write-Host "--- Stopping Backend Application ---" -ForegroundColor Cyan
    $backendScript = Join-Path $ScriptsPath "stop-backend.ps1"
    
    if (Test-Path $backendScript) {
        $backendSuccess = Invoke-StopScript -ScriptPath $backendScript -AppName "Backend API" -ForceStop $Force
    } else {
        Write-Host "❌ Backend stop script not found: $backendScript" -ForegroundColor Red
        $backendSuccess = $false
    }
    
    Write-Host ""
}

# Give a moment for all processes to completely terminate
Start-Sleep -Seconds 2

# Summary
Write-Host "=== Shutdown Summary ===" -ForegroundColor Magenta

if (-not $BackendOnly) {
    if ($frontendSuccess) {
        Write-Host "Frontend: ✅ Stopped" -ForegroundColor Green
    } else {
        Write-Host "Frontend: ❌ Failed to stop" -ForegroundColor Red
    }
}

if (-not $FrontendOnly) {
    if ($backendSuccess) {
        Write-Host "Backend API: ✅ Stopped" -ForegroundColor Green
    } else {
        Write-Host "Backend API: ❌ Failed to stop" -ForegroundColor Red
    }
}

Write-Host ""

if ((-not $BackendOnly -and -not $frontendSuccess) -or (-not $FrontendOnly -and -not $backendSuccess)) {
    Write-Host "⚠️  Some applications failed to stop properly. Check the logs above for details." -ForegroundColor Yellow
    Write-Host "You can try running with -Force parameter for more aggressive termination." -ForegroundColor Yellow
    exit 1
} else {
    Write-Host "🛑 All requested applications have been stopped!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Available Parameters:" -ForegroundColor Yellow
    Write-Host "  -Force         : Forcefully terminate applications" -ForegroundColor Gray
    Write-Host "  -BackendOnly   : Stop only the backend application" -ForegroundColor Gray
    Write-Host "  -FrontendOnly  : Stop only the frontend application" -ForegroundColor Gray
    
    # Show port status
    Write-Host ""
    Write-Host "Port Status:" -ForegroundColor Yellow
    
    function Test-Port {
        param([int]$Port)
        try {
            $connection = Get-NetTCPConnection -LocalPort $Port -ErrorAction SilentlyContinue
            return $connection.Count -gt 0
        }
        catch {
            return $false
        }
    }
    
    if (-not $FrontendOnly) {
        Write-Host "  Backend HTTP (8080): $((Test-Port -Port 8080) ? 'In Use' : 'Available')" -ForegroundColor Gray
        Write-Host "  Backend HTTPS (8443): $((Test-Port -Port 8443) ? 'In Use' : 'Available')" -ForegroundColor Gray
    }
    
    if (-not $BackendOnly) {
        Write-Host "  Frontend (3000): $((Test-Port -Port 3000) ? 'In Use' : 'Available')" -ForegroundColor Gray
    }
    
    exit 0
}