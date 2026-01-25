#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Create GitHub labels for Store Center repository.

.AUTHOR
    Mahedee Hasan

.DESCRIPTION
    This script creates standardized GitHub labels for the Store Center repository
    to categorize issues and pull requests effectively.

.PARAMETER Repository
    The repository in format "owner/repo". Defaults to "mahedee/store-center".

.EXAMPLE
    .\create_github_labels.ps1

.EXAMPLE
    .\create_github_labels.ps1 -Repository "username/repository"
#>

param(
    [Parameter(Mandatory=$false)]
    [string]$Repository = "mahedee/store-center"
)

# Check if GitHub CLI is available
try {
    $null = Get-Command gh -ErrorAction Stop
} catch {
    Write-Error "GitHub CLI (gh) is not found. Please install GitHub CLI and ensure it's in your PATH."
    exit 1
}

# Define all labels to create with colors and descriptions
$labels = @(
    # Priority Labels
    @{
        Name = "high-priority"
        Color = "d73a4a"
        Description = "Issues that need immediate attention"
    },
    @{
        Name = "medium-priority"
        Color = "fbca04"
        Description = "Issues that should be addressed soon"
    },
    @{
        Name = "low-priority"
        Color = "0e8a16"
        Description = "Issues that can be addressed later"
    },
    
    # Architecture & Design Labels
    @{
        Name = "architecture"
        Color = "5319e7"
        Description = "Architecture and design related issues"
    },
    @{
        Name = "refactoring"
        Color = "1d76db"
        Description = "Code refactoring and cleanup"
    },
    @{
        Name = "domain-design"
        Color = "7057ff"
        Description = "Domain modeling and design patterns"
    },
    @{
        Name = "organization"
        Color = "c2e0c6"
        Description = "Code organization and structure"
    },
    
    # Security Labels
    @{
        Name = "security"
        Color = "b60205"
        Description = "Security related issues"
    },
    @{
        Name = "authentication"
        Color = "d93f0b"
        Description = "Authentication and authorization"
    },
    @{
        Name = "validation"
        Color = "f9d0c4"
        Description = "Input validation and data validation"
    },
    
    # Performance Labels
    @{
        Name = "performance"
        Color = "ff6b6b"
        Description = "Performance optimization and improvements"
    },
    @{
        Name = "caching"
        Color = "ff8e53"
        Description = "Caching strategies and implementations"
    },
    @{
        Name = "scalability"
        Color = "ff9f43"
        Description = "Scalability improvements"
    },
    
    # Database Labels
    @{
        Name = "database"
        Color = "006b75"
        Description = "Database related issues"
    },
    @{
        Name = "entity-framework"
        Color = "0ea5e9"
        Description = "Entity Framework specific issues"
    },
    @{
        Name = "data-integrity"
        Color = "164e63"
        Description = "Data integrity and consistency"
    },
    @{
        Name = "migration"
        Color = "075985"
        Description = "Database migrations and schema changes"
    },
    
    # API Labels
    @{
        Name = "api"
        Color = "84cc16"
        Description = "API design and functionality"
    },
    @{
        Name = "versioning"
        Color = "65a30d"
        Description = "API versioning and compatibility"
    },
    
    # Development Practices
    @{
        Name = "testing"
        Color = "a21caf"
        Description = "Testing strategies and test coverage"
    },
    @{
        Name = "quality-assurance"
        Color = "c084fc"
        Description = "Quality assurance and code quality"
    },
    @{
        Name = "documentation"
        Color = "0891b2"
        Description = "Documentation improvements"
    },
    
    # Operations & Infrastructure
    @{
        Name = "monitoring"
        Color = "f59e0b"
        Description = "Monitoring and observability"
    },
    @{
        Name = "observability"
        Color = "d97706"
        Description = "Application observability and insights"
    },
    @{
        Name = "health-checks"
        Color = "dc2626"
        Description = "Health check implementations"
    },
    @{
        Name = "logging"
        Color = "7c2d12"
        Description = "Logging and error tracking"
    },
    @{
        Name = "error-handling"
        Color = "991b1b"
        Description = "Error handling and exception management"
    },
    
    # Deployment & DevOps
    @{
        Name = "deployment"
        Color = "059669"
        Description = "Deployment and release management"
    },
    @{
        Name = "docker"
        Color = "0284c7"
        Description = "Docker and containerization"
    },
    @{
        Name = "configuration"
        Color = "6366f1"
        Description = "Application configuration management"
    },
    @{
        Name = "environment"
        Color = "8b5cf6"
        Description = "Environment-specific configurations"
    },
    
    # Background Processing
    @{
        Name = "background-jobs"
        Color = "ea580c"
        Description = "Background job processing and queues"
    },
    
    # Type Labels
    @{
        Name = "enhancement"
        Color = "a2eeef"
        Description = "New feature or request"
    },
    @{
        Name = "bug"
        Color = "d73a4a"
        Description = "Something isn't working"
    },
    @{
        Name = "feature"
        Color = "0075ca"
        Description = "New functionality"
    },
    @{
        Name = "improvement"
        Color = "7057ff"
        Description = "General improvements"
    },
    
    # Status Labels
    @{
        Name = "in-progress"
        Color = "fbca04"
        Description = "Work is currently in progress"
    },
    @{
        Name = "needs-review"
        Color = "0052cc"
        Description = "Needs code review"
    },
    @{
        Name = "ready-for-testing"
        Color = "0e8a16"
        Description = "Ready for testing"
    },
    @{
        Name = "blocked"
        Color = "b60205"
        Description = "Blocked by dependencies or other issues"
    }
)

Write-Host "Creating $($labels.Count) GitHub labels for Store Center repository..." -ForegroundColor Green

$successCount = 0
$failCount = 0
$skippedCount = 0

foreach ($label in $labels) {
    try {
        Write-Host "`nCreating label: $($label.Name)" -ForegroundColor Yellow
        
        # Check if label already exists
        $existingLabel = gh label list -R $Repository --json name -q ".[] | select(.name == `"$($label.Name)`") | .name" 2>$null
        
        if ($existingLabel -eq $label.Name) {
            Write-Host "⏭️  Label '$($label.Name)' already exists, skipping..." -ForegroundColor Cyan
            $skippedCount++
            continue
        }
        
        # Create the label
        $createOutput = gh label create -R $Repository $label.Name --color $label.Color --description $label.Description 2>&1
        
        if ($LASTEXITCODE -eq 0) {
            $successCount++
            Write-Host "✅ Successfully created label: $($label.Name)" -ForegroundColor Green
        } else {
            $failCount++
            Write-Host "❌ Failed to create label '$($label.Name)': $createOutput" -ForegroundColor Red
        }
    } catch {
        $failCount++
        Write-Host "❌ Error creating label '$($label.Name)': $($_.Exception.Message)" -ForegroundColor Red
    }
    
    # Small delay to avoid rate limiting
    Start-Sleep -Milliseconds 200
}

Write-Host "`n" -NoNewline
Write-Host "========================================" -ForegroundColor Blue
Write-Host "Label Creation Summary:" -ForegroundColor Blue
Write-Host "========================================" -ForegroundColor Blue
Write-Host "✅ Successfully created: $successCount labels" -ForegroundColor Green
Write-Host "⏭️  Already existed: $skippedCount labels" -ForegroundColor Cyan
Write-Host "❌ Failed to create: $failCount labels" -ForegroundColor Red
Write-Host "📊 Total labels: $($labels.Count)" -ForegroundColor Blue

if ($successCount -gt 0 -or $skippedCount -gt 0) {
    Write-Host "`n🎉 Labels are available in repository: $Repository" -ForegroundColor Green
    Write-Host "View them at: https://github.com/$Repository/labels" -ForegroundColor Cyan
}

if ($failCount -gt 0) {
    Write-Host "`n⚠️  Some labels failed to create. Please check the error messages above." -ForegroundColor Yellow
}

# Optional: Show how to apply labels to existing issues
if ($successCount -gt 0) {
    Write-Host "`n💡 To apply labels to existing issues, you can use:" -ForegroundColor Yellow
    Write-Host "   gh issue edit <issue-number> -R $Repository --add-label `"label-name`"" -ForegroundColor Gray
    Write-Host "   Example: gh issue edit 55 -R $Repository --add-label `"architecture,high-priority`"" -ForegroundColor Gray
}