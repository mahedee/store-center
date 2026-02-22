# Script to start the frontend application if it's not already running
param(
    [switch]$Force
)

$ErrorActionPreference = "Stop"

# Configuration
$FrontendPath = "d:\Projects\GitHub\store-center\src\front-end\storecenter.client"
$Port = 3000

Write-Host "Checking if frontend application is running..." -ForegroundColor Yellow

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

# Function to find Next.js processes
function Get-FrontendProcess {
    $processes = Get-Process -Name "node" -ErrorAction SilentlyContinue | Where-Object {
        $_.ProcessName -eq "node"
    }
    
    foreach ($process in $processes) {
        try {
            $commandLine = (Get-CimInstance Win32_Process -Filter "ProcessId = $($process.Id)").CommandLine
            if ($commandLine -like "*next*dev*" -or $commandLine -like "*storecenter.client*") {
                return $process
            }
        }
        catch {
            # Continue checking other processes
        }
    }
    return $null
}

# Check if frontend is already running
$existingProcess = Get-FrontendProcess
$portInUse = Test-Port -Port $Port

if ($existingProcess -or $portInUse) {
    if ($Force) {
        Write-Host "Frontend appears to be running. Force flag specified, attempting to stop existing process..." -ForegroundColor Yellow
        if ($existingProcess) {
            Stop-Process -Id $existingProcess.Id -Force
            Start-Sleep -Seconds 3
        }
        
        # Kill any process using the port
        try {
            $portProcess = Get-NetTCPConnection -LocalPort $Port -ErrorAction SilentlyContinue | Select-Object -First 1
            if ($portProcess) {
                Stop-Process -Id $portProcess.OwningProcess -Force -ErrorAction SilentlyContinue
                Start-Sleep -Seconds 2
            }
        }
        catch {
            # Port might not be in use anymore
        }
    } else {
        Write-Host "Frontend application is already running!" -ForegroundColor Green
        if ($existingProcess) {
            Write-Host "Process ID: $($existingProcess.Id)" -ForegroundColor Cyan
        }
        Write-Host "URL: http://localhost:$Port" -ForegroundColor Cyan
        Write-Host "Use -Force parameter to restart the application." -ForegroundColor Yellow
        exit 0
    }
}

# Start the frontend application
Write-Host "Starting frontend application..." -ForegroundColor Yellow

if (-not (Test-Path $FrontendPath)) {
    Write-Error "Frontend path not found: $FrontendPath"
    exit 1
}

try {
    Push-Location $FrontendPath
    
    # Check if node_modules exists, if not run npm install
    if (-not (Test-Path "node_modules")) {
        Write-Host "Installing dependencies..." -ForegroundColor Yellow
        npm install
    }
    
    Write-Host "Running 'npm run dev' in $FrontendPath" -ForegroundColor Cyan
    
    # Start the process in the background
    $process = Start-Process -FilePath "npm" -ArgumentList "run", "dev" -WorkingDirectory $FrontendPath -PassThru
    
    # Wait a moment for the process to start
    Start-Sleep -Seconds 8
    
    # Verify the application started successfully
    $attempts = 0
    $maxAttempts = 15
    $started = $false
    
    while ($attempts -lt $maxAttempts -and -not $started) {
        $attempts++
        Write-Host "Checking if application started (attempt $attempts/$maxAttempts)..." -ForegroundColor Yellow
        
        if (Test-Port -Port $Port) {
            $started = $true
            break
        }
        
        Start-Sleep -Seconds 4
    }
    
    if ($started) {
        Write-Host "✅ Frontend application started successfully!" -ForegroundColor Green
        Write-Host "URL: http://localhost:$Port" -ForegroundColor Cyan
    } else {
        Write-Error "Failed to start frontend application within expected time"
        exit 1
    }
}
catch {
    Write-Error "Failed to start frontend application: $($_.Exception.Message)"
    exit 1
}
finally {
    Pop-Location
}