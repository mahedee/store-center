// src/components/Demo/ErrorHandlingDemo.jsx
'use client';

import { useState } from 'react';
import { createCategory, fetchCategories, updateCategory, deleteCategory } from '../../api/categoryApi';
import ErrorAlert from '../Error/ErrorAlert';
import { isValidationError, isNotFoundError, extractErrorInfo } from '../../utils/errorHandling';

export default function ErrorHandlingDemo() {
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);
  const [response, setResponse] = useState(null);

  const clearState = () => {
    setError(null);
    setResponse(null);
  };

  const testValidationError = async () => {
    clearState();
    setLoading(true);
    
    try {
      // Try to create a category with empty name (should trigger validation error)
      await createCategory({ name: '', description: 'Test category' });
      setResponse({ message: 'Unexpected success - should have failed validation' });
    } catch (err) {
      setError(err);
      
      if (isValidationError(err)) {
        console.log('✅ Correctly identified as validation error');
        const errorInfo = extractErrorInfo(err);
        console.log('Error details:', errorInfo);
      }
    } finally {
      setLoading(false);
    }
  };

  const testNotFoundError = async () => {
    clearState();
    setLoading(true);
    
    try {
      // Try to delete a non-existent category
      const nonExistentId = '00000000-0000-0000-0000-000000000000';
      await deleteCategory(nonExistentId);
      setResponse({ message: 'Unexpected success - should have failed with not found' });
    } catch (err) {
      setError(err);
      
      if (isNotFoundError(err)) {
        console.log('✅ Correctly identified as not found error');
        const errorInfo = extractErrorInfo(err);
        console.log('Error details:', errorInfo);
      }
    } finally {
      setLoading(false);
    }
  };

  const testServerError = async () => {
    clearState();
    setLoading(true);
    
    try {
      // This might trigger a server error depending on your backend state
      await fetchCategories(1, -1); // Invalid page size
      setResponse({ message: 'Request completed - check console for details' });
    } catch (err) {
      setError(err);
      const errorInfo = extractErrorInfo(err);
      console.log('Error details:', errorInfo);
    } finally {
      setLoading(false);
    }
  };

  const testSuccessfulRequest = async () => {
    clearState();
    setLoading(true);
    
    try {
      // Create a valid category
      const result = await createCategory({ 
        name: `Test Category ${Date.now()}`, 
        description: 'A test category created by the demo' 
      });
      setResponse({ 
        message: 'Category created successfully!', 
        data: result 
      });
    } catch (err) {
      setError(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-4xl mx-auto p-6 bg-white rounded-lg shadow">
      <h2 className="text-2xl font-bold mb-6 text-gray-800">Error Handling System Demo</h2>
      
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-6">
        <button
          onClick={testValidationError}
          disabled={loading}
          className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600 disabled:opacity-50"
        >
          Test Validation Error
        </button>
        
        <button
          onClick={testNotFoundError}
          disabled={loading}
          className="bg-orange-500 text-white px-4 py-2 rounded hover:bg-orange-600 disabled:opacity-50"
        >
          Test Not Found Error
        </button>
        
        <button
          onClick={testServerError}
          disabled={loading}
          className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600 disabled:opacity-50"
        >
          Test Server Error
        </button>
        
        <button
          onClick={testSuccessfulRequest}
          disabled={loading}
          className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 disabled:opacity-50"
        >
          Test Success Case
        </button>
      </div>

      {loading && (
        <div className="text-center py-4">
          <div className="inline-block animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
          <p className="mt-2 text-gray-600">Making request...</p>
        </div>
      )}

      {/* Error Display */}
      {error && (
        <div className="mb-6">
          <h3 className="text-lg font-semibold mb-2 text-red-600">Error Response:</h3>
          <ErrorAlert error={error} />
          
          {/* Additional error analysis for demo purposes */}
          <div className="mt-4 p-4 bg-gray-100 rounded">
            <h4 className="font-medium mb-2">Error Analysis:</h4>
            <ul className="text-sm space-y-1">
              <li>Is Validation Error: {isValidationError(error) ? '✅ Yes' : '❌ No'}</li>
              <li>Is Not Found Error: {isNotFoundError(error) ? '✅ Yes' : '❌ No'}</li>
              <li>Status Code: {extractErrorInfo(error).status}</li>
              <li>Error Type: {extractErrorInfo(error).type}</li>
            </ul>
          </div>
        </div>
      )}

      {/* Success Response */}
      {response && (
        <div className="mb-6">
          <h3 className="text-lg font-semibold mb-2 text-green-600">Success Response:</h3>
          <div className="bg-green-50 border border-green-200 rounded p-4">
            <p className="text-green-800">{response.message}</p>
            {response.data && (
              <pre className="mt-2 text-xs bg-green-100 p-2 rounded overflow-auto">
                {JSON.stringify(response.data, null, 2)}
              </pre>
            )}
          </div>
        </div>
      )}

      {/* Instructions */}
      <div className="bg-blue-50 border border-blue-200 rounded p-4">
        <h3 className="font-medium text-blue-800 mb-2">Testing Instructions:</h3>
        <ul className="text-sm text-blue-700 space-y-1">
          <li>• <strong>Validation Error:</strong> Tries to create category with empty name</li>
          <li>• <strong>Not Found Error:</strong> Tries to delete non-existent category</li>
          <li>• <strong>Server Error:</strong> Makes request with invalid parameters</li>
          <li>• <strong>Success Case:</strong> Creates a valid category</li>
          <li>• Open browser console to see detailed error logging</li>
        </ul>
      </div>
    </div>
  );
}