# start backend
.\scripts\start-backend.ps1  

## start frontend
.\scripts\start-frontend.ps1

## stop backend
.\scripts\stop-backend.ps1

## stop frontend
.\scripts\stop-frontend.ps1


# Start everything
.\scripts\start-applications.ps1

# Start only backend
.\scripts\start-applications.ps1 -BackendOnly

# Force restart both applications
.\scripts\start-applications.ps1 -Force

# Stop everything  
.\scripts\stop-applications.ps1

# Stop only frontend
.\scripts\stop-applications.ps1 -FrontendOnly