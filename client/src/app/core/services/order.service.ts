import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CreateOrderDto, Order } from '../../shared/models/order';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  createOrder(orderToCreate: CreateOrderDto): Observable<Order> {
    return this.http.post<Order>(`${this.baseUrl}/orders`, orderToCreate);
  }

  getOrdersForUser(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.baseUrl}/orders`);
  }

  getOrderDetails(id: number): Observable<Order> {
    return this.http.get<Order>(`${this.baseUrl}/orders/${id}`);
  }
}