using Microsoft.AspNetCore.Mvc;

namespace StoreCenter.Api.Helpers
{
    public static class ApiResponseHelper
    {
        /// <summary>
        /// Returns a success response with optional data.
        /// </summary>
        /// <param name="data">The payload to return (optional).</param>
        /// <param name="message">An optional success message.</param>
        /// <returns>An OkObjectResult with the response structure.</returns>
        public static IActionResult Success(object? data = null, string message = "Operation successful")
        {
            return new OkObjectResult(new
            {
                Success = true,
                Message = message,
                Data = data
            });
        }

        /// <summary>
        /// Returns an error response with optional details.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="details">Additional error details (optional).</param>
        /// <returns>A BadRequestObjectResult with the error response structure.</returns>
        public static IActionResult Error(string message, object? details = null)
        {
            return new BadRequestObjectResult(new
            {
                Success = false,
                Message = message,
                Details = details
            });
        }

        /// <summary>
        /// Returns a validation error response.
        /// </summary>
        /// <param name="errors">A list of validation errors.</param>
        /// <returns>A 422 UnprocessableEntityObjectResult with validation errors.</returns>
        public static IActionResult ValidationError(object errors)
        {
            return new UnprocessableEntityObjectResult(new
            {
                Success = false,
                Message = "Validation failed",
                Errors = errors
            });
        }
    }
}
