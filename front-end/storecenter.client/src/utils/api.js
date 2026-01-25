// Legacy API utilities - use utils/api/services instead
// This file is kept for backward compatibility but should be replaced

// const API_URL = 'http://your-api-url'; // Replace with your actual API URL

// export async function getCategories() {
//   const res = await fetch(`${API_URL}/categories`);
//   return res.json();
// }

// export async function addCategory(name) {
//   const res = await fetch(`${API_URL}/categories`, {
//     method: 'POST',
//     body: JSON.stringify({ name }),
//     headers: { 'Content-Type': 'application/json' },
//   });
//   return res.json();
// }

// export async function deleteCategory(id) {
//   const res = await fetch(`${API_URL}/categories/${id}`, { method: 'DELETE' });
//   return res.json();
// }


// DEPRECATED - Use categoryService.getCategories() instead
export async function fetchCategories(pageNumber = 1, pageSize = 10, orderBy = "Name") {
    const res = await fetch(
      `https://localhost:5001/api/Categories?PageNumber=${pageNumber}&PageSize=${pageSize}&OrderBy=${orderBy}`,
      { cache: "no-store" } // prevent caching for SSR
    );
    if (!res.ok) throw new Error("Failed to fetch categories");
    return res.json();
  }
  