#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Create GitHub issues for Store Center application improvements.

.AUTHOR
    Mahedee Hasan

.DESCRIPTION
    This script creates GitHub issues for identified improvements needed in the Store Center application
    to align with industry best practices.

.PARAMETER Repository
    The repository in format "owner/repo". Defaults to "mahedee/store-center".

.EXAMPLE
    .\create_improvement_issues.ps1

.EXAMPLE
    .\create_improvement_issues.ps1 -Repository "username/repository"
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

# Define all issues to create
$issues = @(
    @{
        Title = "🔧 Inconsistent Repository Pattern Implementation"
        Body = @"
**Issue Type:** Architecture & Structure

**Description:**
The application has mixed repository interfaces in different namespaces (`StoreCenter.Domain.Repositories` vs `StoreCenter.Infrastructure.Interfaces`). This creates confusion and breaks the Clean Architecture principle.

**Current Problem:**
- Repository interfaces are scattered across different namespaces
- Inconsistent naming conventions
- Breaks Clean Architecture separation of concerns

**Proposed Solution:**
- Move all repository interfaces to `Domain.Repositories`
- Move all implementations to `Infrastructure.Repositories`
- Standardize naming conventions
- Update dependency injection accordingly

**Acceptance Criteria:**
- [ ] All repository interfaces in `Domain.Repositories`
- [ ] All implementations in `Infrastructure.Repositories`
- [ ] Consistent naming pattern
- [ ] Updated DI registrations
- [ ] All tests pass

**Labels:** architecture, refactoring, high-priority
"@
        Labels = @("architecture", "refactoring", "high-priority")
    },
    @{
        Title = "🔄 Missing Unit of Work Pattern"
        Body = @"
**Issue Type:** Architecture & Structure

**Description:**
Each repository manages its own database context, which can lead to transaction issues and poor performance in complex operations.

**Current Problem:**
- No centralized transaction management
- Potential data consistency issues
- Poor performance in multi-repository operations
- No atomic operations across multiple entities

**Proposed Solution:**
- Implement Unit of Work pattern
- Create `IUnitOfWork` interface in Domain layer
- Implement `UnitOfWork` class in Infrastructure layer
- Manage transactions across multiple repositories
- Add rollback capabilities

**Acceptance Criteria:**
- [ ] `IUnitOfWork` interface created
- [ ] `UnitOfWork` implementation
- [ ] Repository pattern updated to work with UoW
- [ ] Transaction management implemented
- [ ] Performance tests pass

**Labels:** architecture, performance, medium-priority
"@
        Labels = @("architecture", "performance", "medium-priority")
    },
    @{
        Title = "📁 Inconsistent DTO Naming and Location"
        Body = @"
**Issue Type:** Architecture & Structure

**Description:**
DTOs are scattered with inconsistent naming (`BrandDto` vs `CreateBrandDto`). Some are in `Application.DTOs`, others in `Application.Dtos`.

**Current Problem:**
- Inconsistent folder casing (`DTOs` vs `Dtos`)
- Mixed naming conventions
- Poor organization structure

**Proposed Solution:**
- Standardize DTO naming conventions
- Organize in feature folders: `Application.Features.Brands.DTOs`
- Implement consistent patterns across all features
- Update all references

**Acceptance Criteria:**
- [ ] Consistent DTO naming convention
- [ ] Feature-based folder structure
- [ ] All DTOs properly organized
- [ ] Updated import statements
- [ ] Documentation updated

**Labels:** organization, refactoring, low-priority
"@
        Labels = @("organization", "refactoring", "low-priority")
    },
    @{
        Title = "🛡️ Missing Authentication & Authorization"
        Body = @"
**Issue Type:** Security & Validation

**Description:**
No JWT authentication, role-based authorization, or API security middleware implemented.

**Current Problem:**
- No authentication mechanism
- Unprotected API endpoints
- No role-based access control
- Missing security middleware

**Proposed Solution:**
- Implement JWT authentication
- Add role-based authorization policies
- Protect API endpoints with `[Authorize]` attributes
- Add authentication middleware
- Implement refresh token mechanism

**Acceptance Criteria:**
- [ ] JWT authentication implemented
- [ ] Role-based authorization policies
- [ ] Protected API endpoints
- [ ] Authentication middleware configured
- [ ] Refresh token mechanism
- [ ] Security tests pass

**Labels:** security, authentication, high-priority
"@
        Labels = @("security", "authentication", "high-priority")
    },
    @{
        Title = "✅ Insufficient Input Validation"
        Body = @"
**Issue Type:** Security & Validation

**Description:**
Limited validation on DTOs and missing business rule validations.

**Current Problem:**
- Basic data annotations only
- No business rule validation
- Missing custom validators
- No validation error handling

**Proposed Solution:**
- Implement FluentValidation
- Create custom validators for business rules
- Add comprehensive validation pipeline
- Implement proper error responses

**Acceptance Criteria:**
- [ ] FluentValidation implemented
- [ ] Custom business validators
- [ ] Validation pipeline configured
- [ ] Proper error responses
- [ ] Validation tests pass

**Labels:** validation, security, medium-priority
"@
        Labels = @("validation", "security", "medium-priority")
    },
    @{
        Title = "📊 Missing API Versioning"
        Body = @"
**Issue Type:** Security & Validation

**Description:**
No API versioning strategy for future compatibility.

**Current Problem:**
- No version strategy
- Breaking changes affect all consumers
- No backward compatibility plan

**Proposed Solution:**
- Implement API versioning using Microsoft.AspNetCore.Mvc.Versioning
- Add version headers or URL-based versioning
- Create versioning strategy documentation

**Acceptance Criteria:**
- [ ] API versioning package installed
- [ ] Versioning strategy implemented
- [ ] Version headers configured
- [ ] Documentation updated
- [ ] Backward compatibility maintained

**Labels:** api, versioning, medium-priority
"@
        Labels = @("api", "versioning", "medium-priority")
    },
    @{
        Title = "⚙️ Missing Entity Configurations"
        Body = @"
**Issue Type:** Data & Performance

**Description:**
Entity Framework configurations are not explicit, relying on conventions.

**Current Problem:**
- Implicit EF configurations
- Potential mapping issues
- No explicit relationships defined
- Missing constraints and indexes

**Proposed Solution:**
- Create explicit entity configurations using `IEntityTypeConfiguration<T>`
- Define all relationships explicitly
- Add proper indexes and constraints
- Organize configurations in separate files

**Acceptance Criteria:**
- [ ] Entity configurations created
- [ ] Explicit relationship mappings
- [ ] Proper indexes defined
- [ ] Constraints implemented
- [ ] Configuration tests pass

**Labels:** database, entity-framework, medium-priority
"@
        Labels = @("database", "entity-framework", "medium-priority")
    },
    @{
        Title = "🗑️ No Soft Delete Implementation"
        Body = @"
**Issue Type:** Data & Performance

**Description:**
Hard deletes can cause data integrity issues in production systems.

**Current Problem:**
- Hard deletes remove data permanently
- No audit trail for deleted records
- Potential data integrity issues
- Cannot restore accidentally deleted data

**Proposed Solution:**
- Implement soft delete with `IsDeleted` flag
- Add global query filters
- Create soft delete interceptor
- Add restore functionality

**Acceptance Criteria:**
- [ ] Soft delete mechanism implemented
- [ ] Global query filters applied
- [ ] Audit trail maintained
- [ ] Restore functionality added
- [ ] Migration for existing data

**Labels:** database, data-integrity, high-priority
"@
        Labels = @("database", "data-integrity", "high-priority")
    },
    @{
        Title = "⚡ Missing Caching Strategy"
        Body = @"
**Issue Type:** Data & Performance

**Description:**
No caching mechanism for frequently accessed data like categories and brands.

**Current Problem:**
- No caching layer
- Repeated database calls for static data
- Poor performance for read-heavy operations
- No cache invalidation strategy

**Proposed Solution:**
- Implement Redis caching or in-memory caching
- Cache frequently accessed data
- Add cache invalidation policies
- Implement cache-aside pattern

**Acceptance Criteria:**
- [ ] Caching layer implemented
- [ ] Cache policies defined
- [ ] Cache invalidation strategy
- [ ] Performance benchmarks improved
- [ ] Cache monitoring added

**Labels:** performance, caching, medium-priority
"@
        Labels = @("performance", "caching", "medium-priority")
    },
    @{
        Title = "❗ Insufficient Error Handling & Logging"
        Body = @"
**Issue Type:** Business Logic & Error Handling

**Description:**
No global exception handling, structured logging, or custom exception types.

**Current Problem:**
- No global exception middleware
- Basic logging implementation
- No structured logging
- Missing custom exception types

**Proposed Solution:**
- Add global exception middleware
- Implement Serilog for structured logging
- Create custom exception hierarchy
- Add proper error responses

**Acceptance Criteria:**
- [ ] Global exception middleware
- [ ] Structured logging with Serilog
- [ ] Custom exception types
- [ ] Proper error responses
- [ ] Log aggregation configured

**Labels:** logging, error-handling, high-priority
"@
        Labels = @("logging", "error-handling", "high-priority")
    },
    @{
        Title = "🎯 Missing Result Pattern"
        Body = @"
**Issue Type:** Business Logic & Error Handling

**Description:**
Services return nulls or throw exceptions instead of explicit success/failure results.

**Current Problem:**
- Inconsistent error handling
- Exception-based flow control
- Poor API consumer experience
- No explicit success/failure indication

**Proposed Solution:**
- Implement Result<T> pattern
- Create success/failure result types
- Update service methods to return Results
- Improve error communication

**Acceptance Criteria:**
- [ ] Result<T> pattern implemented
- [ ] Service methods updated
- [ ] Clear success/failure responses
- [ ] Documentation updated
- [ ] Client code examples provided

**Labels:** architecture, error-handling, medium-priority
"@
        Labels = @("architecture", "error-handling", "medium-priority")
    },
    @{
        Title = "🏗️ Business Logic in Controllers"
        Body = @"
**Issue Type:** Business Logic & Error Handling

**Description:**
Some business logic might leak into controllers instead of being in domain services.

**Current Problem:**
- Controllers contain business logic
- Violates separation of concerns
- Hard to test business rules
- Poor maintainability

**Proposed Solution:**
- Implement Domain Services
- Move business logic to appropriate layers
- Keep controllers thin (HTTP concerns only)
- Add domain service interfaces

**Acceptance Criteria:**
- [ ] Domain services created
- [ ] Business logic moved from controllers
- [ ] Controllers handle only HTTP concerns
- [ ] Improved testability
- [ ] Clear separation of concerns

**Labels:** architecture, domain-design, medium-priority
"@
        Labels = @("architecture", "domain-design", "medium-priority")
    },
    @{
        Title = "🧪 Missing Comprehensive Testing Strategy"
        Body = @"
**Issue Type:** Testing & Documentation

**Description:**
No unit tests, integration tests, or test infrastructure.

**Current Problem:**
- No test coverage
- No testing framework setup
- No mocking strategy
- No integration test infrastructure

**Proposed Solution:**
- Add xUnit for unit testing
- Implement Moq for mocking
- Add TestContainers for integration tests
- Create test data builders
- Set up CI/CD test pipeline

**Acceptance Criteria:**
- [ ] Unit testing framework setup
- [ ] Mocking framework configured
- [ ] Integration test infrastructure
- [ ] Test coverage reports
- [ ] CI/CD pipeline with tests

**Labels:** testing, quality-assurance, high-priority
"@
        Labels = @("testing", "quality-assurance", "high-priority")
    },
    @{
        Title = "📖 Insufficient API Documentation"
        Body = @"
**Issue Type:** Testing & Documentation

**Description:**
Basic Swagger setup without detailed documentation, examples, or response schemas.

**Current Problem:**
- Basic Swagger configuration
- Missing XML documentation
- No API examples
- Poor response documentation

**Proposed Solution:**
- Enhanced Swagger configuration
- Add XML comments to controllers
- Include request/response examples
- Add comprehensive API documentation

**Acceptance Criteria:**
- [ ] Enhanced Swagger setup
- [ ] XML documentation added
- [ ] API examples included
- [ ] Response schemas documented
- [ ] API documentation published

**Labels:** documentation, api, medium-priority
"@
        Labels = @("documentation", "api", "medium-priority")
    },
    @{
        Title = "🏥 Missing Health Checks"
        Body = @"
**Issue Type:** Performance & Scalability

**Description:**
No health check endpoints for monitoring application and database status.

**Current Problem:**
- No health monitoring
- No database connectivity checks
- No external service monitoring
- Poor observability

**Proposed Solution:**
- Implement health check endpoints
- Add database health checks
- Monitor external service dependencies
- Create health check dashboard

**Acceptance Criteria:**
- [ ] Health check endpoints implemented
- [ ] Database connectivity checks
- [ ] External service monitoring
- [ ] Health check UI/dashboard
- [ ] Monitoring alerts configured

**Labels:** monitoring, health-checks, medium-priority
"@
        Labels = @("monitoring", "health-checks", "medium-priority")
    },
    @{
        Title = "🔄 No Background Job Processing"
        Body = @"
**Issue Type:** Performance & Scalability

**Description:**
No mechanism for handling background tasks like inventory updates or email notifications.

**Current Problem:**
- No background job processing
- No task queuing system
- Cannot handle long-running operations
- No retry mechanisms

**Proposed Solution:**
- Implement Hangfire or Azure Service Bus
- Create background job infrastructure
- Add retry and error handling
- Monitor job execution

**Acceptance Criteria:**
- [ ] Background job framework implemented
- [ ] Job queuing system setup
- [ ] Retry mechanisms configured
- [ ] Job monitoring dashboard
- [ ] Error handling for failed jobs

**Labels:** background-jobs, scalability, medium-priority
"@
        Labels = @("background-jobs", "scalability", "medium-priority")
    },
    @{
        Title = "⚙️ Missing Configuration Management"
        Body = @"
**Issue Type:** Performance & Scalability

**Description:**
Configuration is not properly organized or validated.

**Current Problem:**
- Loose configuration management
- No configuration validation
- No environment-specific settings organization
- No strongly-typed configuration

**Proposed Solution:**
- Implement strongly-typed configuration classes
- Add configuration validation
- Organize environment-specific settings
- Add configuration documentation

**Acceptance Criteria:**
- [ ] Strongly-typed configuration classes
- [ ] Configuration validation implemented
- [ ] Environment-specific organization
- [ ] Configuration documentation
- [ ] Startup validation checks

**Labels:** configuration, environment, low-priority
"@
        Labels = @("configuration", "environment", "low-priority")
    },
    @{
        Title = "🐳 Missing Containerization"
        Body = @"
**Issue Type:** Deployment & DevOps

**Description:**
No Docker support for consistent deployment across environments.

**Current Problem:**
- No containerization strategy
- Environment inconsistencies
- Manual deployment process
- No container orchestration

**Proposed Solution:**
- Add Dockerfile for application
- Create docker-compose for development
- Add multi-stage builds
- Create container deployment strategy

**Acceptance Criteria:**
- [ ] Dockerfile created
- [ ] docker-compose setup
- [ ] Multi-stage build configured
- [ ] Container registry setup
- [ ] Deployment documentation

**Labels:** docker, deployment, medium-priority
"@
        Labels = @("docker", "deployment", "medium-priority")
    },
    @{
        Title = "🗄️ No Database Migration Strategy"
        Body = @"
**Issue Type:** Deployment & DevOps

**Description:**
Manual migration handling without proper deployment pipeline integration.

**Current Problem:**
- Manual migration process
- No automated migration in deployment
- No rollback strategy
- Migration conflicts in team development

**Proposed Solution:**
- Implement automated migration strategy
- Add rollback capabilities
- Create migration deployment pipeline
- Add migration conflict resolution

**Acceptance Criteria:**
- [ ] Automated migration in CI/CD
- [ ] Rollback strategy implemented
- [ ] Migration conflict handling
- [ ] Migration documentation
- [ ] Environment-specific migration scripts

**Labels:** database, deployment, migration, medium-priority
"@
        Labels = @("database", "deployment", "migration", "medium-priority")
    },
    @{
        Title = "📊 Missing Monitoring & Observability"
        Body = @"
**Issue Type:** Deployment & DevOps

**Description:**
No application insights, metrics, or distributed tracing.

**Current Problem:**
- No application monitoring
- No performance metrics
- No error tracking
- No distributed tracing

**Proposed Solution:**
- Add Application Insights or similar
- Implement OpenTelemetry
- Create performance monitoring
- Add error tracking and alerting

**Acceptance Criteria:**
- [ ] Application monitoring implemented
- [ ] Performance metrics tracked
- [ ] Error tracking configured
- [ ] Distributed tracing setup
- [ ] Monitoring dashboard created
- [ ] Alert rules configured

**Labels:** monitoring, observability, performance, high-priority
"@
        Labels = @("monitoring", "observability", "performance", "high-priority")
    }
)

Write-Host "Creating $($issues.Count) GitHub issues for Store Center improvements..." -ForegroundColor Green

$successCount = 0
$failCount = 0

foreach ($issue in $issues) {
    try {
        Write-Host "`nCreating issue: $($issue.Title)" -ForegroundColor Yellow
        
        # Create the issue (without labels to avoid label not found errors)
        $createOutput = gh issue create -R $Repository --title $issue.Title --body $issue.Body 2>&1
        
        if ($LASTEXITCODE -eq 0) {
            $successCount++
            Write-Host "✅ Successfully created issue!" -ForegroundColor Green
            Write-Host "   URL: $createOutput" -ForegroundColor Cyan
        } else {
            $failCount++
            Write-Host "❌ Failed to create issue: $createOutput" -ForegroundColor Red
        }
    } catch {
        $failCount++
        Write-Host "❌ Error creating issue: $($_.Exception.Message)" -ForegroundColor Red
    }
    
    # Small delay to avoid rate limiting
    Start-Sleep -Milliseconds 500
}

Write-Host "`n" -NoNewline
Write-Host "========================================" -ForegroundColor Blue
Write-Host "Issue Creation Summary:" -ForegroundColor Blue
Write-Host "========================================" -ForegroundColor Blue
Write-Host "✅ Successfully created: $successCount issues" -ForegroundColor Green
Write-Host "❌ Failed to create: $failCount issues" -ForegroundColor Red
Write-Host "📊 Total issues: $($issues.Count)" -ForegroundColor Blue

if ($successCount -gt 0) {
    Write-Host "`n🎉 Issues have been created in repository: $Repository" -ForegroundColor Green
    Write-Host "View them at: https://github.com/$Repository/issues" -ForegroundColor Cyan
}

if ($failCount -gt 0) {
    Write-Host "`n⚠️  Some issues failed to create. Please check the error messages above." -ForegroundColor Yellow
}