// src/api/axiosInstance.js (Legacy - use utils/api/client.js instead)
import axios from 'axios';
import API_CONFIG from '../utils/api/config';

// Create axios instance
const apiClient = axios.create({
  baseURL: API_CONFIG.BASE_URL,
  timeout: API_CONFIG.TIMEOUT,
  headers: API_CONFIG.DEFAULT_HEADERS,
});

// Request interceptor
apiClient.interceptors.request.use(
  (config) => {
    // Add auth token if available
    const token = typeof window !== 'undefined' ? localStorage.getItem('authToken') : null;
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    
    // Log request in development
    if (process.env.NODE_ENV === 'development') {
      console.log('API Request:', config.method?.toUpperCase(), config.url, config.data);
    }
    
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor
apiClient.interceptors.response.use(
  (response) => {
    // Log response in development
    if (process.env.NODE_ENV === 'development') {
      console.log('API Response:', response.status, response.data);
    }
    return response;
  },
  (error) => {
    // Handle new RFC 7807 error responses
    if (error.response?.data) {
      const errorData = error.response.data;
      
      // Check if it's the new error format (RFC 7807)
      if (errorData.type && errorData.title && errorData.status && errorData.detail) {
        // Transform the error for consistent handling
        const transformedError = {
          type: errorData.type,
          title: errorData.title,
          status: errorData.status,
          detail: errorData.detail,
          instance: errorData.instance,
          traceId: errorData.traceId,
          errors: errorData.errors || {},
          isRfc7807Error: true
        };
        
        // Attach transformed error to the error object
        error.rfc7807Error = transformedError;
        
        // Handle common status codes
        if (error.response.status === 401) {
          // Handle unauthorized - redirect to login
          if (typeof window !== 'undefined') {
            localStorage.removeItem('authToken');
            window.location.href = '/login';
          }
        }
        
        // Log structured error info in development
        if (process.env.NODE_ENV === 'development') {
          console.error('API Error (RFC 7807):', transformedError);
        }
      } else {
        // Handle legacy error format
        if (error.response?.status === 500) {
          console.error('Server Error:', error.response.data);
        }
      }
    }
    
    // Log error in development
    if (process.env.NODE_ENV === 'development') {
      console.error('API Error:', error);
    }
    
    return Promise.reject(error);
  }
);

export default apiClient;
