import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  template:`
    <div>
      <h1>Welcome to My Component</h1>
      <p>This is an external template file.</p>

    </div>
    <input [{ng model}]="name" placeholder="Enter Your Name"/>
    <p>hello {{name}}!</p>`
    ,

  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('project2');
}

// constructor() {

// }
// name: string = 'Angular templete';