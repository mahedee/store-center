import apiClient from '../client';

export const categoryService = {
  // Get paginated categories
  getCategories: async (pageNumber = 1, pageSize = 10, orderBy = "Name") => {
    const response = await apiClient.get('/Categories', {
      params: { PageNumber: pageNumber, PageSize: pageSize, OrderBy: orderBy }
    });
    return response.data;
  },

  // Get category by ID
  getCategoryById: async (id) => {
    const response = await apiClient.get(`/Categories/${id}`);
    return response.data;
  },

  // Create new category
  createCategory: async (categoryData) => {
    const response = await apiClient.post('/Categories', categoryData);
    return response.data;
  },

  // Update category
  updateCategory: async (id, categoryData) => {
    const response = await apiClient.put(`/Categories/${id}`, categoryData);
    return response.data;
  },

  // Delete category
  deleteCategory: async (id) => {
    const response = await apiClient.delete(`/Categories/${id}`);
    return response.data;
  },
};