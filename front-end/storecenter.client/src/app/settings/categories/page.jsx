// app/settings/categories/page.jsx
import { fetchCategories } from "@/api/categoryApi";
import CategoryTable from "@/components/Category/CategoryTable";

export default async function CategoryPage() {
  const response = await fetchCategories(1, 10, "Name");
  const categories = response?.data?.results || [];

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Categories</h1>
      <CategoryTable categories={categories} />
    </div>
  );
}
