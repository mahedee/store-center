// Base Types
export interface BaseEntity {
  id: string;
  createdAt: Date;
  updatedAt: Date;
}

// User Types
export interface User extends BaseEntity {
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
  isActive: boolean;
}

// Category Types
export interface Category extends BaseEntity {
  name: string;
  description?: string;
  parentId?: string;
  isActive: boolean;
}

// Product Types
export interface Product extends BaseEntity {
  name: string;
  description?: string;
  price: number;
  sku: string;
  categoryId: string;
  stockQuantity: number;
  isActive: boolean;
  imageUrl?: string;
}

// Order Types
export interface Order extends BaseEntity {
  orderNumber: string;
  customerId: string;
  status: string;
  totalAmount: number;
  items: OrderItem[];
}

export interface OrderItem {
  id: string;
  productId: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
}

// Customer Types
export interface Customer extends BaseEntity {
  firstName: string;
  lastName: string;
  email: string;
  phone?: string;
  address?: Address;
}

export interface Address {
  street: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
}

// Form Types
export interface LoginForm {
  email: string;
  password: string;
}

export interface RegisterForm {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
}

// State Management Types
export interface AppState {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean;
}

// Component Props Types
export interface PageProps {
  params: { [key: string]: string };
  searchParams: { [key: string]: string | string[] | undefined };
}