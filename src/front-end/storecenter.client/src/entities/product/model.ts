import { formatCurrency } from '@/shared/utils';

// Product Domain Model
export class Product {
  constructor(
    public readonly id: string,
    public readonly name: string,
    public readonly description: string | null = null,
    public readonly price: number,
    public readonly sku: string,
    public readonly categoryId: string,
    public readonly stockQuantity: number = 0,
    public readonly isActive: boolean = true,
    public readonly imageUrl: string | null = null,
    public readonly createdAt: Date = new Date(),
    public readonly updatedAt: Date = new Date()
  ) {}

  // Factory method for creating new product
  static create(data: {
    name: string;
    description?: string;
    price: number;
    sku: string;
    categoryId: string;
    stockQuantity?: number;
    imageUrl?: string;
  }): Product {
    return new Product(
      generateId(),
      data.name.trim(),
      data.description?.trim() || null,
      data.price,
      data.sku.trim().toUpperCase(),
      data.categoryId,
      data.stockQuantity || 0,
      true,
      data.imageUrl || null
    );
  }

  // Business methods
  updatePrice(newPrice: number): Product {
    if (newPrice < 0) {
      throw new Error('Price cannot be negative');
    }

    return new Product(
      this.id,
      this.name,
      this.description,
      newPrice,
      this.sku,
      this.categoryId,
      this.stockQuantity,
      this.isActive,
      this.imageUrl,
      this.createdAt,
      new Date()
    );
  }

  updateStock(quantity: number): Product {
    if (quantity < 0) {
      throw new Error('Stock quantity cannot be negative');
    }

    return new Product(
      this.id,
      this.name,
      this.description,
      this.price,
      this.sku,
      this.categoryId,
      quantity,
      this.isActive,
      this.imageUrl,
      this.createdAt,
      new Date()
    );
  }

  addStock(quantity: number): Product {
    if (quantity <= 0) {
      throw new Error('Quantity to add must be positive');
    }

    return this.updateStock(this.stockQuantity + quantity);
  }

  removeStock(quantity: number): Product {
    if (quantity <= 0) {
      throw new Error('Quantity to remove must be positive');
    }

    const newQuantity = this.stockQuantity - quantity;
    if (newQuantity < 0) {
      throw new Error('Insufficient stock');
    }

    return this.updateStock(newQuantity);
  }

  activate(): Product {
    return new Product(
      this.id,
      this.name,
      this.description,
      this.price,
      this.sku,
      this.categoryId,
      this.stockQuantity,
      true,
      this.imageUrl,
      this.createdAt,
      new Date()
    );
  }

  deactivate(): Product {
    return new Product(
      this.id,
      this.name,
      this.description,
      this.price,
      this.sku,
      this.categoryId,
      this.stockQuantity,
      false,
      this.imageUrl,
      this.createdAt,
      new Date()
    );
  }

  // Query methods
  isInStock(): boolean {
    return this.stockQuantity > 0;
  }

  isLowStock(threshold: number = 10): boolean {
    return this.stockQuantity <= threshold && this.stockQuantity > 0;
  }

  isOutOfStock(): boolean {
    return this.stockQuantity === 0;
  }

  canBeSold(quantity: number = 1): boolean {
    return this.isActive && this.stockQuantity >= quantity;
  }

  // Display methods
  get formattedPrice(): string {
    return formatCurrency(this.price);
  }

  get displayName(): string {
    return this.name;
  }

  get stockStatus(): 'in_stock' | 'low_stock' | 'out_of_stock' {
    if (this.isOutOfStock()) return 'out_of_stock';
    if (this.isLowStock()) return 'low_stock';
    return 'in_stock';
  }

  get stockStatusLabel(): string {
    switch (this.stockStatus) {
      case 'out_of_stock':
        return 'Out of Stock';
      case 'low_stock':
        return 'Low Stock';
      case 'in_stock':
        return 'In Stock';
    }
  }

  toJSON() {
    return {
      id: this.id,
      name: this.name,
      description: this.description,
      price: this.price,
      sku: this.sku,
      categoryId: this.categoryId,
      stockQuantity: this.stockQuantity,
      isActive: this.isActive,
      imageUrl: this.imageUrl,
      createdAt: this.createdAt.toISOString(),
      updatedAt: this.updatedAt.toISOString(),
    };
  }
}

// Helper function to generate IDs
function generateId(): string {
  return Math.random().toString(36).substr(2, 9);
}