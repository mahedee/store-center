import { UseQueryOptions, UseMutationOptions } from '@tanstack/react-query';
import { ApiResponse, PaginatedResponse, ApiError } from './types';

// Query Keys Factory
export const queryKeys = {
  all: ['api'] as const,
  
  // Categories
  categories: () => [...queryKeys.all, 'categories'] as const,
  category: (id: string) => [...queryKeys.categories(), id] as const,
  
  // Products
  products: () => [...queryKeys.all, 'products'] as const,
  product: (id: string) => [...queryKeys.products(), id] as const,
  
  // Orders
  orders: () => [...queryKeys.all, 'orders'] as const,
  order: (id: string) => [...queryKeys.orders(), id] as const,
  
  // Customers
  customers: () => [...queryKeys.all, 'customers'] as const,
  customer: (id: string) => [...queryKeys.customers(), id] as const,
  
  // Auth
  auth: () => [...queryKeys.all, 'auth'] as const,
  user: () => [...queryKeys.auth(), 'user'] as const,
};

// Default Query Options
export const defaultQueryOptions: Omit<UseQueryOptions, 'queryKey' | 'queryFn'> = {
  staleTime: 5 * 60 * 1000, // 5 minutes
  retry: (failureCount, error: any) => {
    // Don't retry on 4xx errors except 408, 429
    if (error?.response?.status >= 400 && error?.response?.status < 500) {
      return error?.response?.status === 408 || error?.response?.status === 429;
    }
    return failureCount < 3;
  },
  refetchOnWindowFocus: false,
};

// Default Mutation Options
export const defaultMutationOptions: UseMutationOptions = {
  retry: (failureCount, error: any) => {
    // Don't retry on 4xx errors except 408, 429
    if (error?.response?.status >= 400 && error?.response?.status < 500) {
      return false;
    }
    return failureCount < 2;
  },
};

// Error Handler
export const handleQueryError = (error: unknown): ApiError => {
  if (error && typeof error === 'object' && 'response' in error) {
    const axiosError = error as any;
    return {
      message: axiosError.response?.data?.message || axiosError.message || 'An error occurred',
      errors: axiosError.response?.data?.errors,
      statusCode: axiosError.response?.status || 500,
      traceId: axiosError.response?.data?.traceId,
    };
  }
  
  return {
    message: error instanceof Error ? error.message : 'An unknown error occurred',
    statusCode: 500,
  };
};