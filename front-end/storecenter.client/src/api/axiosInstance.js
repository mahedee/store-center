// src/api/axiosInstance.js
import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'http://localhost:5100/api', // Centralized base URL
  headers: {
    'Content-Type': 'application/json',
    Accept: '*/*',
  },
  timeout: 10000,
});

export default axiosInstance;
