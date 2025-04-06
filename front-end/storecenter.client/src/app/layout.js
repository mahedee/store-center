import './globals.css';
import Sidebar from '@/components/Sidebar';

export const metadata = {
  title: 'Store Center',
  description: 'Manage your store efficiently',
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body className="flex">
        <Sidebar />
        <main className="ml-64 w-full min-h-screen p-6 bg-gray-100">{children}</main>
      </body>
    </html>
  );
}
