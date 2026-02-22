// Re-export types from shared API types
export type { Category, CreateCategoryRequest, UpdateCategoryRequest } from '@/shared/api/types';

// Feature-specific types
export interface CategoryListParams {
  pageNumber?: number;
  pageSize?: number;
  orderBy?: string;
  searchTerm?: string;
}

export interface CategoryFormData {
  name: string;
  description?: string;
}

export interface CategoryTableColumn {
  key: keyof Category | 'actions';
  title: string;
  sortable?: boolean;
}