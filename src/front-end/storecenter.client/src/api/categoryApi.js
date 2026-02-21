// src/api/categoryApi.js (Legacy - use utils/api/services/categoryService.js instead)
import apiClient from './axiosInstance';
import { extractErrorInfo, getUserFriendlyErrorMessage } from '../utils/errorHandling';

// GET categories
export const fetchCategories = async (page = 1, size = 10, orderBy = 'Name') => {
  try {
    const response = await apiClient.get(`/Categories`, {
      params: {
        PageNumber: page,
        PageSize: size,
        OrderBy: orderBy,
      },
    });
    return response.data;
  } catch (error) {
    // Re-throw with enhanced error information
    const errorInfo = extractErrorInfo(error);
    throw new Error(getUserFriendlyErrorMessage(error));
  }
};

// POST create category
export const createCategory = async (data) => {
  try {
    const response = await apiClient.post('/Categories', data);
    return response.data;
  } catch (error) {
    // For validation errors, preserve the full error object
    if (error.rfc7807Error?.errors) {
      throw error; // Let the component handle validation errors
    }
    
    const errorInfo = extractErrorInfo(error);
    throw new Error(getUserFriendlyErrorMessage(error));
  }
};

// PUT update category
export const updateCategory = async (id, data) => {
  try {
    const response = await apiClient.put(`/Categories/${id}`, data);
    return response.data;
  } catch (error) {
    // For validation errors, preserve the full error object
    if (error.rfc7807Error?.errors) {
      throw error; // Let the component handle validation errors
    }
    
    const errorInfo = extractErrorInfo(error);
    throw new Error(getUserFriendlyErrorMessage(error));
  }
};

// DELETE category
export const deleteCategory = async (id) => {
  try {
    const response = await apiClient.delete(`/Categories/${id}`);
    return response.data;
  } catch (error) {
    const errorInfo = extractErrorInfo(error);
    throw new Error(getUserFriendlyErrorMessage(error));
  }
};
