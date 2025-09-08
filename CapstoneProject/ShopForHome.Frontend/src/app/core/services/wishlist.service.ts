import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Product } from '../models/product.model';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class WishlistService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Wishlist`;
  
  private wishlistSubject = new BehaviorSubject<Product[]>([]);
  public wishlist$ = this.wishlistSubject.asObservable();

  getWishlist(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl).pipe(
      tap(items => this.wishlistSubject.next(items))
    );
  }

  addToWishlist(productId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, { productId }).pipe(
      tap(() => this.refreshWishlist())
    );
  }

  removeFromWishlist(productId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/remove/${productId}`).pipe(
      tap(() => this.refreshWishlist())
    );
  }

  isInWishlist(productId: number): Observable<boolean> {
  return this.wishlist$.pipe(
    map(items => items.some(item => item.productId === productId))
  );
  }

  private refreshWishlist(): void {
  // fetch once and update subject
  this.http.get<Product[]>(this.apiUrl).subscribe(items => this.wishlistSubject.next(items));
}

}
