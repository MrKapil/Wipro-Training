import { Component } from '@angular/core';

@Component({
  selector: 'app-customer',
  imports: [],
  templateUrl: './customer.html',
  styleUrl: './customer.css'
})
export class customer {
  Customer_Name: string = 'Baburao Ganaprao Apte';
  Customer_Address: string = 'Kolhapur';
  Customer_Phone: number = 7894534165;

}
