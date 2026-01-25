// Example usage in page.jsx or CreateCategory.jsx
'use client';

import { createCategory } from '@/api/categoryApi';

const handleCreate = async () => {
  try {
    const response = await createCategory({
      name: 'Test Category',
      description: 'Test Category Description',
    });
    console.log('Created:', response.data);
  } catch (error) {
    console.error('Failed to create category:', error);
  }
};
