import { useState } from 'react';

export default function AddCategoryForm({ onAdd }) {
  const [name, setName] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    // Call API to add the category
    await addCategory(name);
    setName('');
    onAdd();
  };

  return (
    <form onSubmit={handleSubmit} className="bg-white p-4 border rounded mb-4">
      <input
        type="text"
        value={name}
        onChange={(e) => setName(e.target.value)}
        placeholder="Category Name"
        className="p-2 border rounded mb-4 w-full"
      />
      <button type="submit" className="bg-green-500 text-white px-4 py-2 rounded">
        Add Category
      </button>
    </form>
  );
}

async function addCategory(name) {
  // Implement the add category API call here
}
