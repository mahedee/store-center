import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { queryKeys, defaultQueryOptions, defaultMutationOptions } from '@/shared/api/queries';
import { categoryService } from '../services/categoryApi';
import { Category, CategoryListParams, CreateCategoryRequest, UpdateCategoryRequest } from '../types';

// Get categories
export const useCategories = (params: CategoryListParams = {}) => {
  return useQuery({
    queryKey: [...queryKeys.categories(), params],
    queryFn: () => categoryService.getCategories(params),
    ...defaultQueryOptions,
  });
};

// Get single category
export const useCategory = (id: string) => {
  return useQuery({
    queryKey: queryKeys.category(id),
    queryFn: () => categoryService.getCategoryById(id),
    enabled: !!id,
    ...defaultQueryOptions,
  });
};

// Category mutations
export const useCategoryMutations = () => {
  const queryClient = useQueryClient();

  const createCategory = useMutation({
    mutationFn: (data: CreateCategoryRequest) => categoryService.createCategory(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: queryKeys.categories() });
    },
    ...defaultMutationOptions,
  });

  const updateCategory = useMutation({
    mutationFn: ({ id, data }: { id: string; data: UpdateCategoryRequest }) => 
      categoryService.updateCategory(id, data),
    onSuccess: (_, { id }) => {
      queryClient.invalidateQueries({ queryKey: queryKeys.categories() });
      queryClient.invalidateQueries({ queryKey: queryKeys.category(id) });
    },
    ...defaultMutationOptions,
  });

  const deleteCategory = useMutation({
    mutationFn: (id: string) => categoryService.deleteCategory(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: queryKeys.categories() });
    },
    ...defaultMutationOptions,
  });

  return {
    createCategory,
    updateCategory,
    deleteCategory,
  };
};