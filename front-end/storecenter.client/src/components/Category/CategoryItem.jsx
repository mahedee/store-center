export default function CategoryItem({ category, onDelete }) {
    const handleDelete = async () => {
      // Add delete logic here
      // After delete, fetch categories again
      await deleteCategory(category.id);
      onDelete();
    };
  
    return (
      <div className="flex justify-between bg-white p-4 mb-2 border rounded">
        <div>{category.name}</div>
        <div>
          <button className="bg-blue-500 text-white px-2 py-1 rounded mr-2">Edit</button>
          <button onClick={handleDelete} className="bg-red-500 text-white px-2 py-1 rounded">
            Delete
          </button>
        </div>
      </div>
    );
  }
  
  async function deleteCategory(id) {
    // Implement the delete API call here
  }
  