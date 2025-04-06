// src/api/categoryApi.js
import axios from './axiosInstance';

// GET categories
export const fetchCategories = (page = 1, size = 10, orderBy = 'Name') => {
  return axios.get(`/Categories`, {
    params: {
      PageNumber: page,
      PageSize: size,
      OrderBy: orderBy,
    },
  });
};

// POST create category
export const createCategory = (data) => {
  return axios.post('/Categories', data);
};

// PUT update category
export const updateCategory = (id, data) => {
  return axios.put(`/Categories/${id}`, data);
};

// DELETE category
export const deleteCategory = (id) => {
  return axios.delete(`/Categories/${id}`);
};
