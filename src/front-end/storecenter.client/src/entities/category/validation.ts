// Category Validation Rules
export class CategoryValidationError extends Error {
  constructor(
    message: string,
    public readonly field: string,
    public readonly code: string
  ) {
    super(message);
    this.name = 'CategoryValidationError';
  }
}

export class CategoryValidator {
  static readonly NAME_MIN_LENGTH = 1;
  static readonly NAME_MAX_LENGTH = 100;
  static readonly DESCRIPTION_MAX_LENGTH = 500;

  static validateName(name: string): void {
    if (!name || name.trim().length === 0) {
      throw new CategoryValidationError(
        'Category name is required',
        'name',
        'REQUIRED'
      );
    }

    const trimmedName = name.trim();
    
    if (trimmedName.length < this.NAME_MIN_LENGTH) {
      throw new CategoryValidationError(
        `Category name must be at least ${this.NAME_MIN_LENGTH} character`,
        'name',
        'MIN_LENGTH'
      );
    }

    if (trimmedName.length > this.NAME_MAX_LENGTH) {
      throw new CategoryValidationError(
        `Category name cannot exceed ${this.NAME_MAX_LENGTH} characters`,
        'name',
        'MAX_LENGTH'
      );
    }

    // Check for invalid characters
    const invalidChars = /[<>\"'&]/;
    if (invalidChars.test(trimmedName)) {
      throw new CategoryValidationError(
        'Category name contains invalid characters',
        'name',
        'INVALID_CHARACTERS'
      );
    }
  }

  static validateDescription(description?: string): void {
    if (description && description.trim().length > this.DESCRIPTION_MAX_LENGTH) {
      throw new CategoryValidationError(
        `Description cannot exceed ${this.DESCRIPTION_MAX_LENGTH} characters`,
        'description',
        'MAX_LENGTH'
      );
    }
  }

  static validateParentId(parentId?: string, currentId?: string): void {
    // Prevent self-referencing
    if (parentId && currentId && parentId === currentId) {
      throw new CategoryValidationError(
        'Category cannot be its own parent',
        'parentId',
        'SELF_REFERENCE'
      );
    }

    // In real app, you'd validate that parent exists and doesn't create circular reference
  }

  static validateForCreation(data: {
    name: string;
    description?: string;
    parentId?: string;
  }): void {
    this.validateName(data.name);
    this.validateDescription(data.description);
    this.validateParentId(data.parentId);
  }

  static validateForUpdate(data: {
    id: string;
    name: string;
    description?: string;
    parentId?: string;
  }): void {
    this.validateName(data.name);
    this.validateDescription(data.description);
    this.validateParentId(data.parentId, data.id);
  }
}

// Business Rules
export class CategoryBusinessRules {
  static canDelete(category: {
    isActive: boolean;
    hasProducts?: boolean;
    hasSubcategories?: boolean;
  }): { canDelete: boolean; reason?: string } {
    if (category.isActive) {
      return {
        canDelete: false,
        reason: 'Cannot delete active category. Deactivate it first.',
      };
    }

    if (category.hasProducts) {
      return {
        canDelete: false,
        reason: 'Cannot delete category that has products. Move or delete products first.',
      };
    }

    if (category.hasSubcategories) {
      return {
        canDelete: false,
        reason: 'Cannot delete category that has subcategories. Remove subcategories first.',
      };
    }

    return { canDelete: true };
  }

  static canActivate(category: {
    parentId?: string;
    parentIsActive?: boolean;
  }): { canActivate: boolean; reason?: string } {
    if (category.parentId && !category.parentIsActive) {
      return {
        canActivate: false,
        reason: 'Cannot activate category when parent category is inactive.',
      };
    }

    return { canActivate: true };
  }
}