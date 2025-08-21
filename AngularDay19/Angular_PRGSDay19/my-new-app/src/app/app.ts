import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { product } from './product/product';
import { customer } from './customer/customer';
import { orders } from './orders/orders';
import { AuthModule } from './auth/auth-module';

@Component({
  selector: 'app-root',
  imports: [product, customer,orders, AuthModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('my-new-app');
}
