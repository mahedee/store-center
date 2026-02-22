'use client';

import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { Button } from '@/shared/components/ui/Button';
import { Input } from '@/shared/components/ui/Input';
import { useCategoryMutations } from '../hooks/useCategories';

interface CategoryFormProps {
  onSuccess?: () => void;
  onCancel?: () => void;
}

export const CategoryForm: React.FC<CategoryFormProps> = ({ 
  onSuccess, 
  onCancel 
}) => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [errors, setErrors] = useState<Record<string, string>>({});
  
  const router = useRouter();
  const { createCategory } = useCategoryMutations();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setErrors({});

    // Basic validation
    const newErrors: Record<string, string> = {};
    if (!name.trim()) {
      newErrors.name = 'Name is required';
    }

    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);
      return;
    }

    try {
      await createCategory.mutateAsync({
        name: name.trim(),
        description: description.trim() || undefined,
      });

      if (onSuccess) {
        onSuccess();
      } else {
        router.push('/categories');
      }
    } catch (error: any) {
      // Handle validation errors from API
      if (error.response?.data?.errors) {
        const apiErrors: Record<string, string> = {};
        for (const [field, messages] of Object.entries(error.response.data.errors)) {
          apiErrors[field.toLowerCase()] = (messages as string[])[0];
        }
        setErrors(apiErrors);
      } else {
        setErrors({ general: error.message || 'An error occurred' });
      }
    }
  };

  const handleCancel = () => {
    if (onCancel) {
      onCancel();
    } else {
      router.back();
    }
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div className="bg-white p-6 rounded-lg shadow">
        <h2 className="text-xl font-bold mb-4">Create New Category</h2>
        
        {errors.general && (
          <div className="mb-4 p-3 bg-red-50 border border-red-200 rounded text-red-700">
            {errors.general}
          </div>
        )}

        <div className="space-y-4">
          <Input
            label="Name"
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            error={errors.name}
            placeholder="Enter category name"
            required
          />

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Description
            </label>
            <textarea
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="Enter category description (optional)"
              className="block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
              rows={3}
            />
            {errors.description && (
              <p className="mt-1 text-sm text-red-600">{errors.description}</p>
            )}
          </div>

          <div className="flex justify-end space-x-3">
            <Button
              type="button"
              variant="outline"
              onClick={handleCancel}
              disabled={createCategory.isPending}
            >
              Cancel
            </Button>
            <Button
              type="submit"
              disabled={createCategory.isPending}
            >
              {createCategory.isPending ? 'Creating...' : 'Create Category'}
            </Button>
          </div>
        </div>
      </div>
    </form>
  );
};