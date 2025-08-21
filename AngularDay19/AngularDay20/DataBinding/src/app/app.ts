import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('DataBinding');

  name: string = 'Raju';
  age:number = 25;


  //for property binding:-


  btnweight = 100;
  btnwidth=100;

  //for event binding

  addProduct()
  {
    console.log("Produt added Successfully");
  }

  firstname: string = "Kapil";
}


