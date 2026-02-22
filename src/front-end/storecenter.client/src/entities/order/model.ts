// Order Domain Model
export class Order {
  constructor(
    public readonly id: string,
    public readonly orderNumber: string,
    public readonly customerId: string,
    public readonly status: OrderStatus,
    public readonly items: OrderItem[],
    public readonly createdAt: Date = new Date(),
    public readonly updatedAt: Date = new Date()
  ) {}

  // Factory method for creating new order
  static create(data: {
    customerId: string;
    items: Array<{
      productId: string;
      quantity: number;
      unitPrice: number;
    }>;
  }): Order {
    const orderItems = data.items.map(item => 
      new OrderItem(
        generateId(),
        item.productId,
        item.quantity,
        item.unitPrice
      )
    );

    return new Order(
      generateId(),
      generateOrderNumber(),
      data.customerId,
      OrderStatus.PENDING,
      orderItems
    );
  }

  // Business methods
  updateStatus(newStatus: OrderStatus): Order {
    return new Order(
      this.id,
      this.orderNumber,
      this.customerId,
      newStatus,
      this.items,
      this.createdAt,
      new Date()
    );
  }

  addItem(productId: string, quantity: number, unitPrice: number): Order {
    const newItem = new OrderItem(
      generateId(),
      productId,
      quantity,
      unitPrice
    );

    return new Order(
      this.id,
      this.orderNumber,
      this.customerId,
      this.status,
      [...this.items, newItem],
      this.createdAt,
      new Date()
    );
  }

  removeItem(itemId: string): Order {
    const filteredItems = this.items.filter(item => item.id !== itemId);

    return new Order(
      this.id,
      this.orderNumber,
      this.customerId,
      this.status,
      filteredItems,
      this.createdAt,
      new Date()
    );
  }

  // Calculations
  get totalAmount(): number {
    return this.items.reduce((sum, item) => sum + item.totalPrice, 0);
  }

  get totalItems(): number {
    return this.items.reduce((sum, item) => sum + item.quantity, 0);
  }

  // Status checks
  canBeCancelled(): boolean {
    return this.status === OrderStatus.PENDING || this.status === OrderStatus.PROCESSING;
  }

  canBeShipped(): boolean {
    return this.status === OrderStatus.PROCESSING;
  }

  isCompleted(): boolean {
    return this.status === OrderStatus.DELIVERED;
  }

  toJSON() {
    return {
      id: this.id,
      orderNumber: this.orderNumber,
      customerId: this.customerId,
      status: this.status,
      items: this.items.map(item => item.toJSON()),
      totalAmount: this.totalAmount,
      createdAt: this.createdAt.toISOString(),
      updatedAt: this.updatedAt.toISOString(),
    };
  }
}

export class OrderItem {
  constructor(
    public readonly id: string,
    public readonly productId: string,
    public readonly quantity: number,
    public readonly unitPrice: number
  ) {}

  get totalPrice(): number {
    return this.quantity * this.unitPrice;
  }

  toJSON() {
    return {
      id: this.id,
      productId: this.productId,
      quantity: this.quantity,
      unitPrice: this.unitPrice,
      totalPrice: this.totalPrice,
    };
  }
}

export enum OrderStatus {
  PENDING = 'pending',
  PROCESSING = 'processing',
  SHIPPED = 'shipped',
  DELIVERED = 'delivered',
  CANCELLED = 'cancelled',
}

// Helper functions
function generateId(): string {
  return Math.random().toString(36).substr(2, 9);
}

function generateOrderNumber(): string {
  const now = new Date();
  const year = now.getFullYear();
  const month = String(now.getMonth() + 1).padStart(2, '0');
  const day = String(now.getDate()).padStart(2, '0');
  const random = Math.floor(Math.random() * 10000).toString().padStart(4, '0');
  
  return `ORD-${year}${month}${day}-${random}`;
}