import React from 'react';
import { ProductCatalog } from '@/widgets/ProductCatalog';

export default function ProductsPage() {
  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-3xl font-bold text-gray-900">Products</h1>
      </div>
      <ProductCatalog />
    </div>
  );
}