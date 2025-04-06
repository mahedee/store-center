This is a [Next.js](https://nextjs.org) project bootstrapped with [`create-next-app`](https://github.com/vercel/next.js/tree/canary/packages/create-next-app).

## Getting Started

First, run the development server:

```bash
npm run dev
# or
yarn dev
# or
pnpm dev
# or
bun dev
```

Open [http://localhost:3000](http://localhost:3000) with your browser to see the result.

You can start editing the page by modifying `app/page.js`. The page auto-updates as you edit the file.

This project uses [`next/font`](https://nextjs.org/docs/app/building-your-application/optimizing/fonts) to automatically optimize and load [Geist](https://vercel.com/font), a new font family for Vercel.

## Learn More

To learn more about Next.js, take a look at the following resources:

- [Next.js Documentation](https://nextjs.org/docs) - learn about Next.js features and API.
- [Learn Next.js](https://nextjs.org/learn) - an interactive Next.js tutorial.

You can check out [the Next.js GitHub repository](https://github.com/vercel/next.js) - your feedback and contributions are welcome!

## Deploy on Vercel

The easiest way to deploy your Next.js app is to use the [Vercel Platform](https://vercel.com/new?utm_medium=default-template&filter=next.js&utm_source=create-next-app&utm_campaign=create-next-app-readme) from the creators of Next.js.

Check out our [Next.js deployment documentation](https://nextjs.org/docs/app/building-your-application/deploying) for more details.


## Recommended Folder Structure

```bash
/src
├── /app                    → App Router pages and layouts
│   ├── /dashboard
│   │   ├── /page.jsx       → Route handler
│   │   └── /layout.jsx     → Optional layout for this route
│   ├── /settings
│   │   └── /categories
│   │       ├── page.jsx
│   │       ├── CreateCategoryForm.jsx
│   │       └── EditCategoryForm.jsx
│   └── layout.js           → Root layout
├── /components             → Reusable UI components
│   ├── Sidebar.jsx
│   ├── Table.jsx
│   └── Button.jsx
├── /features               → Domain-driven folder for each feature/module
│   └── /category
│       ├── CategoryList.jsx
│       ├── CreateCategory.jsx
│       ├── EditCategory.jsx
│       └── index.js        → Barrel export (optional)
├── /services               → API calls, all centralized
│   ├── api.js              → Axios instance with base URL, interceptors
│   └── categoryService.js  → All category-related API calls
├── /hooks                  → Reusable custom hooks (e.g. useFetch, useCategory)
├── /lib                    → Utility functions, helpers, constants
│   ├── utils.js
│   └── constants.js
├── /store                  → (Optional) Redux, Zustand, or context store
├── /styles
│   ├── globals.css
│   └── tailwind.config.js
├── /types                  → TypeScript interfaces/types (if using TS)
└── /assets                 → Images, fonts, icons, etc.

```