import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CouponService } from '../../../core/services/coupon.service';
import { Coupon } from '../../../core/models/coupon.model';

@Component({
  selector: 'app-admin-coupons',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './coupons.html',
  styleUrls: ['./coupons.css']
})
export class CouponsComponent implements OnInit {
  coupons: Coupon[] = [];
  
  // Create coupon form
  newCouponCode = '';
  newDiscountPercent = 0;
  
  // Assign coupon form
  selectedCouponId = 0;
  targetUserId = 0;
  
  message = '';

  constructor(private couponService: CouponService) {}

  ngOnInit(): void {
    this.loadCoupons();
  }

  loadCoupons(): void {
    // Note: You'll need a getAllCoupons method in backend for admin
    // For now, we'll use getMyCoupons as placeholder
    this.couponService.getMyCoupons().subscribe({
      next: coupons => this.coupons = coupons,
      error: err => this.message = 'Failed to load coupons'
    });
  }

  createCoupon(): void {
    if (!this.newCouponCode || this.newDiscountPercent <= 0) {
      this.message = 'Please enter valid coupon code and discount percent';
      return;
    }

    this.couponService.createCoupon(this.newCouponCode, this.newDiscountPercent).subscribe({
      next: () => {
        this.message = 'Coupon created successfully!';
        this.newCouponCode = '';
        this.newDiscountPercent = 0;
        this.loadCoupons();
      },
      error: err => this.message = 'Failed to create coupon'
    });
  }

  assignCoupon(): void {
    if (!this.selectedCouponId || !this.targetUserId) {
      this.message = 'Please select coupon and enter user ID';
      return;
    }

    this.couponService.assignCoupon(this.selectedCouponId, this.targetUserId).subscribe({
      next: () => {
        this.message = 'Coupon assigned successfully!';
        this.selectedCouponId = 0;
        this.targetUserId = 0;
      },
      error: err => this.message = 'Failed to assign coupon'
    });
  }
}
