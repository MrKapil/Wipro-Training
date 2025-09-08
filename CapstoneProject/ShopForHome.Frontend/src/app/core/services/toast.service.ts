import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface Toast {
  message: string;
  type: 'success' | 'error' | 'warning' | 'info';
  id: number;
}

@Injectable({ providedIn: 'root' })
export class ToastService {
  private toasts$ = new BehaviorSubject<Toast[]>([]);
  toasts = this.toasts$.asObservable();

  show(message: string, type: Toast['type'] = 'info', duration = 3000) {
    const toast: Toast = { message, type, id: Date.now() };
    this.toasts$.next([...this.toasts$.value, toast]);
    
    if (duration > 0) {
      setTimeout(() => this.remove(toast.id), duration);
    }
  }

  remove(id: number) {
    this.toasts$.next(this.toasts$.value.filter(t => t.id !== id));
  }
}
