import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Cart, CartItem } from '../models/cart.model';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class CartService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Cart`;
  
  private cartSubject = new BehaviorSubject<Cart | null>(null);
  public cart$ = this.cartSubject.asObservable();

  getCart(): Observable<Cart> {
    return this.http.get<Cart>(this.apiUrl).pipe(
      tap(cart => this.cartSubject.next(cart))
    );
  }

  addToCart(productId: number, quantity: number = 1): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, { productId, quantity }).pipe(
      tap(() => this.refreshCart())
    );
  }

  updateQuantity(cartItemId: number, quantity: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/update`, { cartItemId, quantity }).pipe(
      tap(() => this.refreshCart())
    );
  }

  removeFromCart(cartItemId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/remove/${cartItemId}`).pipe(
      tap(() => this.refreshCart())
    );
  }

  clearCart(): Observable<any> {
    return this.http.delete(`${this.apiUrl}/clear`).pipe(
      tap(() => this.cartSubject.next(null))
    );
  }

  private refreshCart(): void {
    this.getCart().subscribe();
  }

  getCartItemCount(): Observable<number> {
  return this.cart$.pipe(
    map(cart => cart?.items?.reduce((sum, item) => sum + item.quantity, 0) || 0)
  );
}
}
