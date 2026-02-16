// src/components/Error/ErrorAlert.jsx
import React from 'react';
import { extractErrorInfo, formatValidationErrors } from '../../utils/errorHandling';

const ErrorAlert = ({ error, className = '' }) => {
  if (!error) return null;

  const errorInfo = extractErrorInfo(error);
  const validationErrors = formatValidationErrors(errorInfo.errors);

  return (
    <div className={`bg-red-50 border border-red-200 rounded-md p-4 ${className}`}>
      <div className="flex">
        <div className="flex-shrink-0">
          <svg className="h-5 w-5 text-red-400" viewBox="0 0 20 20" fill="currentColor">
            <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clipRule="evenodd" />
          </svg>
        </div>
        <div className="ml-3">
          <h3 className="text-sm font-medium text-red-800">
            {errorInfo.title}
          </h3>
          
          {/* Main error detail */}
          {errorInfo.detail && (
            <div className="mt-2 text-sm text-red-700">
              {errorInfo.detail}
            </div>
          )}
          
          {/* Validation errors */}
          {validationErrors.length > 0 && (
            <div className="mt-2">
              <ul className="list-disc list-inside text-sm text-red-700">
                {validationErrors.map((validationError, index) => (
                  <li key={index}>
                    {validationError.displayText}
                  </li>
                ))}
              </ul>
            </div>
          )}

          {/* Trace ID for debugging */}
          {process.env.NODE_ENV === 'development' && errorInfo.traceId && (
            <div className="mt-2 text-xs text-red-600">
              Trace ID: {errorInfo.traceId}
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default ErrorAlert;