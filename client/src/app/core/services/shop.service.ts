import { HttpClient, HttpParams } from '@angular/common/http';
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
  brands: string[] = [];
  types: string[] = [];

  getProducts(
    brands?: string[],
    types?: string[]
  ): Observable<PagedResult<Product>> {
    let params = new HttpParams();

    params = params.append('pageNumber', 1);
    params = params.append('pageSize', 10);

    if (brands && brands.length > 0) {
      params = params.append('brand', brands.join(','));
    }

    if (types && types.length > 0) {
      params = params.append('type', types.join(','));
    }

    return this.http.get<PagedResult<Product>>(`${this.baseUrl}/products`, {
      params,
    });
  }

  getBrands(): void {
    if (this.brands.length > 0) {
      return;
    }

    this.http.get<string[]>(`${this.baseUrl}/products/brands`).subscribe({
      next: (res) => {
        this.brands = res;
      },
    });
  }

  getTypes(): void {
    if (this.types.length > 0) {
      return;
    }

    this.http.get<string[]>(`${this.baseUrl}/products/types`).subscribe({
      next: (res) => {
        this.types = res;
      },
    });
  }
}
