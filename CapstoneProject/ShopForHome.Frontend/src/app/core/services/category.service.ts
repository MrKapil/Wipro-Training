import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Category, CategoryDto } from '../models/category.model';

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private apiUrl = `${environment.apiUrl}/Categories`;

  constructor(private http: HttpClient) {}

  getAllCategories(): Observable<CategoryDto[]> {
    return this.http.get<CategoryDto[]>(this.apiUrl);
  }

  getCategoryById(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.apiUrl}/${id}`);
  }
}
