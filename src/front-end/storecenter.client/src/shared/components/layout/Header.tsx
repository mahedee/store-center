'use client';

import React from 'react';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { UserMenu } from './UserMenu';
import { NotificationMenu } from './NotificationMenu';
import { SearchBar } from './SearchBar';

export const Header: React.FC = () => {
  const router = useRouter();

  return (
    <header className="bg-white shadow-sm border-b border-gray-200">
      <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
        <div className="flex h-16 justify-between items-center">
          {/* Left section - Logo and main navigation */}
          <div className="flex items-center">
            {/* Company Logo */}
            <div className="flex-shrink-0">
              <Link href="/" className="flex items-center">
                <div className="h-8 w-8 bg-blue-600 rounded-lg flex items-center justify-center">
                  <span className="text-white font-bold text-lg">SC</span>
                </div>
                <span className="ml-2 text-xl font-semibold text-gray-900 hidden sm:block">
                  Store Center
                </span>
              </Link>
            </div>
          </div>

          {/* Center - Search Bar */}
          <div className="flex-1 max-w-lg mx-4 hidden lg:block">
            <SearchBar placeholder="Search products, categories, orders..." />
          </div>

          {/* Right section - Search, Notifications, Profile */}
          <div className="flex items-center space-x-4">
            {/* Mobile search button */}
            <button className="lg:hidden p-2 rounded-md text-gray-400 hover:text-gray-500">
              <svg className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
              </svg>
            </button>

            {/* Notifications */}
            <NotificationMenu />

            {/* User menu */}
            <UserMenu />
          </div>
        </div>
      </div>
    </header>
  );
};