// src/api/axiosInstance.js
import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'http://localhost:5100/api', // Try HTTP first
  headers: {
    'Content-Type': 'application/json',
    Accept: '*/*',
  },
  timeout: 10000,
  // For development - ignore SSL certificate errors
  httpsAgent: process.env.NODE_ENV === 'development' ? 
    new (require('https').Agent)({ rejectUnauthorized: false }) : undefined
});

// Add request interceptor for debugging
axiosInstance.interceptors.request.use(
  (config) => {
    console.log('Making request to:', config.baseURL + config.url);
    return config;
  },
  (error) => {
    console.error('Request error:', error);
    return Promise.reject(error);
  }
);

// Add response interceptor for debugging
axiosInstance.interceptors.response.use(
  (response) => {
    console.log('Response received:', response.status, response.data);
    return response;
  },
  (error) => {
    console.error('Response error:', {
      message: error.message,
      code: error.code,
      response: error.response?.data,
      status: error.response?.status
    });
    return Promise.reject(error);
  }
);

export default axiosInstance;
