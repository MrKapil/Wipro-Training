import { Component } from '@angular/core';
import { ToastService } from '@core/services/toast.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="toast-container">
      <div *ngFor="let toast of toastService.toasts | async" 
           [class]="'toast toast-' + toast.type"
           (click)="toastService.remove(toast.id)">
        {{ toast.message }}
      </div>
    </div>
  `,
  styles: [`
    .toast-container {
      position: fixed;
      top: 20px;
      right: 20px;
      z-index: 10000;
    }
    .toast {
      padding: 12px 20px;
      margin-bottom: 10px;
      border-radius: 6px;
      color: white;
      cursor: pointer;
      animation: slideIn 0.3s ease;
    }
    .toast-success { background: #4caf50; }
    .toast-error { background: #f44336; }
    .toast-warning { background: #ff9800; }
    .toast-info { background: #2196f3; }
    @keyframes slideIn {
      from { transform: translateX(100%); opacity: 0; }
      to { transform: translateX(0); opacity: 1; }
    }
  `]
})
export class ToastComponent {
  constructor(public toastService: ToastService) {}
}
