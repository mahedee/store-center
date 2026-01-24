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
        const response = await fetchCategories(1, 10, "Name");
        setCategories(response?.data?.results || []);
      } catch (err) {
        setError("Failed to load categories. Please ensure the backend server is running.");
        console.error("Error fetching categories:", err);
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
