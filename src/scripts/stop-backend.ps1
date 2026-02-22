# Script to stop the backend API application if it's running
param(
    [switch]$Force
)

$ErrorActionPreference = "Stop"

# Configuration
$Port = 8080
$HttpsPort = 8443

Write-Host "Stopping backend application..." -ForegroundColor Yellow

# Function to check if a port is in use
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

# Function to find and stop processes using specific ports
function Stop-ProcessByPort {
    param([int]$Port)
    
    try {
        $connections = Get-NetTCPConnection -LocalPort $Port -ErrorAction SilentlyContinue
        foreach ($connection in $connections) {
            $processId = $connection.OwningProcess
            $process = Get-Process -Id $processId -ErrorAction SilentlyContinue
            
            if ($process) {
                Write-Host "Found process using port $Port`: $($process.ProcessName) (PID: $processId)" -ForegroundColor Cyan
                
                if ($Force) {
                    Stop-Process -Id $processId -Force
                    Write-Host "Forcefully stopped process $processId" -ForegroundColor Yellow
                } else {
                    Stop-Process -Id $processId
                    Write-Host "Stopped process $processId" -ForegroundColor Green
                }
                
                return $true
            }
        }
    }
    catch {
        Write-Host "Error stopping process on port $Port`: $($_.Exception.Message)" -ForegroundColor Red
        return $false
    }
    
    return $false
}

# Function to find dotnet processes running our API
function Get-BackendProcesses {
    $backendProcesses = @()
    $processes = Get-Process -Name "dotnet" -ErrorAction SilentlyContinue
    
    foreach ($process in $processes) {
        try {
            $commandLine = (Get-CimInstance Win32_Process -Filter "ProcessId = $($process.Id)").CommandLine
            if ($commandLine -like "*StoreCenter.Api*") {
                $backendProcesses += $process
            }
        }
        catch {
            # Continue checking other processes
        }
    }
    
    return $backendProcesses
}

$stopped = $false

# Method 1: Find and stop StoreCenter.Api processes by command line
Write-Host "Looking for StoreCenter.Api processes..." -ForegroundColor Yellow
$backendProcesses = Get-BackendProcesses

if ($backendProcesses.Count -gt 0) {
    foreach ($process in $backendProcesses) {
        Write-Host "Found StoreCenter.Api process: $($process.ProcessName) (PID: $($process.Id))" -ForegroundColor Cyan
        
        try {
            if ($Force) {
                Stop-Process -Id $process.Id -Force
                Write-Host "Forcefully stopped process $($process.Id)" -ForegroundColor Yellow
            } else {
                Stop-Process -Id $process.Id
                Write-Host "Stopped process $($process.Id)" -ForegroundColor Green
            }
            $stopped = $true
        }
        catch {
            Write-Host "Failed to stop process $($process.Id): $($_.Exception.Message)" -ForegroundColor Red
        }
    }
}

# Method 2: Stop processes using the configured ports
Write-Host "Checking for processes using backend ports..." -ForegroundColor Yellow

if (Test-Port -Port $Port) {
    Write-Host "Found process using HTTP port $Port" -ForegroundColor Cyan
    if (Stop-ProcessByPort -Port $Port) {
        $stopped = $true
    }
}

if (Test-Port -Port $HttpsPort) {
    Write-Host "Found process using HTTPS port $HttpsPort" -ForegroundColor Cyan
    if (Stop-ProcessByPort -Port $HttpsPort) {
        $stopped = $true
    }
}

# Wait a moment and verify the processes are stopped
if ($stopped) {
    Start-Sleep -Seconds 3
    
    $stillRunning = (Test-Port -Port $Port) -or (Test-Port -Port $HttpsPort) -or ((Get-BackendProcesses).Count -gt 0)
    
    if (-not $stillRunning) {
        Write-Host "✅ Backend application stopped successfully!" -ForegroundColor Green
    } else {
        Write-Host "⚠️  Some processes may still be running. Use -Force parameter for forceful termination." -ForegroundColor Yellow
        if (-not $Force) {
            exit 1
        }
    }
} else {
    Write-Host "No backend processes found running." -ForegroundColor Gray
    Write-Host "✅ Backend application is not running." -ForegroundColor Green
}

Write-Host ""
Write-Host "Backend ports status:" -ForegroundColor Yellow
Write-Host "  HTTP Port $Port`: $((Test-Port -Port $Port) ? 'In Use' : 'Available')" -ForegroundColor Gray
Write-Host "  HTTPS Port $HttpsPort`: $((Test-Port -Port $HttpsPort) ? 'In Use' : 'Available')" -ForegroundColor Gray