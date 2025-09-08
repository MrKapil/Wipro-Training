import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'price',
  standalone: true
})
export class PricePipe implements PipeTransform {
  
  transform(value: number | string | null | undefined, currency: string = '₹'): string {
    if (value == null || value === '') {
      return `${currency}0.00`;
    }

    const numValue = typeof value === 'string' ? parseFloat(value) : value;
    
    if (isNaN(numValue)) {
      return `${currency}0.00`;
    }

    // Format number with Indian comma system (lakhs/crores)
    return `${currency}${this.formatIndianCurrency(numValue)}`;
  }

  private formatIndianCurrency(num: number): string {
    const formatted = num.toFixed(2);
    const parts = formatted.split('.');
    const wholePart = parts[0];
    const decimalPart = parts[1];

    // Indian number formatting (lakhs, crores)
    let result = '';
    let count = 0;
    
    for (let i = wholePart.length - 1; i >= 0; i--) {
      if (count > 0 && count % 2 === 0 && count !== wholePart.length - 1) {
        result = ',' + result;
      } else if (count === 3) {
        result = ',' + result;
      }
      result = wholePart[i] + result;
      count++;
    }

    return `${result}.${decimalPart}`;
  }
}
