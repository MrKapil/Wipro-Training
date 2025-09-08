import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { WishlistService } from '../../core/services/wishlist.service';
import { CartService } from '../../core/services/cart.service';
import { Product } from '../../core/models/product.model';

@Component({
  selector: 'app-wishlist',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './wishlist.html',
  styleUrls: ['./wishlist.css']
})
export class WishlistComponent implements OnInit {
  wishlistItems: Product[] = [];
  loading = false;

  constructor(
    private wishlistService: WishlistService,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    this.loadWishlist();
  }

  loadWishlist(): void {
    this.loading = true;
    this.wishlistService.getWishlist().subscribe({
      next: (items) => {
        this.wishlistItems = items;
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  removeFromWishlist(productId: number): void {
    this.wishlistService.removeFromWishlist(productId).subscribe({
      next: () => this.loadWishlist(),
      error: (err) => console.error('Remove failed:', err)
    });
  }

  moveToCart(product: Product): void {
    this.cartService.addToCart(product.productId, 1).subscribe({
      next: () => {
        this.removeFromWishlist(product.productId);
        alert('Product moved to cart!');
      },
      error: (err) => console.error('Add to cart failed:', err)
    });
  }

  getImageUrl(filename?: string): string {
    return filename ? `assets/images/${filename}` : 'assets/images/placeholder.png';
  }
}
