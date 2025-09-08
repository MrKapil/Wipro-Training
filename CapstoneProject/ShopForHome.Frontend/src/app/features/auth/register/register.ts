import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrls: ['./register.css']
})
export class RegisterComponent {
  fullName = '';
  email = '';
  password = '';
  confirmPassword = '';
  error = '';
  loading = false;

  constructor(private auth: AuthService, private router: Router) {}

  onRegister(): void {
    // Validation
    if (!this.fullName || !this.email || !this.password || !this.confirmPassword) {
      this.error = 'Please fill all fields';
      return;
    }

    if (this.password !== this.confirmPassword) {
      this.error = 'Passwords do not match';
      return;
    }

    if (this.password.length < 6) {
      this.error = 'Password must be at least 6 characters';
      return;
    }

    this.loading = true;
    this.error = '';

    this.auth.register(this.fullName, this.email, this.password).subscribe({
      next: (response) => {
        console.log('Registration successful', response);
        this.loading = false;
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.error('Registration failed', err);
        this.loading = false;
        this.error = err.error?.message || 'Registration failed. Please try again.';
      }
    });
  }
}
