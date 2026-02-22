// Category Domain Model
export class Category {
  constructor(
    public readonly id: string,
    public readonly name: string,
    public readonly description: string | null = null,
    public readonly parentId: string | null = null,
    public readonly isActive: boolean = true,
    public readonly createdAt: Date = new Date(),
    public readonly updatedAt: Date = new Date()
  ) {}

  // Factory method for creating new category
  static create(data: {
    name: string;
    description?: string;
    parentId?: string;
  }): Category {
    return new Category(
      generateId(),
      data.name.trim(),
      data.description?.trim() || null,
      data.parentId || null
    );
  }

  // Business methods
  activate(): Category {
    return new Category(
      this.id,
      this.name,
      this.description,
      this.parentId,
      true,
      this.createdAt,
      new Date()
    );
  }

  deactivate(): Category {
    return new Category(
      this.id,
      this.name,
      this.description,
      this.parentId,
      false,
      this.createdAt,
      new Date()
    );
  }

  updateInfo(name: string, description?: string): Category {
    if (!name.trim()) {
      throw new Error('Category name cannot be empty');
    }

    return new Category(
      this.id,
      name.trim(),
      description?.trim() || null,
      this.parentId,
      this.isActive,
      this.createdAt,
      new Date()
    );
  }

  // Validation methods
  isValid(): boolean {
    return this.name.length > 0 && this.name.length <= 100;
  }

  canBeDeleted(): boolean {
    // Business rule: can only delete if inactive
    // In real app, you'd also check if it has products or subcategories
    return !this.isActive;
  }

  // Helper methods
  get displayName(): string {
    return this.name;
  }

  get fullDescription(): string {
    return this.description || `Category: ${this.name}`;
  }

  toJSON() {
    return {
      id: this.id,
      name: this.name,
      description: this.description,
      parentId: this.parentId,
      isActive: this.isActive,
      createdAt: this.createdAt.toISOString(),
      updatedAt: this.updatedAt.toISOString(),
    };
  }
}

// Helper function to generate IDs (in real app, this would be handled by the backend)
function generateId(): string {
  return Math.random().toString(36).substr(2, 9);
}