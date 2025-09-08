export interface Coupon {
  couponId: number;
  code: string;
  discountPercent: number;  // e.g., 10 = 10% off
  isActive?: boolean;
}

export interface CouponAssignment {
  couponAssignmentId: number;
  couponId: number;
  userId: number;
  coupon?: Coupon;
}

export interface ApplyCouponRequest {
  code: string;
}

export interface ApplyCouponResponse {
  original: number;
  final: number;
  discountAmount: number;
}
