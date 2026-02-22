import React from 'react';
import { CategoryTable } from '@/features/categories/components/CategoryTable';

interface CategoriesPageProps {
  searchParams: {
    page?: string;
    size?: string;
    search?: string;
  };
}

export default function CategoriesPage({ searchParams }: CategoriesPageProps) {
  // Parse searchParams to avoid Next.js warnings
  const page = searchParams.page ? parseInt(searchParams.page) : 1;
  const size = searchParams.size ? parseInt(searchParams.size) : 10;
  const search = searchParams.search;
  
  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-3xl font-bold text-gray-900">Categories</h1>
      </div>
      <CategoryTable page={page} size={size} search={search} />
    </div>
  );
}