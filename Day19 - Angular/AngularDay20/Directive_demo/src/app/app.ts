import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule],
  template: `
      <h1>{{ title() }}</h1>

      Hello {{ name }},

      <div *ngIf='showmessage'>
      <p>Welcome</p>
      </div>

      <table>
  @for (f of data; track f.id) {
    <tr>
      <td>{{ f.name }}</td>
    </tr>
  }
  @empty {
    <tr>
      <td>No data available</td>
    </tr>
  }
</table>

<div class="status-wrapper">
@switch (status){

@case ('pending'){
  <p>status: Pending Approved</p>

}
@case ('approved'){
  <p>Status: Pending Approval</p>
}
@case ('rejected'){
  <p>status: Pending Approval</p>
}
@default {
<p>status: Unkown</p>
}
}



<p>Company Name: {{ companyName | uppercase }}</p>  
<p>Purchase Date: {{ purchaseDate | date:'fullDate' }}</p>
<p>Total Amount: {{ totalAmount | currency:'USD':'symbol':'1.2-2' }}</p>
<p>Discount Rate: {{ discountRate | percent:'1.2-2' }}</p>
<p>Note: {{ note | slice:0:30 }}...</p>   
<p>Product ID: {{ productDetails.id }}</p>
<p>Product Name: {{ productDetails.name }}</p>  
<p>Product Specs: {{ productDetails.specs | json }}</p>

`,


})
export class App {
  protected readonly title = signal('Directive_demo');

  name: string = "Directive demo";

  showmessage=  true;

  data: any[] = [];

  status = 'approved';

  companyName = 'acme technologies';
  purchaseDate = new Date(2025, 7, 9);
  totalAmount = 12345.678;
  discountRate = 0.15;
  note = 'This is a limited-time offer for premium customers only.';
  productDetails = { id: 101, name: 'Laptop', specs: { ram: '16GB', cpu: 'i7' } };

  

}

