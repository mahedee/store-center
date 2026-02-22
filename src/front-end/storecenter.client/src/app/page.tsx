import React from 'react';
import { DashboardStats } from '@/widgets/DashboardStats';

export default function Home() {
  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-3xl font-bold text-gray-900">Welcome to Store Center</h1>
      </div>
      <DashboardStats />
    </div>
  );
}