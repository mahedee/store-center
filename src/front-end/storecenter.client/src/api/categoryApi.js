// src/api/categoryApi.js (Legacy - use utils/api/services/categoryService.js instead)
import apiClient from './axiosInstance';

// GET categories
export const fetchCategories = (page = 1, size = 10, orderBy = 'Name') => {
  return apiClient.get(`/Categories`, {
    params: {
      PageNumber: page,
      PageSize: size,
      OrderBy: orderBy,
    },
  });
};

// POST create category
export const createCategory = (data) => {
  return apiClient.post('/Categories', data);
};

// PUT update category
export const updateCategory = (id, data) => {
  return apiClient.put(`/Categories/${id}`, data);
};

// DELETE category
export const deleteCategory = (id) => {
  return apiClient.delete(`/Categories/${id}`);
};
