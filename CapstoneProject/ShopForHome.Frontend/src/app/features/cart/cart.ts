import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CartService } from '../../core/services/cart.service';
import { Cart, CartItem } from '../../core/models/cart.model';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './cart.html',
  styleUrls: ['./cart.css']
})
export class CartComponent implements OnInit {
  cart?: Cart;
  loading = false;
  total = 0;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.loading = true;
    this.cartService.getCart().subscribe({
      next: (cart) => {
        this.cart = cart;
        this.calculateTotal();
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  updateQuantity(item: CartItem, quantity: number): void {
    if (quantity <= 0) {
      this.removeItem(item);
      return;
    }

    this.cartService.updateQuantity(item.cartItemId, quantity).subscribe({
      next: () => this.loadCart(),
      error: (err) => console.error('Update failed:', err)
    });
  }

  removeItem(item: CartItem): void {
    this.cartService.removeFromCart(item.cartItemId).subscribe({
      next: () => this.loadCart(),
      error: (err) => console.error('Remove failed:', err)
    });
  }

  calculateTotal(): void {
  this.total = this.cart?.items?.reduce((sum, item) =>
    sum + (item.quantity * (item.unitPrice || 0)), 0) || 0;
}



  getImageUrl(filename?: string): string {
    return filename ? `assets/images/${filename}` : 'assets/images/placeholder.png';
  }
}
