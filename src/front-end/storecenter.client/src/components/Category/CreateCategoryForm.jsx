'use client';

// ENHANCED ERROR HANDLING:
// - Uses RFC 7807 compliant error responses from backend
// - Displays field-specific validation errors inline
// - Shows user-friendly error messages via ErrorAlert component
// - Handles different error types (validation, network, server errors)

import { useState } from 'react';
import { useRouter } from 'next/navigation';
import { createCategory } from '../../api/categoryApi';
import ErrorAlert from '../Error/ErrorAlert';
import { isValidationError, extractErrorInfo, formatValidationErrors } from '../../utils/errorHandling';

export default function CreateCategoryForm() {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [error, setError] = useState(null);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const router = useRouter();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);
    setIsSubmitting(true);

    try {
      await createCategory({ name, description });
      router.push('/settings/categories');
    } catch (err) {
      setError(err);
    } finally {
      setIsSubmitting(false);
    }
  };

  // Extract field-specific errors for inline display
  const fieldErrors = error ? extractValidationErrorsForFields(error) : {};

  return (
    <form onSubmit={handleSubmit} className="max-w-md mx-auto p-4 bg-white rounded shadow">
      <h2 className="text-xl font-bold mb-4">Create New Category</h2>
      
      {/* Global error display */}
      {error && <ErrorAlert error={error} className="mb-4" />}

      <div className="mb-4">
        <label className="block font-medium mb-1">Name</label>
        <input
          type="text"
          className={`w-full border px-3 py-2 rounded ${
            fieldErrors.Name ? 'border-red-500 focus:border-red-500' : 'border-gray-300 focus:border-blue-500'
          }`}
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
          disabled={isSubmitting}
        />
        {fieldErrors.Name && (
          <p className="text-red-500 text-sm mt-1">{fieldErrors.Name[0]}</p>
        )}
      </div>

      <div className="mb-4">
        <label className="block font-medium mb-1">Description</label>
        <textarea
          className={`w-full border px-3 py-2 rounded ${
            fieldErrors.Description ? 'border-red-500 focus:border-red-500' : 'border-gray-300 focus:border-blue-500'
          }`}
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          disabled={isSubmitting}
        />
        {fieldErrors.Description && (
          <p className="text-red-500 text-sm mt-1">{fieldErrors.Description[0]}</p>
        )}
      </div>

      <button 
        type="submit" 
        disabled={isSubmitting}
        className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed"
      >
        {isSubmitting ? 'Creating...' : 'Create Category'}
      </button>
    </form>
  );
}

// Helper function to extract field-specific validation errors
function extractValidationErrorsForFields(error) {
  if (!isValidationError(error)) {
    return {};
  }

  const errorInfo = extractErrorInfo(error);
  return errorInfo.errors || {};
}
