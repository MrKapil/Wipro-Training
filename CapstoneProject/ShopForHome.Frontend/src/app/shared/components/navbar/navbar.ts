import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CartService } from '../../../core/services/cart.service';
import { Observable, map } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLinkActive, RouterLink],
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.css']
})
export class NavbarComponent implements OnInit {
  cartItemCount$?: Observable<number>;
  
  constructor(
    public authService: AuthService,
    private cartService: CartService,
    private router: Router
  ) {
    console.log('User logged in?', this.authService.isLoggedIn());
    console.log('User token:', this.authService.getToken());
  }

  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.cartItemCount$ = this.cartService.getCart().pipe(
        map(cart => cart?.items?.length || 0)
      );
    }
  }

  get isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  get isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  get userName(): string {
    return this.authService.getUserName() || 'User';
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  goToHome(): void {
    this.router.navigate(['/']);
  }
}
