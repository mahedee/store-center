"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { deleteCategory } from "../../api/categoryApi";
import ErrorAlert from "../Error/ErrorAlert";
import { getUserFriendlyErrorMessage } from "../../utils/errorHandling";

export default function CategoryTable({ categories, onCategoryDeleted }) {
  const router = useRouter();
  const [error, setError] = useState(null);
  const [deletingId, setDeletingId] = useState(null);

  const handleDelete = async (category) => {
    if (!window.confirm(`Are you sure you want to delete "${category.name}"?`)) {
      return;
    }

    setError(null);
    setDeletingId(category.id);

    try {
      await deleteCategory(category.id);
      
      // Notify parent component to refresh the list
      if (onCategoryDeleted) {
        onCategoryDeleted(category.id);
      }
      
      // Show success message (you could use a toast notification here)
      alert(`Category "${category.name}" deleted successfully`);
    } catch (err) {
      setError(err);
    } finally {
      setDeletingId(null);
    }
  };

  return (
    <div className="mt-4">
      <button
        onClick={() => router.push("/settings/categories/create")}
        className="bg-blue-600 text-white px-4 py-2 rounded mb-4 hover:bg-blue-700"
      >
        Create New Category
      </button>

      {/* Error display */}
      {error && <ErrorAlert error={error} className="mb-4" />}

      <table className="min-w-full bg-white rounded shadow">
        <thead>
          <tr>
            <th className="py-2 px-4 border bg-gray-50">Name</th>
            <th className="py-2 px-4 border bg-gray-50">Description</th>
            <th className="py-2 px-4 border bg-gray-50">Actions</th>
          </tr>
        </thead>
        <tbody>
          {categories.map((cat) => (
            <tr key={cat.id} className="hover:bg-gray-50">
              <td className="py-2 px-4 border">{cat.name}</td>
              <td className="py-2 px-4 border">{cat.description}</td>
              <td className="py-2 px-4 border">
                <div className="flex space-x-2">
                  <button
                    onClick={() => router.push(`/categories/edit/${cat.id}`)}
                    className="bg-yellow-400 text-white px-2 py-1 rounded hover:bg-yellow-500 focus:outline-none focus:ring-2 focus:ring-yellow-500"
                  >
                    Edit
                  </button>
                  <button
                    onClick={() => handleDelete(cat)}
                    disabled={deletingId === cat.id}
                    className="bg-red-500 text-white px-2 py-1 rounded hover:bg-red-600 focus:outline-none focus:ring-2 focus:ring-red-500 disabled:opacity-50 disabled:cursor-not-allowed"
                  >
                    {deletingId === cat.id ? 'Deleting...' : 'Delete'}
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {categories.length === 0 && (
        <div className="text-center py-8 text-gray-500">
          No categories found. <button 
            onClick={() => router.push("/settings/categories/create")}
            className="text-blue-600 hover:underline"
          >
            Create your first category
          </button>
        </div>
      )}
    </div>
  );
}
