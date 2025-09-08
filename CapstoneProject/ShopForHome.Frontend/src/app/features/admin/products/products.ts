import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Product } from '@core/models/product.model';
import { Category } from '@core/models/category.model';


@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './products.html',
  styleUrls: ['./products.css']
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  categories: Category[] = [];
  showForm = false;
  showBulkUpload = false;
  currentProduct: Partial<Product> = {};
  isEditing = false;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadProducts();
    this.loadCategories();
  }

  loadProducts(): void {
    this.http.get<Product[]>(`${environment.apiUrl}/Products`).subscribe(
      data => this.products = data
    );
  }

  loadCategories(): void {
    this.http.get<Category[]>(`${environment.apiUrl}/Categories`).subscribe(
      data => this.categories = data
    );
  }

  addProduct(): void {
    this.currentProduct = { isActive: true, rating: 0 };
    this.isEditing = false;
    this.showForm = true;
  }

  editProduct(product: Product): void {
    this.currentProduct = { ...product };
    this.isEditing = true;
    this.showForm = true;
  }

  saveProduct(): void {
    if (this.isEditing) {
      this.http.put(`${environment.apiUrl}/Products/${this.currentProduct.productId}`, this.currentProduct)
        .subscribe(() => {
          this.loadProducts();
          this.closeForm();
        });
    } else {
      this.http.post(`${environment.apiUrl}/Products`, this.currentProduct)
        .subscribe(() => {
          this.loadProducts();
          this.closeForm();
        });
    }
  }

  deleteProduct(productId: number): void {
    if (confirm('Are you sure you want to delete this product?')) {
      this.http.delete(`${environment.apiUrl}/Products/${productId}`)
        .subscribe(() => this.loadProducts());
    }
  }

  closeForm(): void {
    this.showForm = false;
    this.currentProduct = {};
  }

  // Bulk upload functionality
  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('file', file);
      
      this.http.post(`${environment.apiUrl}/BulkUpload/products`, formData)
        .subscribe({
          next: (result: any) => {
            alert(`Upload completed. ${result.successCount} products added.`);
            this.loadProducts();
            this.showBulkUpload = false;
          },
          error: () => alert('Upload failed. Please check the CSV format.')
        });
    }
  }

  getCategoryName(categoryId: number): string {
    const category = this.categories.find(c => c.categoryId === categoryId);
    return category?.name || 'Unknown';
  }
}
