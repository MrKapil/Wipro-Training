import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Product, ProductFilters } from '../../../core/models/product.model';
import { CategoryDto } from '../../../core/models/category.model';
import { ProductService } from '../../../core/services/product.service';
import { CategoryService } from '../../../core/services/category.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './product-list.html',
  styleUrls: ['./product-list.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  categories: CategoryDto[] = [];
  loading = false;
  
  // Filter properties
  filters: ProductFilters = {
    page: 1,
    pageSize: 12
  };
  
  searchQuery = '';
  selectedCategoryId?: number;
  minPrice?: number;
  maxPrice?: number;
  minRating?: number;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.loadCategories();
    this.loadProducts();
  }

  loadCategories(): void {
  this.categoryService.getAllCategories().subscribe({
    next: (categories) => {
      //  Map categories to ensure consistent 'id' property
      this.categories = categories.map(cat => ({
        ...cat,
        id: categories  || cat.categoryId || 0  // Use id if exists, fallback to categoryId
      }));
    },
    error: (error) => console.error('Error loading categories:', error)
  });
}

  loadProducts(): void {
    this.loading = true;
    
    // Build filters
    const filters: ProductFilters = {
      ...this.filters,
      categoryId: this.selectedCategoryId,
      minPrice: this.minPrice,
      maxPrice: this.maxPrice,
      rating: this.minRating,
      q: this.searchQuery.trim() || undefined
    };

    this.productService.getProducts(filters).subscribe({
      next: (products) => {
        this.products = products;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading products:', error);
        this.loading = false;
      }
    });
  }

  // Apply filters
  applyFilters(): void {
    this.filters.page = 1; // Reset to first page
    this.loadProducts();
  }

  // Clear filters
  clearFilters(): void {
    this.searchQuery = '';
    this.selectedCategoryId = undefined;
    this.minPrice = undefined;
    this.maxPrice = undefined;
    this.minRating = undefined;
    this.filters = { page: 1, pageSize: 12 };
    this.loadProducts();
  }

  // Get image URL with fallback
  getImageUrl(imageFileName?: string): string {
    if (imageFileName) {
      return `assets/images/${imageFileName}`;
    }
    return 'assets/images/placeholder.png';
  }

  // Format price
  formatPrice(price: number): string {
    return `₹${price.toLocaleString('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
  }

  // Format rating
  getStarRating(rating?: number): string {
    if (!rating) return '';
    return '⭐'.repeat(Math.floor(rating)) + (rating % 1 >= 0.5 ? '⭐' : '');
  }
}
