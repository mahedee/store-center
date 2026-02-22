'use client';

import React from 'react';
import { CategoryForm } from '@/features/categories/components/CategoryForm';
import { useRouter } from 'next/navigation';
import { useCategory } from '@/features/categories/hooks/useCategories';

export default function EditCategoryPage({ params }: { params: { id: string } }) {
  const router = useRouter();
  const { data: category, isLoading, error } = useCategory(params.id);

  const handleSuccess = () => {
    router.push('/categories');
  };

  const handleCancel = () => {
    router.push('/categories');
  };

  if (isLoading) {
    return (
      <div className="flex justify-center items-center h-64">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
      </div>
    );
  }

  if (error || !category) {
    return (
      <div className="space-y-6">
        <div className="flex items-center justify-between">
          <h1 className="text-3xl font-bold text-gray-900">Edit Category</h1>
        </div>
        <div className="bg-red-50 border border-red-200 rounded-md p-4">
          <p className="text-red-700">Category not found or failed to load.</p>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-3xl font-bold text-gray-900">Edit Category</h1>
      </div>
      
      <div className="bg-white shadow rounded-lg p-6">
        <CategoryForm 
          initialData={category}
          isEditMode={true}
          onSuccess={handleSuccess} 
          onCancel={handleCancel} 
        />
      </div>
    </div>
  );
}