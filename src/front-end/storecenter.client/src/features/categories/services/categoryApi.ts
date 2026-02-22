import { apiClient } from '@/shared/api/client';
import { categoriesApi } from '@/shared/api/categories';
import { ApiResponse, PaginatedResponse } from '@/shared/api/types';
import { Category, CreateCategoryRequest, UpdateCategoryRequest, CategoryListParams } from '../types';

export const categoryService = {
  // Get all categories with pagination
  getCategories: async (params: CategoryListParams = {}): Promise<{ categories: Category[], pagination: { totalPages: number, totalRecords: number, currentPage: number, pageSize: number } }> => {
    const response = await categoriesApi.getAll(params);
    return {
      categories: response.results,
      pagination: {
        totalPages: response.totalPages,
        totalRecords: response.count,
        currentPage: response.currentPage,
        pageSize: response.pageSize
      }
    };
  },

  // Get category by ID
  getCategoryById: async (id: string): Promise<Category> => {
    return await categoriesApi.getById(id);
  },

  // Create new category
  createCategory: async (categoryData: CreateCategoryRequest): Promise<Category> => {
    return await categoriesApi.create(categoryData);
  },

  // Update category
  updateCategory: async (id: string, categoryData: UpdateCategoryRequest): Promise<Category> => {
    return await categoriesApi.update(id, categoryData);
  },

  // Delete category
  deleteCategory: async (id: string): Promise<void> => {
    return await categoriesApi.delete(id);
  }
};

export default categoryService;