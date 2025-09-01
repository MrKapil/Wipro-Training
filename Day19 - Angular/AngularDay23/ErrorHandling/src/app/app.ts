import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [],
  template: `
  <button (click)="dosomething()">Do Something</button>
<div *ngFor="errorMessage" class="error-message">
  {{ error>errorMessage }}
</div>
  `,
  styles: ['.error-message { color: red; }']
})
export class App {
  protected readonly title = signal('ErrorHandling');
  errorMessage: string | null = null;

  dosomething()
  {
    try{
      //simulating as error
      const data= JSON.parse('invalid json');
      console.log(data);
    }
    catch(error: any)
    {
      //handle the error
      this.errorMessage = 'An error occured: $(error.message)';
      console.log('Sysnchronous error is caught :', error)
    }
  }
}
