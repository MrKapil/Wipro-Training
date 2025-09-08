import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, BehaviorSubject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';

interface LoginRequest {
  email: string;
  password: string;
}

interface RegisterRequest {
  fullName: string;
  email: string;
  password: string;
}

interface AuthResponse {
  token: string;
  expiresAt: string;
  role: string;
  fullName: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private api = `${environment.apiUrl}/Auth`;
  private tokenKey = 'shopforhome_token';
  private userKey = 'shopforhome_user';
  
  // Observable to track login state
  private isLoggedInSubject = new BehaviorSubject<boolean>(this.hasValidToken());
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();

    constructor(private http: HttpClient, private router: Router) {}

  login(email: string, password: string): Observable<AuthResponse> {
    const request: LoginRequest = { email, password };
    
    return this.http.post<AuthResponse>(`${this.api}/login`, request).pipe(
      tap(response => {
        this.storeAuthData(response);
        this.isLoggedInSubject.next(true);
      })
    );
  }

  register(fullName: string, email: string, password: string): Observable<AuthResponse> {
    const request: RegisterRequest = { fullName, email, password };
    
    return this.http.post<AuthResponse>(`${this.api}/register`, request).pipe(
      tap(response => {
        this.storeAuthData(response);
        this.isLoggedInSubject.next(true);
      })
    );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userKey);
    this.isLoggedInSubject.next(false);
    // Redirect to login page after logout
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getUserName(): string | null {
    const userData = localStorage.getItem(this.userKey);
    return userData ? JSON.parse(userData).fullName : null;
  }

  getUserRole(): string | null {
    const userData = localStorage.getItem(this.userKey);
    return userData ? JSON.parse(userData).role : null;
  }

  isLoggedIn(): boolean {
    return this.hasValidToken();
  }

  isAdmin(): boolean {
    return this.getUserRole() === 'Admin';
  }

  private storeAuthData(response: AuthResponse): void {
    localStorage.setItem(this.tokenKey, response.token);
    localStorage.setItem(this.userKey, JSON.stringify({
      fullName: response.fullName,
      role: response.role,
      expiresAt: response.expiresAt
    }));
  }

  private hasValidToken(): boolean {
    const token = this.getToken();
    if (!token) return false;

    const userData = localStorage.getItem(this.userKey);
    if (!userData) return false;

    const { expiresAt } = JSON.parse(userData);
    return new Date(expiresAt) > new Date();
  }
}
