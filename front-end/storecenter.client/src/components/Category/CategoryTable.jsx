"use client";

import { useRouter } from "next/navigation";

export default function CategoryTable({ categories }) {
  const router = useRouter();

  return (
    <div className="mt-4">
      <button
        onClick={() => router.push("/categories/create")}
        className="bg-blue-600 text-white px-4 py-2 rounded mb-4"
      >
        Create New Category
      </button>
      <table className="min-w-full bg-white rounded shadow">
        <thead>
          <tr>
            <th className="py-2 px-4 border">Name</th>
            <th className="py-2 px-4 border">Description</th>
            <th className="py-2 px-4 border">Actions</th>
          </tr>
        </thead>
        <tbody>
          {categories.map((cat) => (
            <tr key={cat.id}>
              <td className="py-2 px-4 border">{cat.name}</td>
              <td className="py-2 px-4 border">{cat.description}</td>
              <td className="py-2 px-4 border flex space-x-2">
                <button
                  onClick={() => router.push(`/categories/edit/${cat.id}`)}
                  className="bg-yellow-400 text-white px-2 py-1 rounded"
                >
                  Edit
                </button>
                <button
                  onClick={() => alert(`Delete ${cat.name}`)} // Later replace with actual logic
                  className="bg-red-500 text-white px-2 py-1 rounded"
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
