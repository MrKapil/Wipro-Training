export interface Product {
  productId: number;
  name: string;
  sku: string;
  description?: string;
  price: number;
  rating?: number;
  categoryId?: number;
  category?: string;
  imageFileName?: string;
  isActive: boolean;
  createdAt?: string;
  stockQuantity?: number;
}

export interface ProductFilters {
  page?: number;
  pageSize?: number;
  categoryId?: number;
  minPrice?: number;
  maxPrice?: number;
  rating?: number;
  q?: string;
}
