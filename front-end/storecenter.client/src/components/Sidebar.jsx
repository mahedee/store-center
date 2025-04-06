'use client';

import { useState } from 'react';
import Link from 'next/link';

const Sidebar = () => {
  const [openMenus, setOpenMenus] = useState({});

  const toggleMenu = (menu) => {
    setOpenMenus((prev) => ({
      ...prev,
      [menu]: !prev[menu],
    }));
  };

  return (
    <aside className="w-64 h-screen bg-gray-900 text-white fixed left-0 top-0 overflow-y-auto shadow-lg">
      <div className="p-4 text-xl font-bold border-b border-gray-700">
        Store Center
      </div>
      <nav className="p-4">
        <ul className="space-y-2">
          <li>
            <button
              onClick={() => toggleMenu('settings')}
              className="w-full text-left px-2 py-2 hover:bg-gray-700 rounded"
            >
              Settings
            </button>
            {openMenus.settings && (
              <ul className="ml-4 mt-1 space-y-1">
                <li>
                  <Link href="/settings/categories" className="block px-2 py-1 hover:bg-gray-700 rounded">
                    Categories
                  </Link>
                </li>
              </ul>
            )}
          </li>

          <li>
            <button
              onClick={() => toggleMenu('reports')}
              className="w-full text-left px-2 py-2 hover:bg-gray-700 rounded"
            >
              Reports
            </button>
            {openMenus.reports && (
              <ul className="ml-4 mt-1 space-y-1">
                <li>
                  <button
                    onClick={() => toggleMenu('inventory')}
                    className="w-full text-left px-2 py-1 hover:bg-gray-700 rounded"
                  >
                    Inventory
                  </button>
                  {openMenus.inventory && (
                    <ul className="ml-4 mt-1 space-y-1">
                      <li>
                        <Link href="/reports/inventory/stock-report" className="block px-2 py-1 hover:bg-gray-700 rounded">
                          Stock Report
                        </Link>
                      </li>
                    </ul>
                  )}
                </li>
              </ul>
            )}
          </li>
        </ul>
      </nav>
    </aside>
  );
};

export default Sidebar;
