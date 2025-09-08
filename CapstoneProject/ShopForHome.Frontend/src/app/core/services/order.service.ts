import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Order, CheckoutRequest } from '../models/order.model';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Orders`;

  checkout(request: CheckoutRequest): Observable<Order> {
    return this.http.post<Order>(`${this.apiUrl}/checkout`, request);
  }

  getMyOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/my`);
  }

  getOrderById(orderId: number): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/${orderId}`);
  }
  myOrders(): Observable<Order[]> {
  return this.http.get<Order[]>(`${this.apiUrl}/my`);
}

}
