import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Product } from '../../../core/models/product.model';
import { ProductService } from '../../../core/services/product.service';
import { CartService } from '../../../core/services/cart.service';
import { WishlistService } from '../../../core/services/wishlist.service';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './product-detail.html',
  styleUrls: ['./product-detail.css']
})
export class ProductDetailComponent implements OnInit {
  product?: Product;
  loading = false;
  quantity = 1;
  addingToCart = false;
  addingToWishlist = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private cartService: CartService,
    private wishlistService: WishlistService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const productId = Number(this.route.snapshot.paramMap.get('id'));
    if (productId) {
      this.loadProduct(productId);
    }
  }

  loadProduct(id: number): void {
    this.loading = true;
    this.productService.getProductById(id).subscribe({
      next: (product) => {
        this.product = product;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading product:', error);
        this.loading = false;
        // Redirect to products page if product not found
        this.router.navigate(['/products']);
      }
    });
  }

  addToCart(): void {
    if (!this.product) return;
    
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }

    this.addingToCart = true;
    this.cartService.addToCart(this.product.productId, this.quantity).subscribe({
      next: () => {
        this.addingToCart = false;
        // Show success message (you could use a toast service here)
        alert(`Added ${this.quantity} item(s) to cart!`);
      },
      error: (error) => {
        console.error('Error adding to cart:', error);
        this.addingToCart = false;
        alert('Failed to add item to cart. Please try again.');
      }
    });
  }

  addToWishlist(): void {
    if (!this.product) return;
    
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }

    this.addingToWishlist = true;
    this.wishlistService.addToWishlist(this.product.productId).subscribe({
      next: () => {
        this.addingToWishlist = false;
        alert('Added to wishlist!');
      },
      error: (error) => {
        console.error('Error adding to wishlist:', error);
        this.addingToWishlist = false;
        alert('Failed to add to wishlist. Please try again.');
      }
    });
  }

  getImageUrl(imageFileName?: string): string {
    if (imageFileName) {
      return `assets/images/${imageFileName}`;
    }
    return 'assets/images/placeholder.png';
  }

  formatPrice(price: number): string {
    return `₹${price.toLocaleString('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
  }

  getStarRating(rating?: number): string {
    if (!rating) return '';
    return '⭐'.repeat(Math.floor(rating)) + (rating % 1 >= 0.5 ? '⭐' : '');
  }

  increaseQuantity(): void {
    this.quantity++;
  }

  decreaseQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
}
