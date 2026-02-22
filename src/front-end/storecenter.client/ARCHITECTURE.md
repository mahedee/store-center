# Store Center Frontend

A modern Next.js 14+ application following feature-driven architecture with Domain-Driven Design (DDD) principles.

## Architecture Overview

This project follows a clean, scalable architecture that separates concerns and promotes maintainability:

```
src/
├── app/                          # Next.js 13+ App Router
│   ├── (auth)/                   # Auth route group
│   ├── (dashboard)/              # Dashboard route group
│   ├── layout.tsx                # Root layout
│   ├── page.tsx                  # Home page
│   └── globals.css               # Global styles
│
├── shared/                       # Shared utilities across features
│   ├── api/                      # API layer
│   ├── components/               # Reusable UI components
│   ├── hooks/                    # Custom React hooks
│   ├── utils/                    # Pure utility functions
│   ├── constants/                # App constants
│   ├── types/                    # TypeScript type definitions
│   └── stores/                   # Global state management
│
├── features/                     # Feature-based modules
│   ├── auth/                     # Authentication feature
│   ├── categories/               # Category management
│   ├── products/                 # Product management
│   ├── orders/                   # Order management
│   └── customers/                # Customer management
│
├── entities/                     # Business entities (DDD approach)
│   ├── category/                 # Category domain model
│   ├── product/                  # Product domain model
│   └── order/                    # Order domain model
│
└── widgets/                      # Complex composed components
    ├── CategoryCatalog/          # Category management widget
    ├── ProductCatalog/           # Product catalog widget
    ├── OrderSummary/             # Order summary widget
    └── DashboardStats/           # Dashboard statistics
```

## Key Features

### 1. Feature-Driven Architecture
- Each feature is self-contained with its own components, hooks, services, and types
- Clear separation of concerns
- Easy to scale and maintain

### 2. Domain-Driven Design (DDD)
- Business entities with domain logic
- Validation rules and business rules separated
- Rich domain models with behavior

### 3. Modern React Patterns
- React Query for server state management
- Custom hooks for data fetching
- TypeScript for type safety
- Composition over inheritance

### 4. Scalable UI Components
- Shared UI components in `shared/components/ui`
- Layout components for consistent structure
- Widgets for complex business logic

## Getting Started

### Prerequisites
- Node.js 18+ 
- npm or yarn

### Installation

1. Install dependencies:
```bash
npm install
```

2. Set up environment variables:
```bash
cp .env.example .env.local
```

3. Configure your API base URL in `.env.local`:
```
NEXT_PUBLIC_API_BASE_URL=https://localhost:5001/api
```

4. Start the development server:
```bash
npm run dev
```

## Project Structure Explained

### `/app` - Next.js App Router
- Uses Next.js 13+ App Router with route groups
- `(auth)` - Authentication pages (login, register)
- `(dashboard)` - Main application pages

### `/shared` - Shared Resources
- **api/**: HTTP client, types, and query configurations
- **components/**: Reusable UI components (Button, Input, etc.)
- **utils/**: Pure utility functions
- **constants/**: Application constants
- **types/**: Shared TypeScript types

### `/features` - Feature Modules
Each feature follows this structure:
- **components/**: Feature-specific UI components
- **hooks/**: Data fetching and state management hooks
- **services/**: API service functions
- **stores/**: Feature-specific state (if needed)
- **types.ts**: Feature-specific TypeScript types

### `/entities` - Business Entities
- Domain models with business logic
- Validation rules and business rules
- Rich objects that encapsulate behavior

### `/widgets` - Composed Components
- Complex UI components that combine multiple features
- Business-focused components like catalogs and dashboards

## Development Guidelines

### Adding a New Feature

1. Create the feature directory structure:
```
src/features/my-feature/
├── components/
├── hooks/
├── services/
├── stores/
└── types.ts
```

2. Define types in `types.ts`
3. Create API service functions
4. Build custom hooks for data management
5. Create UI components
6. Add widgets if needed

### Creating Business Entities

1. Create entity directory:
```
src/entities/my-entity/
├── model.ts
└── validation.ts
```

2. Implement domain model with business logic
3. Add validation and business rules

### API Integration

- Use the shared API client in `shared/api/client.ts`
- Follow the service pattern in `features/*/services/`
- Use React Query for data fetching and caching
- Handle errors consistently

### State Management

- Use React Query for server state
- Use React hooks for local state
- Create custom hooks for complex state logic
- Keep state close to where it's used

## Technology Stack

- **Framework**: Next.js 14+
- **Language**: TypeScript
- **Styling**: Tailwind CSS
- **State Management**: React Query + React Hooks
- **HTTP Client**: Axios
- **Form Handling**: Native React forms
- **Icons**: Heroicons (SVG)

## Contributing

1. Follow the established architecture patterns
2. Write TypeScript with proper typing
3. Create reusable components when appropriate
4. Add proper error handling
5. Write meaningful commit messages

## Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run start` - Start production server
- `npm run lint` - Run ESLint

## API Integration

The application expects a REST API with the following endpoints:
- `/auth/*` - Authentication endpoints
- `/Categories` - Category management
- `/Products` - Product management  
- `/Orders` - Order management
- `/Customers` - Customer management

See the backend API documentation for detailed endpoint specifications.