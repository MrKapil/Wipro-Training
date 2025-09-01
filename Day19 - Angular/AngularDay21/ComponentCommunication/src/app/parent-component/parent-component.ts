import { Component } from '@angular/core';
import { ChildComponent } from '../child-component/child-component';

@Component({
  selector: 'app-parent-component',
  imports: [ChildComponent],
  templateUrl: './parent-component.html',
  styleUrl: './parent-component.css'
})
export class ParentComponent {
  //myInputMessage:string = "I am the Child Component";
  user = {
    name: 'Alice',
    age:30,
  };
  //this is for @output decorator
  recieveNotification(message:string){
    console.log('Recived from child:', message)
  }
  
}
