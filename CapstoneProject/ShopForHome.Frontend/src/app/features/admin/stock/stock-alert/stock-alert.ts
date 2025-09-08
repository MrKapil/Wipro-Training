import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '@env/environment';

interface LowStockItem {
  productId: number;
  productName: string;
  sku: string;
  currentStock: number;
  categoryName: string;
  imageFileName: string;
}

@Component({
  selector: 'app-stock-alert',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stock-alert.html',
  styleUrls: ['./stock-alert.css']
})
export class StockAlertComponent implements OnInit {
  lowStockItems: LowStockItem[] = [];
  loading = false;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadLowStockItems();
  }

  loadLowStockItems(): void {
    this.loading = true;
    this.http.get<LowStockItem[]>(`${environment.apiUrl}/Stock/low-stock`).subscribe({
      next: (data) => {
        this.lowStockItems = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        // Fallback sample data
        this.lowStockItems = [
          { productId: 1, productName: 'Classic Wood Chair', sku: 'CH-1001', currentStock: 5, categoryName: 'Furniture', imageFileName: 'chair-wood-01.jpg' },
          { productId: 3, productName: 'Sony 42 inch TV', sku: 'TV-SONY-42', currentStock: 3, categoryName: 'Electronics', imageFileName: 'tv-sony-42.jpg' }
        ];
      }
    });
  }

  updateStock(productId: number, newStock: number): void {
    this.http.put(`${environment.apiUrl}/Stock/update`, { productId, stockQty: newStock })
      .subscribe(() => {
        this.loadLowStockItems();
        alert('Stock updated successfully');
      });
  }
}
