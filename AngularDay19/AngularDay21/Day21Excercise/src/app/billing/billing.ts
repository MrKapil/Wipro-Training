import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-billing',
  imports: [],
  templateUrl: './billing.html',
  styleUrl: './billing.css'
})
export class Billing {
@Input() details:any;
}
