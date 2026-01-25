import { MainLayout } from '@/components/Layout';
import './globals.css';

export const metadata = {
  title: 'Store Center',
  description: 'Manage your store efficiently',
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>
        <MainLayout>
          {children}
        </MainLayout>
      </body>
    </html>
  );
}
