import { Component, Input, Output } from '@angular/core';
import { EventEmitter } from 'stream';

@Component({
  selector: 'app-details',
  imports: [],
  templateUrl: './details.html',
  styleUrls: ['./details.css']
})
export class Details {

@Input() product:any;
@Output() goToBilling = new EventEmitter<any>();

updateQuantity(qty:number)
{
  this.product.quantity = qty;
  this.product.totalPrice = this.product.price * qty;
}
proceedToBilling(){
  this.goToBilling.emit(this.product);
}
}
