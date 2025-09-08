import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderService } from '../../../core/services/order.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-order-history',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './order-history.html',
  styleUrls: ['./order-history.css']
})
export class OrderHistoryComponent implements OnInit {
  orders: any[] = [];
  loading = false;
  error = '';

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.loadOrderHistory();
  }

  loadOrderHistory(): void {
    this.loading = true;
    this.orderService.myOrders().subscribe({
      next: (orders) => {
        this.orders = orders;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load order history';
        this.loading = false;
        console.error('Error loading orders:', err);
      }
    });
  }

  getStatusColor(status: string): string {
    switch(status?.toLowerCase()) {
      case 'delivered': return '#22c55e';
      case 'shipped': return '#3b82f6'; 
      case 'processing': return '#f59e0b';
      case 'cancelled': return '#ef4444';
      default: return '#6b7280';
    }
  }

  calculateTotal(order: any): number {
    return order.items?.reduce((total: number, item: any) => 
      total + (item.unitPrice * item.quantity), 0) || 0;
  }
}
