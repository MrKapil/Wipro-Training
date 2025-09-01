import { Component, Input, EventEmitter, Output } from '@angular/core';


@Component({
  selector: 'app-child-component',
  standalone: true,
  imports: [],
  templateUrl: './child-component.html',
  styleUrls: ['./child-component.css']
})
export class ChildComponent {

  //myInputMessage: string = "I Am th parent Component";

@Input () userData:any;
  ngOnInit() {
    if(!this.userData){
      console.error('No user data provided');
    }
  }

  //this os for child to parent communatiopn
  //we ca use EventEmmiter
@Output() notifyParent: EventEmitter<string> = new EventEmitter<string>();

sendNotification()
{
  const message = "Hello Parent , This is your Child";
  this.notifyParent.emit(message);
}

}
