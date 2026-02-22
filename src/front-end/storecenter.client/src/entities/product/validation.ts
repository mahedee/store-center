// Product Validation Rules
export class ProductValidationError extends Error {
  constructor(
    message: string,
    public readonly field: string,
    public readonly code: string
  ) {
    super(message);
    this.name = 'ProductValidationError';
  }
}

export class ProductValidator {
  static readonly NAME_MIN_LENGTH = 1;
  static readonly NAME_MAX_LENGTH = 200;
  static readonly DESCRIPTION_MAX_LENGTH = 1000;
  static readonly SKU_MIN_LENGTH = 3;
  static readonly SKU_MAX_LENGTH = 50;
  static readonly MIN_PRICE = 0.01;
  static readonly MAX_PRICE = 999999.99;

  static validateName(name: string): void {
    if (!name || name.trim().length === 0) {
      throw new ProductValidationError(
        'Product name is required',
        'name',
        'REQUIRED'
      );
    }

    const trimmedName = name.trim();
    
    if (trimmedName.length < this.NAME_MIN_LENGTH) {
      throw new ProductValidationError(
        `Product name must be at least ${this.NAME_MIN_LENGTH} character`,
        'name',
        'MIN_LENGTH'
      );
    }

    if (trimmedName.length > this.NAME_MAX_LENGTH) {
      throw new ProductValidationError(
        `Product name cannot exceed ${this.NAME_MAX_LENGTH} characters`,
        'name',
        'MAX_LENGTH'
      );
    }
  }

  static validateDescription(description?: string): void {
    if (description && description.trim().length > this.DESCRIPTION_MAX_LENGTH) {
      throw new ProductValidationError(
        `Description cannot exceed ${this.DESCRIPTION_MAX_LENGTH} characters`,
        'description',
        'MAX_LENGTH'
      );
    }
  }

  static validateSku(sku: string): void {
    if (!sku || sku.trim().length === 0) {
      throw new ProductValidationError(
        'SKU is required',
        'sku',
        'REQUIRED'
      );
    }

    const trimmedSku = sku.trim();
    
    if (trimmedSku.length < this.SKU_MIN_LENGTH) {
      throw new ProductValidationError(
        `SKU must be at least ${this.SKU_MIN_LENGTH} characters`,
        'sku',
        'MIN_LENGTH'
      );
    }

    if (trimmedSku.length > this.SKU_MAX_LENGTH) {
      throw new ProductValidationError(
        `SKU cannot exceed ${this.SKU_MAX_LENGTH} characters`,
        'sku',
        'MAX_LENGTH'
      );
    }

    // SKU should only contain alphanumeric characters, hyphens, and underscores
    const validSkuPattern = /^[A-Z0-9\-_]+$/;
    if (!validSkuPattern.test(trimmedSku.toUpperCase())) {
      throw new ProductValidationError(
        'SKU can only contain letters, numbers, hyphens, and underscores',
        'sku',
        'INVALID_FORMAT'
      );
    }
  }

  static validatePrice(price: number): void {
    if (typeof price !== 'number' || isNaN(price)) {
      throw new ProductValidationError(
        'Price must be a valid number',
        'price',
        'INVALID_TYPE'
      );
    }

    if (price < this.MIN_PRICE) {
      throw new ProductValidationError(
        `Price must be at least $${this.MIN_PRICE}`,
        'price',
        'MIN_VALUE'
      );
    }

    if (price > this.MAX_PRICE) {
      throw new ProductValidationError(
        `Price cannot exceed $${this.MAX_PRICE.toLocaleString()}`,
        'price',
        'MAX_VALUE'
      );
    }

    // Check for reasonable decimal precision (max 2 decimal places)
    const decimalPlaces = price.toString().split('.')[1];
    if (decimalPlaces && decimalPlaces.length > 2) {
      throw new ProductValidationError(
        'Price cannot have more than 2 decimal places',
        'price',
        'INVALID_PRECISION'
      );
    }
  }

  static validateStockQuantity(quantity: number): void {
    if (typeof quantity !== 'number' || isNaN(quantity)) {
      throw new ProductValidationError(
        'Stock quantity must be a valid number',
        'stockQuantity',
        'INVALID_TYPE'
      );
    }

    if (quantity < 0) {
      throw new ProductValidationError(
        'Stock quantity cannot be negative',
        'stockQuantity',
        'NEGATIVE_VALUE'
      );
    }

    if (!Number.isInteger(quantity)) {
      throw new ProductValidationError(
        'Stock quantity must be a whole number',
        'stockQuantity',
        'NOT_INTEGER'
      );
    }
  }

  static validateCategoryId(categoryId: string): void {
    if (!categoryId || categoryId.trim().length === 0) {
      throw new ProductValidationError(
        'Category is required',
        'categoryId',
        'REQUIRED'
      );
    }
  }

  static validateForCreation(data: {
    name: string;
    description?: string;
    price: number;
    sku: string;
    categoryId: string;
    stockQuantity?: number;
  }): void {
    this.validateName(data.name);
    this.validateDescription(data.description);
    this.validatePrice(data.price);
    this.validateSku(data.sku);
    this.validateCategoryId(data.categoryId);
    this.validateStockQuantity(data.stockQuantity || 0);
  }

  static validateForUpdate(data: {
    name: string;
    description?: string;
    price: number;
    stockQuantity: number;
    categoryId: string;
  }): void {
    this.validateName(data.name);
    this.validateDescription(data.description);
    this.validatePrice(data.price);
    this.validateStockQuantity(data.stockQuantity);
    this.validateCategoryId(data.categoryId);
  }
}

// Business Rules
export class ProductBusinessRules {
  static canDelete(product: {
    isActive: boolean;
    hasOrders?: boolean;
  }): { canDelete: boolean; reason?: string } {
    if (product.hasOrders) {
      return {
        canDelete: false,
        reason: 'Cannot delete product that has been ordered. Deactivate instead.',
      };
    }

    return { canDelete: true };
  }

  static canUpdatePrice(product: {
    isActive: boolean;
    hasActiveSales?: boolean;
  }, newPrice: number): { canUpdate: boolean; reason?: string } {
    if (product.hasActiveSales && newPrice < 0) {
      return {
        canUpdate: false,
        reason: 'Cannot set negative price for product with active sales.',
      };
    }

    return { canUpdate: true };
  }

  static getStockAlert(stockQuantity: number, lowStockThreshold: number = 10): {
    level: 'none' | 'low' | 'critical';
    message?: string;
  } {
    if (stockQuantity === 0) {
      return {
        level: 'critical',
        message: 'Product is out of stock',
      };
    }

    if (stockQuantity <= lowStockThreshold) {
      return {
        level: 'low',
        message: `Low stock: ${stockQuantity} units remaining`,
      };
    }

    return { level: 'none' };
  }
}