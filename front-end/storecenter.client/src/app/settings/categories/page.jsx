import { fetchCategories } from "@/utils/api";
import CategoryTable from "@/components/Category/CategoryTable";

export default async function CategoryPage() {
  const data = await fetchCategories();
  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Categories</h1>
      <CategoryTable categories={data.results} />
    </div>
  );
}
