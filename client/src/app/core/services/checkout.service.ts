import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { DeliveryMethod } from '../../shared/models/delivery-method';
import { Observable, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  deliveryMethods: DeliveryMethod[] = [];

  getDeliveryMethods(): Observable<DeliveryMethod[]> {
    if (this.deliveryMethods.length > 0) {
      return of(this.deliveryMethods);
    }

    return this.http
      .get<DeliveryMethod[]>(`${this.baseUrl}/payments/delivery-methods`)
      .pipe(
        tap((methods) => {
          this.deliveryMethods = methods.sort((a, b) => b.price - a.price); // sort from highest price to lowest price
          return methods;
        })
      );
  }
}
