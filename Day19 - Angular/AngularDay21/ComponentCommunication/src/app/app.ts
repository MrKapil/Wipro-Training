import { Component, signal } from '@angular/core';
import { ParentComponent } from './parent-component/parent-component';

@Component({
  selector: 'app-root',
  imports: [ParentComponent],
  template:`
  <app-parent-component></app-parent-component>

  ` ,
  styleUrls: ['./app.css']
})
export class App {
  protected readonly title = signal('ComponentCommunication');
}
