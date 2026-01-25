#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Create GitHub issues for Store Center frontend enterprise enhancements.

.AUTHOR
    GitHub Copilot

.DESCRIPTION
    This script creates GitHub issues for enterprise-level enhancements needed in the Store Center frontend application
    to align with industry best practices.

.PARAMETER Repository
    The repository in format "owner/repo". Defaults to "mahedee/store-center".

.EXAMPLE
    .\create_enterprise_issues.ps1

.EXAMPLE
    .\create_enterprise_issues.ps1 -Repository "username/repository"
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

# Define all enterprise enhancement issues to create
$issues = @(
    @{
        Title = "🏗️ Create Main Layout Components (Header, Sidebar, Footer)"
        Body = @"
**Issue Type:** Enterprise Enhancement - Layout

**Description:**
Create the core layout structure for the enterprise application.

**Current Problem:**
- No standardized layout structure
- Missing enterprise navigation components
- No consistent header/footer across pages

**Proposed Solution:**
- Create Header component with logo and main navigation
- Create Sidebar component for secondary navigation
- Create Footer component with company info and links
- Create MainLayout wrapper component
- Implement responsive design for mobile/tablet
- Add proper TypeScript interfaces

**Files to Create:**
- `src/components/Layout/Header.tsx`
- `src/components/Layout/Sidebar.tsx` 
- `src/components/Layout/Footer.tsx`
- `src/components/Layout/MainLayout.tsx`
- `src/components/Layout/index.ts`

**Technical Notes:**
- Use CSS Grid/Flexbox for layout
- Implement mobile-first responsive design
- Add proper semantic HTML elements
- Include accessibility attributes

**Acceptance Criteria:**
- [ ] Header component with logo and main navigation
- [ ] Sidebar component for secondary navigation
- [ ] Footer component with company info and links
- [ ] MainLayout wrapper component
- [ ] Responsive design for mobile/tablet
- [ ] Proper TypeScript interfaces
- [ ] All tests pass

**Priority:** High
"@
        Labels = @("enhancement", "layout", "frontend", "high-priority")
    },
    @{
        Title = "🍞 Add Breadcrumb Navigation Component"
        Body = @"
**Issue Type:** Enterprise Enhancement - Navigation

**Description:**
Implement breadcrumb navigation to help users understand their current location in the application hierarchy.

**Current Problem:**
- No breadcrumb navigation
- Users can't easily understand current location
- No way to navigate back to parent sections

**Proposed Solution:**
- Create dynamic breadcrumb component
- Auto-generate breadcrumbs from current route
- Make breadcrumb items clickable (except last item)
- Style breadcrumbs with proper separators
- Add accessibility attributes
- Handle special characters and formatting

**Files to Create:**
- `src/components/Layout/Breadcrumb.tsx`
- `src/hooks/useBreadcrumb.ts` (optional)

**Technical Notes:**
- Use React Router's useLocation hook
- Implement proper ARIA navigation attributes
- Add support for custom breadcrumb titles
- Include home/root breadcrumb

**Acceptance Criteria:**
- [ ] Dynamic breadcrumb component
- [ ] Auto-generate breadcrumbs from current route
- [ ] Clickable breadcrumb items (except last item)
- [ ] Proper separators and styling
- [ ] Accessibility attributes
- [ ] Special characters handling

**Dependencies:** React Router DOM
**Priority:** Medium
"@
        Labels = @("enhancement", "navigation", "ux")
    },
    @{
        Title = "⏳ Implement Loading Spinners and Skeleton Screens"
        Body = @"
**Issue Type:** Enterprise Enhancement - UX

**Description:**
Create reusable loading components to improve user experience during data fetching.

**Current Problem:**
- No loading states for data fetching
- Poor user experience during API calls
- No visual feedback for long operations

**Proposed Solution:**
- Create LoadingSpinner component with different sizes
- Create SkeletonLoader for content placeholders
- Add PageLoader for full-page loading states
- Implement loading states for buttons
- Add proper animations and transitions

**Files to Create:**
- `src/components/UI/LoadingSpinner.tsx`
- `src/components/UI/SkeletonLoader.tsx`
- `src/components/UI/PageLoader.tsx`
- `src/components/UI/ButtonLoader.tsx`

**Technical Notes:**
- Use CSS animations for smooth transitions
- Support different spinner styles and colors
- Implement skeleton loading for cards, tables, and lists
- Add accessibility considerations for screen readers

**Acceptance Criteria:**
- [ ] LoadingSpinner component with different sizes
- [ ] SkeletonLoader for content placeholders
- [ ] PageLoader for full-page loading states
- [ ] Loading states for buttons
- [ ] Proper animations and transitions
- [ ] Accessibility support

**Priority:** Medium
"@
        Labels = @("enhancement", "ui", "ux")
    },
    @{
        Title = "🔔 Add Toast/Notification System"
        Body = @"
**Issue Type:** Enterprise Enhancement - Notifications

**Description:**
Create a notification system for displaying success, error, warning, and info messages.

**Current Problem:**
- No notification system
- No user feedback for actions
- No way to display system messages

**Proposed Solution:**
- Install and configure toast library (react-hot-toast)
- Create NotificationProvider wrapper
- Create notification service/hooks
- Style notifications to match app theme
- Add different notification types (success, error, warning, info)
- Implement auto-dismiss and manual dismiss options

**Files to Create:**
- `src/components/UI/NotificationProvider.tsx`
- `src/hooks/useNotification.ts`
- `src/services/notificationService.ts`

**Technical Notes:**
- Position notifications appropriately (top-right recommended)
- Add icons for different notification types
- Implement queue system for multiple notifications
- Add sound effects (optional)

**Acceptance Criteria:**
- [ ] Toast library configured
- [ ] NotificationProvider wrapper
- [ ] Notification service/hooks
- [ ] Styled notifications matching app theme
- [ ] Different notification types
- [ ] Auto-dismiss and manual dismiss options

**Dependencies:** react-hot-toast or similar library
**Priority:** High
"@
        Labels = @("enhancement", "ui", "notifications", "high-priority")
    },
    @{
        Title = "👤 Implement User Profile Menu"
        Body = @"
**Issue Type:** Enterprise Enhancement - Authentication

**Description:**
Create user menu dropdown with profile options, settings, and logout functionality.

**Current Problem:**
- No user menu
- No user profile access
- No logout functionality

**Proposed Solution:**
- Create dropdown user menu component
- Add user avatar/initials display
- Include menu items: Profile, Settings, Logout
- Implement click outside to close
- Add proper ARIA attributes for accessibility
- Style with hover and focus states

**Files to Create:**
- `src/components/Layout/UserMenu.tsx`
- `src/components/UI/Dropdown.tsx`
- `src/components/UI/Avatar.tsx`

**Technical Notes:**
- Use React portals for dropdown positioning
- Implement keyboard navigation (arrow keys, Enter, Escape)
- Add user authentication status integration
- Include notification badges if needed

**Acceptance Criteria:**
- [ ] Dropdown user menu component
- [ ] User avatar/initials display
- [ ] Menu items: Profile, Settings, Logout
- [ ] Click outside to close
- [ ] ARIA attributes for accessibility
- [ ] Hover and focus states

**Priority:** Medium
"@
        Labels = @("enhancement", "authentication", "ui")
    },
    @{
        Title = "🛡️ Add Error Boundary for Graceful Error Handling"
        Body = @"
**Issue Type:** Enterprise Enhancement - Reliability

**Description:**
Implement error boundaries to catch and display errors gracefully without crashing the entire application.

**Current Problem:**
- No error boundaries
- Application crashes on unhandled errors
- Poor error user experience

**Proposed Solution:**
- Create ErrorBoundary component
- Create custom error fallback UI
- Add error logging functionality
- Implement retry mechanism
- Add different error types handling
- Create error page component

**Files to Create:**
- `src/components/ErrorBoundary/ErrorBoundary.tsx`
- `src/components/ErrorBoundary/ErrorFallback.tsx`
- `src/pages/ErrorPage.tsx`
- `src/services/errorService.ts`

**Technical Notes:**
- Implement React 18 error boundary patterns
- Add integration with error tracking services (Sentry, LogRocket)
- Include development vs production error displays
- Add error reporting functionality

**Acceptance Criteria:**
- [ ] ErrorBoundary component
- [ ] Custom error fallback UI
- [ ] Error logging functionality
- [ ] Retry mechanism
- [ ] Different error types handling
- [ ] Error page component

**Priority:** High
"@
        Labels = @("enhancement", "error-handling", "reliability", "high-priority")
    },
    @{
        Title = "🪟 Implement Reusable Modal/Dialog Components"
        Body = @"
**Issue Type:** Enterprise Enhancement - UI Components

**Description:**
Create a flexible modal system for confirmations, forms, and content display.

**Current Problem:**
- No modal system
- No confirmation dialogs
- No standardized popup components

**Proposed Solution:**
- Create base Modal component
- Create ConfirmDialog component
- Create FormModal component
- Implement portal rendering
- Add keyboard navigation (ESC to close)
- Implement focus trap
- Add backdrop click to close
- Style with animations

**Files to Create:**
- `src/components/UI/Modal.tsx`
- `src/components/UI/ConfirmDialog.tsx`
- `src/components/UI/FormModal.tsx`
- `src/hooks/useModal.ts`

**Technical Notes:**
- Use React portals for rendering outside DOM hierarchy
- Implement focus management and trap
- Add ARIA attributes for accessibility
- Support multiple modal stacking
- Include smooth open/close animations

**Acceptance Criteria:**
- [ ] Base Modal component
- [ ] ConfirmDialog component
- [ ] FormModal component
- [ ] Portal rendering
- [ ] Keyboard navigation (ESC to close)
- [ ] Focus trap implementation
- [ ] Backdrop click to close
- [ ] Animations

**Priority:** Medium
"@
        Labels = @("enhancement", "ui", "modals")
    },
    @{
        Title = "🔐 Add Route Protection and Role-Based Access Control"
        Body = @"
**Issue Type:** Enterprise Enhancement - Security

**Description:**
Implement protected routes and role-based access control for enterprise security.

**Current Problem:**
- No route protection
- No role-based access control
- Security vulnerabilities

**Proposed Solution:**
- Create ProtectedRoute component
- Implement role-based route protection
- Add authentication context/provider
- Create route guards
- Handle unauthorized access redirects
- Add loading states for auth checks

**Files to Create:**
- `src/components/Auth/ProtectedRoute.tsx`
- `src/contexts/AuthContext.tsx`
- `src/hooks/useAuth.ts`
- `src/guards/RouteGuard.tsx`
- `src/services/authService.ts`

**Technical Notes:**
- Integrate with backend authentication
- Implement JWT token handling
- Add refresh token logic
- Include permission-based component rendering
- Add audit logging for security events

**Acceptance Criteria:**
- [ ] ProtectedRoute component
- [ ] Role-based route protection
- [ ] Authentication context/provider
- [ ] Route guards
- [ ] Unauthorized access redirects
- [ ] Loading states for auth checks

**Priority:** High
"@
        Labels = @("security", "authentication", "routes", "high-priority")
    },
    @{
        Title = "📊 Implement Enterprise Data Table with Sorting and Filtering"
        Body = @"
**Issue Type:** Enterprise Enhancement - Data Display

**Description:**
Create a feature-rich data table component for displaying and managing large datasets.

**Current Problem:**
- Basic table components
- No sorting or filtering
- Poor data management UX

**Proposed Solution:**
- Create base DataTable component
- Implement column sorting (ascending/descending)
- Add filtering capabilities
- Implement pagination
- Add row selection (single/multiple)
- Include search functionality
- Add export functionality (CSV/Excel)
- Implement responsive design

**Files to Create:**
- `src/components/UI/DataTable.tsx`
- `src/components/UI/TablePagination.tsx`
- `src/components/UI/TableFilters.tsx`
- `src/hooks/useDataTable.ts`

**Technical Notes:**
- Support virtual scrolling for large datasets
- Implement server-side pagination and filtering
- Add column resizing and reordering
- Include bulk actions functionality
- Add print-friendly styling

**Acceptance Criteria:**
- [ ] Base DataTable component
- [ ] Column sorting (ascending/descending)
- [ ] Filtering capabilities
- [ ] Pagination
- [ ] Row selection (single/multiple)
- [ ] Search functionality
- [ ] Export functionality (CSV/Excel)
- [ ] Responsive design

**Priority:** Medium
"@
        Labels = @("enhancement", "ui", "data-table")
    },
    @{
        Title = "📝 Create Enterprise Form Components with Validation"
        Body = @"
**Issue Type:** Enterprise Enhancement - Forms

**Description:**
Build reusable form components with proper validation and error handling.

**Current Problem:**
- Basic form components
- No validation framework
- Inconsistent form UX

**Proposed Solution:**
- Install React Hook Form and Yup
- Create form wrapper component
- Create input components (Text, Select, Checkbox, Radio)
- Implement form validation
- Add error display components
- Create form builder utility
- Add form submission states

**Files to Create:**
- `src/components/Forms/FormProvider.tsx`
- `src/components/Forms/Input.tsx`
- `src/components/Forms/Select.tsx`
- `src/components/Forms/Checkbox.tsx`
- `src/components/Forms/FormError.tsx`
- `src/schemas/validationSchemas.ts`

**Technical Notes:**
- Use React Hook Form for performance
- Implement real-time validation
- Add custom validation rules
- Include file upload components
- Support conditional field rendering

**Acceptance Criteria:**
- [ ] React Hook Form and Yup installed
- [ ] Form wrapper component
- [ ] Input components (Text, Select, Checkbox, Radio)
- [ ] Form validation
- [ ] Error display components
- [ ] Form builder utility
- [ ] Form submission states

**Dependencies:** react-hook-form, yup or zod for validation
**Priority:** High
"@
        Labels = @("enhancement", "forms", "validation", "high-priority")
    },
    @{
        Title = "🔍 Implement Global Search with Autocomplete"
        Body = @"
**Issue Type:** Enterprise Enhancement - Search

**Description:**
Create a global search component with autocomplete and advanced search options.

**Current Problem:**
- No global search functionality
- Poor search UX
- No autocomplete features

**Proposed Solution:**
- Create search input component
- Implement autocomplete functionality
- Add search results dropdown
- Include recent searches
- Add advanced search modal
- Implement search history
- Add keyboard navigation

**Files to Create:**
- `src/components/Search/SearchBar.tsx`
- `src/components/Search/SearchResults.tsx`
- `src/components/Search/AdvancedSearch.tsx`
- `src/hooks/useSearch.ts`
- `src/services/searchService.ts`

**Technical Notes:**
- Implement debounced search to reduce API calls
- Add search result highlighting
- Include fuzzy search capabilities
- Support search filters and categories
- Add search analytics

**Acceptance Criteria:**
- [ ] Search input component
- [ ] Autocomplete functionality
- [ ] Search results dropdown
- [ ] Recent searches
- [ ] Advanced search modal
- [ ] Search history
- [ ] Keyboard navigation

**Priority:** Medium
"@
        Labels = @("enhancement", "search", "ux")
    },
    @{
        Title = "🎨 Add Theme Support and Dark Mode"
        Body = @"
**Issue Type:** Enterprise Enhancement - Theming

**Description:**
Implement a flexible theme system with light/dark mode support.

**Current Problem:**
- No theme system
- No dark mode support
- Limited customization options

**Proposed Solution:**
- Create theme provider and context
- Define light and dark theme variables
- Create theme toggle component
- Implement CSS custom properties
- Add theme persistence (localStorage)
- Update all components to use theme
- Add smooth theme transitions

**Files to Create:**
- `src/contexts/ThemeContext.tsx`
- `src/themes/lightTheme.ts`
- `src/themes/darkTheme.ts`
- `src/components/UI/ThemeToggle.tsx`
- `src/hooks/useTheme.ts`

**Technical Notes:**
- Use CSS custom properties for theme variables
- Implement system preference detection
- Add theme-aware component styling
- Include accessibility considerations for contrast
- Support custom theme creation

**Acceptance Criteria:**
- [ ] Theme provider and context
- [ ] Light and dark theme variables
- [ ] Theme toggle component
- [ ] CSS custom properties
- [ ] Theme persistence (localStorage)
- [ ] All components use theme
- [ ] Smooth theme transitions

**Priority:** Low
"@
        Labels = @("enhancement", "theme", "ux")
    }
)

Write-Host "Creating $($issues.Count) GitHub issues for Store Center frontend enterprise enhancements..." -ForegroundColor Green

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