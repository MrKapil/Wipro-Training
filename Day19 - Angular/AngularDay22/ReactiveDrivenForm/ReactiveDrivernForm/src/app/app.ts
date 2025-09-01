import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserForm } from './user-form/user-form';

@Component({
  selector: 'app-root',
  declarations: [app, UserForm]
  imports: [RouterOutlet, UserForm],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ReactiveDrivernForm');
}
