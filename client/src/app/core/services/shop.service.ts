import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { PagedResult } from '../../shared/models/paged-result';
import { Product } from '../../shared/models/product';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:7073/api';

  getProducts(): Observable<PagedResult<Product>> {
    return this.http.get<PagedResult<Product>>(
      `${this.baseUrl}/products?PageNumber=1&PageSize=5`
    );
  }
}
