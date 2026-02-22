'use client';

import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { Button } from '@/shared/components/ui/Button';
import { useCategories, useCategoryMutations } from '../hooks/useCategories';
import { Category } from '../types';
import { formatDate } from '@/shared/utils';

interface CategoryTableProps {
  page?: number;
  size?: number; 
  search?: string;
}

export const CategoryTable: React.FC<CategoryTableProps> = ({ 
  page = 1, 
  size = 10, 
  search 
}) => {
  const router = useRouter();
  const [deletingIds, setDeletingIds] = useState<Set<string>>(new Set());

  const { data: categoriesData, isLoading, error } = useCategories({
    pageNumber: page,
    pageSize: size,
    searchTerm: search,
  });

  const { deleteCategory } = useCategoryMutations();

  const handleDelete = async (category: Category) => {
    if (!window.confirm(`Are you sure you want to delete "${category.name}"?`)) {
      return;
    }

    setDeletingIds(prev => new Set(prev).add(category.id));

    try {
      await deleteCategory.mutateAsync(category.id);
    } catch (error) {
      console.error('Failed to delete category:', error);
    } finally {
      setDeletingIds(prev => {
        const newSet = new Set(prev);
        newSet.delete(category.id);
        return newSet;
      });
    }
  };

  const handleEdit = (category: Category) => {
    router.push(`/categories/edit/${category.id}`);
  };

  const handleCreateNew = () => {
    router.push('/categories/create');
  };

  if (isLoading) {
    return (
      <div className="flex justify-center items-center h-64">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
      </div>
    );
  }

  if (error) {
    console.error('Categories error:', error);
    return (
      <div className="bg-red-50 border border-red-200 rounded-md p-4">
        <p className="text-red-700">Failed to load categories. Please try again.</p>
        {process.env.NODE_ENV === 'development' && (
          <pre className="text-xs text-red-600 mt-2 overflow-auto">
            {JSON.stringify(error, null, 2)}
          </pre>
        )}
      </div>
    );
  }

  // Extract categories and pagination from response
  const categories = categoriesData?.categories || [];
  const pagination = categoriesData?.pagination;

  return (
    <div className="space-y-4">
      {/* Header */}
      <div className="flex justify-between items-center">
        <h2 className="text-2xl font-bold text-gray-900">Categories</h2>
        <Button onClick={handleCreateNew}>
          Create New Category
        </Button>
      </div>

      {/* Table */}
      <div className="bg-white shadow rounded-lg overflow-hidden">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Name
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Description
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Status
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Created
              </th>
              <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                Actions
              </th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {categories.length === 0 ? (
              <tr>
                <td colSpan={5} className="px-6 py-4 text-center text-gray-500">
                  No categories found
                </td>
              </tr>
            ) : (
              categories.map((category) => (
                <tr key={category.id} className="hover:bg-gray-50">
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="text-sm font-medium text-gray-900">
                      {category.name}
                    </div>
                  </td>
                  <td className="px-6 py-4">
                    <div className="text-sm text-gray-900">
                      {category.description || '-'}
                    </div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${
                      !category.isDeleted 
                        ? 'bg-green-100 text-green-800' 
                        : 'bg-red-100 text-red-800'
                    }`}>
                      {!category.isDeleted ? 'Active' : 'Deleted'}
                    </span>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                    {formatDate(category.createdAt)}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                    <div className="flex justify-end space-x-2">
                      <Button
                        size="sm"
                        variant="outline"
                        onClick={() => handleEdit(category)}
                      >
                        Edit
                      </Button>
                      <Button
                        size="sm"
                        variant="danger"
                        onClick={() => handleDelete(category)}
                        disabled={deletingIds.has(category.id)}
                      >
                        {deletingIds.has(category.id) ? 'Deleting...' : 'Delete'}
                      </Button>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      {/* Pagination */}
      {pagination && pagination.totalPages > 1 && (
        <div className="flex items-center justify-between">
          <div className="text-sm text-gray-700">
            Showing {((page - 1) * size) + 1} to {Math.min(page * size, pagination.totalRecords)} of {pagination.totalRecords} results
          </div>
          <div className="flex space-x-2">
            {page > 1 && (
              <Button
                variant="outline"
                onClick={() => router.push(`/categories?page=${page - 1}&size=${size}${search ? `&search=${search}` : ''}`)}
              >
                Previous
              </Button>
            )}
            {page < pagination.totalPages && (
              <Button
                variant="outline"
                onClick={() => router.push(`/categories?page=${page + 1}&size=${size}${search ? `&search=${search}` : ''}`)}
              >
                Next
              </Button>
            )}
          </div>
        </div>
      )}
    </div>
  );
};