import { Component, Output } from '@angular/core';
import { EventEmitter } from 'stream';

@Component({
  selector: 'app-products',
  imports: [],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {

@Output() productSelected = new EventEmitter<any>();

buy(Products: any)
{
  this.productSelected.emit(product);
}
}
