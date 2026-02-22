# Script to stop the frontend application if it's running
param(
    [switch]$Force
)

$ErrorActionPreference = "Stop"

# Configuration
$Port = 3000

Write-Host "Stopping frontend application..." -ForegroundColor Yellow

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

# Function to find Next.js/Node processes
function Get-FrontendProcesses {
    $frontendProcesses = @()
    $processes = Get-Process -Name "node" -ErrorAction SilentlyContinue
    
    foreach ($process in $processes) {
        try {
            $commandLine = (Get-CimInstance Win32_Process -Filter "ProcessId = $($process.Id)").CommandLine
            if ($commandLine -like "*next*dev*" -or $commandLine -like "*storecenter.client*" -or $commandLine -like "*npm*run*dev*") {
                $frontendProcesses += $process
            }
        }
        catch {
            # Continue checking other processes
        }
    }
    
    return $frontendProcesses
}

$stopped = $false

# Method 1: Find and stop Next.js/frontend processes by command line
Write-Host "Looking for frontend processes..." -ForegroundColor Yellow
$frontendProcesses = Get-FrontendProcesses

if ($frontendProcesses.Count -gt 0) {
    foreach ($process in $frontendProcesses) {
        Write-Host "Found frontend process: $($process.ProcessName) (PID: $($process.Id))" -ForegroundColor Cyan
        
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

# Method 2: Stop processes using the frontend port
Write-Host "Checking for processes using frontend port..." -ForegroundColor Yellow

if (Test-Port -Port $Port) {
    Write-Host "Found process using port $Port" -ForegroundColor Cyan
    if (Stop-ProcessByPort -Port $Port) {
        $stopped = $true
    }
}

# Method 3: Kill any remaining npm/node processes related to the project (more aggressive)
if ($Force) {
    Write-Host "Force mode: Looking for additional npm/node processes..." -ForegroundColor Yellow
    
    $npmProcesses = Get-Process -Name "npm" -ErrorAction SilentlyContinue
    foreach ($process in $npmProcesses) {
        try {
            Stop-Process -Id $process.Id -Force
            Write-Host "Forcefully stopped npm process $($process.Id)" -ForegroundColor Yellow
            $stopped = $true
        }
        catch {
            # Process might already be stopped
        }
    }
}

# Wait a moment and verify the processes are stopped
if ($stopped) {
    Start-Sleep -Seconds 3
    
    $stillRunning = (Test-Port -Port $Port) -or ((Get-FrontendProcesses).Count -gt 0)
    
    if (-not $stillRunning) {
        Write-Host "✅ Frontend application stopped successfully!" -ForegroundColor Green
    } else {
        Write-Host "⚠️  Some processes may still be running. Use -Force parameter for forceful termination." -ForegroundColor Yellow
        if (-not $Force) {
            exit 1
        }
    }
} else {
    Write-Host "No frontend processes found running." -ForegroundColor Gray
    Write-Host "✅ Frontend application is not running." -ForegroundColor Green
}

Write-Host ""
Write-Host "Frontend port status:" -ForegroundColor Yellow
Write-Host "  Port $Port`: $((Test-Port -Port $Port) ? 'In Use' : 'Available')" -ForegroundColor Gray