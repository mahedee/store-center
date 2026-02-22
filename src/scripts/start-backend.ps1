# Script to start the backend API application if it's not already running
param(
    [switch]$Force
)

$ErrorActionPreference = "Stop"

# Configuration
$BackendPath = "d:\Projects\GitHub\store-center\src\back-end\StoreCenter\StoreCenter.Api"
$ProcessName = "StoreCenter.Api"
$Port = 8080
$HttpsPort = 8443

Write-Host "Checking if backend application is running..." -ForegroundColor Yellow

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

# Function to find dotnet processes running our API
function Get-BackendProcess {
    $processes = Get-Process -Name "dotnet" -ErrorAction SilentlyContinue | Where-Object {
        $_.ProcessName -eq "dotnet" -and 
        $_.MainModule.ModuleName -eq "dotnet.exe"
    }
    
    foreach ($process in $processes) {
        try {
            $commandLine = (Get-CimInstance Win32_Process -Filter "ProcessId = $($process.Id)").CommandLine
            if ($commandLine -like "*StoreCenter.Api*") {
                return $process
            }
        }
        catch {
            # Continue checking other processes
        }
    }
    return $null
}

# Check if backend is already running
$existingProcess = Get-BackendProcess
$portInUse = (Test-Port -Port $Port) -or (Test-Port -Port $HttpsPort)

if ($existingProcess -or $portInUse) {
    if ($Force) {
        Write-Host "Backend appears to be running. Force flag specified, attempting to stop existing process..." -ForegroundColor Yellow
        if ($existingProcess) {
            Stop-Process -Id $existingProcess.Id -Force
            Start-Sleep -Seconds 3
        }
    } else {
        Write-Host "Backend application is already running!" -ForegroundColor Green
        Write-Host "Process ID: $($existingProcess.Id)" -ForegroundColor Cyan
        Write-Host "Use -Force parameter to restart the application." -ForegroundColor Yellow
        exit 0
    }
}

# Start the backend application
Write-Host "Starting backend application..." -ForegroundColor Yellow

if (-not (Test-Path $BackendPath)) {
    Write-Error "Backend path not found: $BackendPath"
    exit 1
}

try {
    Push-Location $BackendPath
    
    Write-Host "Running 'dotnet run' in $BackendPath" -ForegroundColor Cyan
    
    # Start the process in the background
    $process = Start-Process -FilePath "dotnet" -ArgumentList "run" -WorkingDirectory $BackendPath -PassThru
    
    # Wait a moment for the process to start
    Start-Sleep -Seconds 5
    
    # Verify the application started successfully
    $attempts = 0
    $maxAttempts = 12
    $started = $false
    
    while ($attempts -lt $maxAttempts -and -not $started) {
        $attempts++
        Write-Host "Checking if application started (attempt $attempts/$maxAttempts)..." -ForegroundColor Yellow
        
        if ((Test-Port -Port $Port) -or (Test-Port -Port $HttpsPort)) {
            $started = $true
            break
        }
        
        Start-Sleep -Seconds 5
    }
    
    if ($started) {
        Write-Host "✅ Backend application started successfully!" -ForegroundColor Green
        Write-Host "HTTP:  http://localhost:$Port" -ForegroundColor Cyan
        Write-Host "HTTPS: https://localhost:$HttpsPort" -ForegroundColor Cyan
        Write-Host "Swagger: http://localhost:$Port/swagger" -ForegroundColor Cyan
    } else {
        Write-Error "Failed to start backend application within expected time"
        exit 1
    }
}
catch {
    Write-Error "Failed to start backend application: $($_.Exception.Message)"
    exit 1
}
finally {
    Pop-Location
}