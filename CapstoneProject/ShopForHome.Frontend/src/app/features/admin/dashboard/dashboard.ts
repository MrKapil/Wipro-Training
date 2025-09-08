import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css']
})
export class DashboardComponent implements OnInit {
  stats = {
    totalUsers: 0,
    totalProducts: 0,
    lowStockCount: 0,
    totalOrders: 0
  };

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadStats();
  }

  loadStats(): void {
    // Load dashboard statistics
    this.http.get<any>(`${environment.apiUrl}/Reports/dashboard-stats`).subscribe(
      data => this.stats = data,
      () => {
        // Fallback static data if endpoint doesn't exist yet
        this.stats = { totalUsers: 25, totalProducts: 156, lowStockCount: 8, totalOrders: 47 };
      }
    );
  }
}
