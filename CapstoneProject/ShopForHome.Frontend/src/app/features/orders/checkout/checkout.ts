import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from '@core/services/cart.service';
import { OrderService } from '@core/services/order.service';
import { CouponService } from '@core/services/coupon.service';
import { Cart } from '@core/models/cart.model';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './checkout.html',
  styleUrls: ['./checkout.css']
})
export class CheckoutComponent implements OnInit {
  cart?: Cart;
  shippingAddress = '';
  couponCode = '';
  appliedCoupon: any = null;
  totalAmount = 0;
  discountAmount = 0;
  finalAmount = 0;
  loading = false;

  constructor(
    private cartService: CartService,
    private orderService: OrderService,
    private couponService: CouponService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.cartService.getCart().subscribe(cart => {
      this.cart = cart;
      this.calculateTotal();
    });
  }

  calculateTotal(): void {
    if (!this.cart?.items) return;
    
    this.totalAmount = this.cart.items.reduce(
      (sum, item) => sum + (item.unitPrice * item.quantity), 0
    );
    
    this.finalAmount = this.totalAmount - this.discountAmount;
  }

  applyCoupon(): void {
    if (!this.couponCode.trim()) return;

    this.couponService.applyCoupon(this.couponCode, this.totalAmount).subscribe({
      next: (result) => {
        this.appliedCoupon = result;
        this.discountAmount = result.discountAmount || 0;
        this.calculateTotal();
      },
      error: () => alert('Invalid coupon code')
    });
  }

  removeCoupon(): void {
    this.appliedCoupon = null;
    this.couponCode = '';
    this.discountAmount = 0;
    this.calculateTotal();
  }

  placeOrder(): void {
    if (!this.shippingAddress.trim()) {
      alert('Please enter shipping address');
      return;
    }

    this.loading = true;
    const orderData = {
      shippingAddress: this.shippingAddress,
      couponCode: this.appliedCoupon?.code
    };

    this.orderService.checkout(orderData).subscribe({
      next: (order) => {
        alert(`Order placed successfully! Order ID: ${order.orderId}`);
        this.router.navigate(['/orders']);
      },
      error: () => {
        alert('Order failed. Please try again.');
        this.loading = false;
      }
    });
  }
}
