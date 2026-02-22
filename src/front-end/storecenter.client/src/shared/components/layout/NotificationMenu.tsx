'use client';

import React, { useState } from 'react';

interface NotificationMenuProps {}

export const NotificationMenu: React.FC<NotificationMenuProps> = () => {
  const [isOpen, setIsOpen] = useState(false);
  const [notifications] = useState([
    { id: 1, text: 'New order received', time: '2 minutes ago' },
    { id: 2, text: 'Product stock low', time: '1 hour ago' },
  ]);

  return (
    <div className="relative">
      <button
        onClick={() => setIsOpen(!isOpen)}
        className="p-2 text-gray-400 hover:text-gray-500 relative"
      >
        <svg className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth={2}
            d="M15 17h5l-5 5v-5zM4 4h1v16H4V4zm13.426 2.097c.59.59.59 1.538 0 2.128L9.97 16.681c-.59.59-1.538.59-2.128 0-.59-.59-.59-1.538 0-2.128L16.298 6.097c.59-.59 1.538-.59 2.128 0z"
          />
        </svg>
        {notifications.length > 0 && (
          <span className="absolute -top-1 -right-1 h-4 w-4 bg-red-500 text-white text-xs rounded-full flex items-center justify-center">
            {notifications.length}
          </span>
        )}
      </button>

      {isOpen && (
        <div className="absolute right-0 mt-2 w-80 bg-white rounded-md shadow-lg py-1 z-50">
          <div className="px-4 py-2 text-sm font-medium text-gray-900 border-b">
            Notifications
          </div>
          {notifications.length > 0 ? (
            notifications.map((notification) => (
              <div key={notification.id} className="px-4 py-3 border-b last:border-b-0">
                <p className="text-sm text-gray-900">{notification.text}</p>
                <p className="text-xs text-gray-500 mt-1">{notification.time}</p>
              </div>
            ))
          ) : (
            <div className="px-4 py-3 text-sm text-gray-500">
              No new notifications
            </div>
          )}
        </div>
      )}
    </div>
  );
};