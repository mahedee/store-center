// app/settings/categories/page.jsx
"use client";

import { useState, useEffect } from "react";
import { fetchCategories } from "@/api/categoryApi";
import CategoryTable from "@/components/Category/CategoryTable";

export default function CategoryPage() {
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const loadCategories = async () => {
      try {
        console.log('Attempting to fetch categories...');
        const response = await fetchCategories(1, 10, "Name");
        setCategories(response?.data?.results || []);
      } catch (err) {
        console.error("Detailed error:", err);
        let errorMessage = "Failed to load categories. ";
        
        if (err.code === 'NETWORK_ERROR' || err.message === 'Network Error') {
          errorMessage += "Backend server appears to be unreachable. Please check if the API server is running on http://localhost:5100";
        } else if (err.response?.status === 404) {
          errorMessage += "API endpoint not found. Check if the Categories endpoint exists.";
        } else if (err.response?.status >= 500) {
          errorMessage += "Server error occurred.";
        } else if (err.name === 'TypeError' && err.message.includes('Failed to fetch')) {
          errorMessage += "CORS error or server not responding. Check backend CORS configuration.";
        } else {
          errorMessage += `Error: ${err.message}`;
        }
        
        setError(errorMessage);
      } finally {
        setLoading(false);
      }
    };

    loadCategories();
  }, []);

  if (loading) return <div className="p-6">Loading categories...</div>;
  if (error) return <div className="p-6 text-red-500">{error}</div>;

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Categories</h1>
      <CategoryTable categories={categories} />
    </div>
  );
}
