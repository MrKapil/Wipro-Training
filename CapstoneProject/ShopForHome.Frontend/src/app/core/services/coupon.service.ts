import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Coupon, ApplyCouponRequest, ApplyCouponResponse } from '../models/coupon.model';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class CouponService {
  private api = `${environment.apiUrl}/Coupons`;

  constructor(private http: HttpClient) {}

  // User: Get my assigned coupons
  getMyCoupons(): Observable<Coupon[]> {
    return this.http.get<Coupon[]>(`${this.api}/my`);
  }

  // User: Apply coupon
  applyCoupon(code: string, orderTotal: number): Observable<ApplyCouponResponse> {
    return this.http.post<ApplyCouponResponse>(`${this.api}/apply?orderTotal=${orderTotal}`, { code });
  }

  // Admin: Create coupon
  createCoupon(code: string, discountPercent: number): Observable<Coupon> {
    return this.http.post<Coupon>(`${this.api}/create?code=${code}&discountPercent=${discountPercent}`, {});
  }

  // Admin: Assign coupon to user
  assignCoupon(couponId: number, userId: number): Observable<string> {
    return this.http.post<string>(`${this.api}/assign?couponId=${couponId}&userId=${userId}`, {});
  }
}
