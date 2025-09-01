import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Details } from './details/details';
import { Products } from './products/products';
import { Billing } from './billing/billing';
@Component({
  selector: 'app-root',
  imports: [Products, Details, Billing],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  protected readonly title = signal('Day21Excercise');

  selectedProduct: any = null;
  billingData:any = null;

  onProductSelected(Products:any)
  {
    this.selectedProduct = {Products, quantity:1, totalPrice:Products.price}
    this.billingData = null;
  }

  onBilling(details:any)
  {
    this.billingData = details;
  }
}
