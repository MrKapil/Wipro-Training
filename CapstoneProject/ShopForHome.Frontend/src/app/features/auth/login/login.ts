import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class LoginComponent {
  email = '';
  password = '';
  error = '';
  loading = false;

  constructor(private auth: AuthService, private router: Router) {}

  onLogin(): void {
    if (!this.email || !this.password) {
      this.error = 'Please fill all fields';
      return;
    }

    this.loading = true;
    this.error = '';

    this.auth.login(this.email, this.password).subscribe({
      next: (response) => {
        console.log('Login successful', response);
        this.loading = false;
        // Redirect based on role
      const returnUrl = this.router.parseUrl(this.router.url).queryParams?.['returnUrl'];

      if (returnUrl) {
        this.router.navigateByUrl(returnUrl);
      } else if (response.role === 'Admin') {
        this.router.navigate(['/admin']);
      } else {
        this.router.navigate(['/home']); // This now goes to HomeComponent
      }
    },
      error: (err) => {
        console.error('Login failed', err);
        this.loading = false;
        this.error = err.error?.message || 'Invalid email or password';
      }
    });
  }
}
