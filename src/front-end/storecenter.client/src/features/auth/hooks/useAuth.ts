import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { queryKeys } from '@/shared/api/queries';
import { authService } from '../services/authApi';
import { LoginRequest, RegisterRequest, AuthResponse, User } from '../types';
import { STORAGE_KEYS } from '@/shared/constants';

// Auth hook for managing authentication state
export const useAuth = () => {
  const queryClient = useQueryClient();

  // Get current user
  const { data: user, isLoading: isUserLoading } = useQuery({
    queryKey: queryKeys.user(),
    queryFn: authService.getProfile,
    enabled: !!getStoredToken(),
    retry: false,
  });

  // Login mutation
  const loginMutation = useMutation({
    mutationFn: (credentials: LoginRequest) => authService.login(credentials),
    onSuccess: (response: AuthResponse) => {
      // Store token
      localStorage.setItem(STORAGE_KEYS.AUTH_TOKEN, response.token);
      
      // Update user cache
      queryClient.setQueryData(queryKeys.user(), response.user);
    },
  });

  // Register mutation
  const registerMutation = useMutation({
    mutationFn: (userData: RegisterRequest) => authService.register(userData),
    onSuccess: (response: AuthResponse) => {
      // Store token
      localStorage.setItem(STORAGE_KEYS.AUTH_TOKEN, response.token);
      
      // Update user cache
      queryClient.setQueryData(queryKeys.user(), response.user);
    },
  });

  // Logout mutation
  const logoutMutation = useMutation({
    mutationFn: authService.logout,
    onSuccess: () => {
      // Clear token
      localStorage.removeItem(STORAGE_KEYS.AUTH_TOKEN);
      
      // Clear all cached data
      queryClient.clear();
    },
  });

  // Helper functions
  const login = async (credentials: LoginRequest) => {
    const result = await loginMutation.mutateAsync(credentials);
    return result;
  };

  const register = async (userData: RegisterRequest) => {
    const result = await registerMutation.mutateAsync(userData);
    return result;
  };

  const logout = async () => {
    await logoutMutation.mutateAsync();
  };

  return {
    user: user?.data,
    isAuthenticated: !!user?.data && !!getStoredToken(),
    isLoading: isUserLoading || loginMutation.isPending || registerMutation.isPending,
    login,
    register,
    logout,
    loginError: loginMutation.error,
    registerError: registerMutation.error,
  };
};

// Helper function to get stored token
const getStoredToken = (): string | null => {
  if (typeof window === 'undefined') return null;
  return localStorage.getItem(STORAGE_KEYS.AUTH_TOKEN);
};