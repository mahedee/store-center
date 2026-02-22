import React from 'react';
import { OrderSummary } from '@/widgets/OrderSummary';

export default function OrdersPage() {
  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-3xl font-bold text-gray-900">Orders</h1>
      </div>
      <OrderSummary />
    </div>
  );
}