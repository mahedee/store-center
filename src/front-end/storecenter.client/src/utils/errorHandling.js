// src/utils/errorHandling.js
// Error handling utilities for API responses

/**
 * Extracts error information from an Axios error response
 * @param {Error} error - The error from axios
 * @returns {Object} Normalized error object
 */
export const extractErrorInfo = (error) => {
  // Check if it's a network error
  if (!error.response) {
    return {
      type: 'network',
      title: 'Network Error',
      detail: 'Unable to connect to server. Please check your connection.',
      status: 0,
      errors: {}
    };
  }

  // Check if it's the new RFC 7807 format
  if (error.rfc7807Error) {
    return error.rfc7807Error;
  }

  // Handle legacy error responses
  const { status, data } = error.response;
  
  return {
    type: 'legacy',
    title: getErrorTitleFromStatus(status),
    detail: data?.message || data?.title || 'An error occurred',
    status,
    errors: data?.errors || {}
  };
};

/**
 * Gets a user-friendly error title from HTTP status code
 * @param {number} status - HTTP status code
 * @returns {string} User-friendly title
 */
const getErrorTitleFromStatus = (status) => {
  switch (status) {
    case 400: return 'Bad Request';
    case 401: return 'Unauthorized';
    case 403: return 'Forbidden';
    case 404: return 'Not Found';
    case 422: return 'Validation Error';
    case 500: return 'Server Error';
    default: return 'Error';
  }
};

/**
 * Formats validation errors for display
 * @param {Object} errors - Validation errors object
 * @returns {Array} Array of formatted error messages
 */
export const formatValidationErrors = (errors) => {
  if (!errors || typeof errors !== 'object') {
    return [];
  }

  const formattedErrors = [];
  
  Object.entries(errors).forEach(([field, messages]) => {
    if (Array.isArray(messages)) {
      messages.forEach(message => {
        formattedErrors.push({
          field,
          message,
          displayText: field === 'General' ? message : `${field}: ${message}`
        });
      });
    }
  });

  return formattedErrors;
};

/**
 * Gets a user-friendly error message from an error object
 * @param {Error} error - The error object
 * @returns {string} User-friendly error message
 */
export const getUserFriendlyErrorMessage = (error) => {
  const errorInfo = extractErrorInfo(error);
  
  // For validation errors, show the first validation message
  if (errorInfo.errors && Object.keys(errorInfo.errors).length > 0) {
    const validationErrors = formatValidationErrors(errorInfo.errors);
    if (validationErrors.length > 0) {
      return validationErrors[0].displayText;
    }
  }

  // Return the detail or a generic message
  return errorInfo.detail || 'An unexpected error occurred';
};

/**
 * Checks if an error is a validation error
 * @param {Error} error - The error object
 * @returns {boolean} True if it's a validation error
 */
export const isValidationError = (error) => {
  const errorInfo = extractErrorInfo(error);
  return errorInfo.status === 400 && 
         (errorInfo.type?.includes('validation') || Object.keys(errorInfo.errors || {}).length > 0);
};

/**
 * Checks if an error is a not found error
 * @param {Error} error - The error object
 * @returns {boolean} True if it's a not found error
 */
export const isNotFoundError = (error) => {
  const errorInfo = extractErrorInfo(error);
  return errorInfo.status === 404;
};