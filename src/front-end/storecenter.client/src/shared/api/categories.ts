import { apiClient } from './client';
import { 
  Category, 
  CreateCategoryRequest, 
  UpdateCategoryRequest, 
  PaginationParams 
} from './types';

// Backend response format
interface BackendPaginatedResponse<T> {
  currentPage: number;
  pageSize: number;
  count: number;
  totalPages: number;
  hasPrevious: boolean;
  hasNext: boolean;
  success: boolean;
  errors: string[];
  results: T[];
}

// Categories API endpoints
const CATEGORIES_BASE = '/categories';

export const categoriesApi = {
  // Get all categories (paginated)
  getAll: async (params?: PaginationParams): Promise<BackendPaginatedResponse<Category>> => {
    const response = await apiClient.get<BackendPaginatedResponse<Category>>(CATEGORIES_BASE, { params });
    return response.data;
  },

  // Get category by ID
  getById: async (id: string): Promise<Category> => {
    const response = await apiClient.get<Category>(`${CATEGORIES_BASE}/${id}`);
    return response.data;
  },

  // Create new category
  create: async (data: CreateCategoryRequest): Promise<Category> => {
    const response = await apiClient.post<Category>(CATEGORIES_BASE, data);
    return response.data;
  },

  // Update category
  update: async (id: string, data: UpdateCategoryRequest): Promise<Category> => {
    const response = await apiClient.put<Category>(`${CATEGORIES_BASE}/${id}`, data);
    return response.data;
  },

  // Delete category
  delete: async (id: string): Promise<void> => {
    await apiClient.delete(`${CATEGORIES_BASE}/${id}`);
  },

  // Search categories
  search: async (searchTerm: string, params?: PaginationParams): Promise<Category[]> => {
    const response = await apiClient.get<BackendPaginatedResponse<Category>>(CATEGORIES_BASE, {
      params: { ...params, searchTerm }
    });
    return response.data.results;
  }
};

export default categoriesApi;