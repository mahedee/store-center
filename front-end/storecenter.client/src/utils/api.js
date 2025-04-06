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


export async function fetchCategories(pageNumber = 1, pageSize = 10, orderBy = "Name") {
    const res = await fetch(
      `http://localhost:5100/api/Categories?PageNumber=${pageNumber}&PageSize=${pageSize}&OrderBy=${orderBy}`,
      { cache: "no-store" } // prevent caching for SSR
    );
    if (!res.ok) throw new Error("Failed to fetch categories");
    return res.json();
  }
  