'use client';

import React from 'react';
import { CategoryForm } from '@/features/categories/components/CategoryForm';
import { useRouter } from 'next/navigation';

export default function CreateCategoryPage() {
  const router = useRouter();

  const handleSuccess = () => {
    router.push('/categories');
  };

  const handleCancel = () => {
    router.push('/categories');
  };

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-3xl font-bold text-gray-900">Create New Category</h1>
      </div>
      
      <div className="bg-white shadow rounded-lg p-6">
        <CategoryForm onSuccess={handleSuccess} onCancel={handleCancel} />
      </div>
    </div>
  );
}