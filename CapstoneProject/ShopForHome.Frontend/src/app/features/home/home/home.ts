import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { ProductService } from '@core/services/product.service';
import { CategoryService } from '@core/services/category.service';
import { CartService } from '@core/services/cart.service';
import { WishlistService } from '@core/services/wishlist.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class Home implements OnInit {
  featuredProducts: any[] = [];
  categories: any[] = [];
  loading = false;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private cartService: CartService,
    private wishlistService: WishlistService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadCategories();
    this.loadFeaturedProducts();
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (categories) => {
        this.categories = categories.map((cat: any) => ({
          ...cat,
          productCount: Math.floor(Math.random() * 50) + 10
        }));
      },
      error: (error) => console.error('Error loading categories:', error)
    });
  }

  loadFeaturedProducts(): void {
    this.loading = true;
    this.productService.getProducts({ page: 1, pageSize: 8 }).subscribe({
      next: (products) => {
        this.featuredProducts = products.map((product: any) => ({
          ...product,
          discount: Math.floor(Math.random() * 40) + 10,
          originalPrice: product.price * 1.3,
          reviewCount: Math.floor(Math.random() * 100) + 5
        }));
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading products:', error);
        this.loading = false;
      }
    });
  }

  filterByCategory(categoryId: any): void {
    this.router.navigate(['/products'], { queryParams: { categoryId } });
  }

  viewProduct(productId: any): void {
    this.router.navigate(['/products', productId]);
  }

  addToCart(product: any): void {
    this.cartService.addToCart(product.id, 1).subscribe({
      next: () => alert(`${product.name} added to cart!`),
      error: () => alert('Failed to add item to cart')
    });
  }

  addToWishlist(productId: any): void {
    this.wishlistService.addToWishlist(productId).subscribe({
      next: () => alert('Added to wishlist!'),
      error: () => alert('Failed to add to wishlist')
    });
  }

  buyNow(product: any): void {
    this.addToCart(product);
    setTimeout(() => this.router.navigate(['/cart']), 500);
  }

  getImageUrl(imageFileName?: string): string {
    return imageFileName ? `assets/images/${imageFileName}` : 'assets/images/placeholder.png';
  }

  formatPrice(price: number): string {
    return `₹${price.toLocaleString('en-IN', { minimumFractionDigits: 2 })}`;
  }

  getStarRating(rating?: number): string {
    if (!rating) return '';
    return '⭐'.repeat(Math.floor(rating));
  }
}
