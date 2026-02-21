# Error Handling System Implementation

This implementation provides comprehensive error handling across all layers of the StoreCenter application, following RFC 7807 standards and best practices.

## Backend Implementation

### 1. Domain Layer - Custom Exceptions

- **DomainException**: Base abstract class for all domain-specific exceptions
- **CategoryExceptions**: Specific exceptions for category operations
  - `CategoryNotFoundException`
  - `CategoryNameAlreadyExistsException`
  - `InvalidCategoryDataException`

### 2. Application Layer - Result Patterns

- **Result<T>**: Provides a consistent way to handle success/failure states
- **ValidationException**: Handles validation errors with field-specific details
- **NotFoundException**: Standard not found exception

### 3. API Layer - Global Exception Handling

- **ErrorResponse**: RFC 7807 compliant error response model
- **GlobalExceptionHandlingMiddleware**: Centralized exception handling
- **Structured Logging**: Serilog integration with correlation IDs

### 4. Updated Controllers

Controllers now:
- Throw exceptions instead of returning error responses
- Include structured logging
- Use request/response DTOs
- Validate business rules at the domain level

## Frontend Implementation

### 1. Error Utilities (`utils/errorHandling.js`)

- **extractErrorInfo()**: Normalizes errors from different sources
- **formatValidationErrors()**: Formats validation errors for display
- **getUserFriendlyErrorMessage()**: Gets user-friendly error messages
- **isValidationError()**: Type checking for validation errors
- **isNotFoundError()**: Type checking for not found errors

### 2. Error Components

- **ErrorAlert**: Displays errors in a user-friendly format
- **ErrorBoundary**: Catches React component errors

### 3. Updated API Client

- **Enhanced Axios Interceptor**: Handles RFC 7807 error responses
- **Error Transformation**: Converts backend errors to frontend format
- **Backwards Compatibility**: Handles both new and legacy error formats

## Usage Examples

### Backend - Throwing Domain Exceptions

```csharp
// In Category entity
public void ValidateForCreation()
{
    if (string.IsNullOrWhiteSpace(Name))
        throw new InvalidCategoryDataException("Category name is required.");
}

// In Controller
[HttpPost]
public async Task<IActionResult> Post([FromBody] CreateCategoryRequest request)
{
    var category = new Category { Name = request.Name };
    category.ValidateForCreation(); // Throws exception if invalid
    
    // Exception is caught by GlobalExceptionHandlingMiddleware
    return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
}
```

### Frontend - Using Error Components

```jsx
import { ErrorAlert } from '@/components/Error';
import { isValidationError, extractErrorInfo } from '@/utils/errorHandling';

function MyComponent() {
  const [error, setError] = useState(null);
  
  const handleSubmit = async () => {
    try {
      await createCategory({ name, description });
    } catch (err) {
      setError(err); // ErrorAlert will handle the display
    }
  };

  return (
    <div>
      {error && <ErrorAlert error={error} />}
      {/* Your form here */}
    </div>
  );
}
```

## Error Response Format (RFC 7807)

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Validation Error", 
  "status": 400,
  "detail": "One or more validation errors occurred",
  "instance": "/api/categories",
  "traceId": "12345",
  "errors": {
    "Name": ["Category name cannot be empty"]
  }
}
```

## Configuration

### Backend - appsettings.json

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {TraceId} {Message:lj}{NewLine}{Exception}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/storecenter-.txt",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7
        }
      }
    ],
    "Enrich": ["FromLogContext", "WithTraceIdentifier"]
  }
}
```

## Benefits

1. **Consistent Error Responses**: All APIs return standardized error format
2. **Better User Experience**: Clear, actionable error messages
3. **Easier Debugging**: Structured logging with correlation IDs
4. **Type-Safe Error Handling**: Frontend can identify error types
5. **RFC 7807 Compliance**: Industry-standard error format
6. **Backwards Compatibility**: Handles both old and new error formats

## Testing

Use the `ErrorHandlingDemo` component to test various error scenarios:

1. Validation errors
2. Not found errors  
3. Server errors
4. Success cases

The demo component shows how errors are handled and provides analysis of error types.

## Next Steps

1. **Extend to Other Controllers**: Apply similar patterns to all controllers
2. **Add More Domain Exceptions**: Create specific exceptions for each entity
3. **Implement Retry Policies**: For transient failures
4. **Add Error Monitoring**: Integrate with monitoring services
5. **Create Error Documentation**: Document all possible error scenarios