import React from 'react';
import { MainLayout } from '@/shared/components/layout';
import { QueryProvider } from '@/shared/components/QueryProvider';
import './globals.css';

interface Metadata {
  title: string;
  description: string;
}

export const metadata: Metadata = {
  title: 'Store Center',
  description: 'Manage your store efficiently',
};

interface RootLayoutProps {
  children: React.ReactNode;
}

export default function RootLayout({ children }: RootLayoutProps) {
  return (
    <html lang="en">
      <body>
        <QueryProvider>
          <MainLayout>
            {children}
          </MainLayout>
        </QueryProvider>
      </body>
    </html>
  );
}