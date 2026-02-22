# Script to start both backend and frontend applications
param(
    [switch]$Force,
    [switch]$BackendOnly,
    [switch]$FrontendOnly
)

$ErrorActionPreference = "Stop"

$ScriptsPath = Split-Path -Parent $MyInvocation.MyCommand.Definition

Write-Host "=== StoreCenter Application Startup ===" -ForegroundColor Magenta
Write-Host ""

# Function to run script and capture result
function Invoke-AppScript {
    param(
        [string]$ScriptPath,
        [string]$AppName,
        [bool]$ForceRestart = $false
    )
    
    Write-Host "Starting $AppName..." -ForegroundColor Yellow
    
    try {
        if ($ForceRestart) {
            & $ScriptPath -Force
        } else {
            & $ScriptPath
        }
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✅ $AppName started successfully" -ForegroundColor Green
            return $true
        } else {
            Write-Host "❌ Failed to start $AppName" -ForegroundColor Red
            return $false
        }
    }
    catch {
        Write-Host "❌ Error starting $AppName`: $($_.Exception.Message)" -ForegroundColor Red
        return $false
    }
}

$backendSuccess = $true
$frontendSuccess = $true

# Start Backend
if (-not $FrontendOnly) {
    Write-Host "--- Starting Backend Application ---" -ForegroundColor Cyan
    $backendScript = Join-Path $ScriptsPath "start-backend.ps1"
    
    if (Test-Path $backendScript) {
        $backendSuccess = Invoke-AppScript -ScriptPath $backendScript -AppName "Backend API" -ForceRestart $Force
    } else {
        Write-Host "❌ Backend startup script not found: $backendScript" -ForegroundColor Red
        $backendSuccess = $false
    }
    
    Write-Host ""
}

# Start Frontend
if (-not $BackendOnly) {
    Write-Host "--- Starting Frontend Application ---" -ForegroundColor Cyan
    $frontendScript = Join-Path $ScriptsPath "start-frontend.ps1"
    
    if (Test-Path $frontendScript) {
        $frontendSuccess = Invoke-AppScript -ScriptPath $frontendScript -AppName "Frontend" -ForceRestart $Force
    } else {
        Write-Host "❌ Frontend startup script not found: $frontendScript" -ForegroundColor Red
        $frontendSuccess = $false
    }
    
    Write-Host ""
}

# Summary
Write-Host "=== Startup Summary ===" -ForegroundColor Magenta

if (-not $FrontendOnly) {
    if ($backendSuccess) {
        Write-Host "Backend API: ✅ Running" -ForegroundColor Green
        Write-Host "  - HTTP:  http://localhost:8080" -ForegroundColor Cyan
        Write-Host "  - HTTPS: https://localhost:8443" -ForegroundColor Cyan
        Write-Host "  - Swagger: http://localhost:8080/swagger" -ForegroundColor Cyan
    } else {
        Write-Host "Backend API: ❌ Failed to start" -ForegroundColor Red
    }
}

if (-not $BackendOnly) {
    if ($frontendSuccess) {
        Write-Host "Frontend: ✅ Running" -ForegroundColor Green
        Write-Host "  - URL: http://localhost:3000" -ForegroundColor Cyan
    } else {
        Write-Host "Frontend: ❌ Failed to start" -ForegroundColor Red
    }
}

Write-Host ""

if ((-not $BackendOnly -and -not $frontendSuccess) -or (-not $FrontendOnly -and -not $backendSuccess)) {
    Write-Host "⚠️  Some applications failed to start. Check the logs above for details." -ForegroundColor Yellow
    exit 1
} else {
    Write-Host "🎉 All requested applications are running!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Available Parameters:" -ForegroundColor Yellow
    Write-Host "  -Force         : Restart applications if already running" -ForegroundColor Gray
    Write-Host "  -BackendOnly   : Start only the backend application" -ForegroundColor Gray
    Write-Host "  -FrontendOnly  : Start only the frontend application" -ForegroundColor Gray
    exit 0
}