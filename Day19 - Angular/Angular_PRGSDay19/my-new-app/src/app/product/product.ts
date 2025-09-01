import { Component } from '@angular/core';

@Component({
  selector: 'app-product',
  imports: [],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class product {

  productName: string = 'Dell';
  price: number = 10500;
  isAvailable: boolean = true;

  constructor(){
    this.productName = 'Dell G3 1500';
    this.price = 150000;
  }
message: string = 'All Products Are Here';

//method defination
greetUser(name: string): string{
  return `Hello, ${name}! ${this.message}`;

}
displayMessage(): void{
  console.log(this.message);
}

}
