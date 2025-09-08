import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Product, ProductFilters } from '../models/product.model';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private apiUrl = `${environment.apiUrl}/Products`;

  constructor(private http: HttpClient) {}

  // Get products with filters
  getProducts(filters?: ProductFilters): Observable<Product[]> {
    let params = new HttpParams();
    
    if (filters) {
      if (filters.page) params = params.set('page', filters.page.toString());
      if (filters.pageSize) params = params.set('pageSize', filters.pageSize.toString());
      if (filters.categoryId) params = params.set('categoryId', filters.categoryId.toString());
      if (filters.minPrice) params = params.set('minPrice', filters.minPrice.toString());
      if (filters.maxPrice) params = params.set('maxPrice', filters.maxPrice.toString());
      if (filters.rating) params = params.set('rating', filters.rating.toString());
      if (filters.q) params = params.set('q', filters.q);
    }

    return this.http.get<Product[]>(this.apiUrl, { params });
  }

  // Get single product by ID
  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  // Admin: Create product
  createProduct(product: Partial<Product>): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  // Admin: Update product
  updateProduct(id: number, product: Partial<Product>): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl}/${id}`, product);
  }

  // Admin: Delete product (soft delete)
  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  // Search products
  searchProducts(query: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${environment.apiUrl}/Search`, {
      params: { q: query }
    });
  }
}
