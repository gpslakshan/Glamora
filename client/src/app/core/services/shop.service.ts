import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { PagedResult } from '../../shared/models/paged-result';
import { Product } from '../../shared/models/product';
import { Observable } from 'rxjs';
import { ShopParams } from '../../shared/models/shop-params';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:7073/api';
  brands: string[] = [];
  types: string[] = [];

  getProducts(shopParams: ShopParams): Observable<PagedResult<Product>> {
    let params = new HttpParams();

    if (shopParams.searchTerm) {
      params = params.append('searchTerm', shopParams.searchTerm);
    }

    if (shopParams.brands.length > 0) {
      params = params.append('brand', shopParams.brands.join(','));
    }

    if (shopParams.types.length > 0) {
      params = params.append('type', shopParams.types.join(','));
    }

    if (shopParams.sort) {
      params = params.append('sort', shopParams.sort);
    }

    params = params.append('pageNumber', shopParams.pageNumber);
    params = params.append('pageSize', shopParams.pageSize);

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

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/products/${id}`);
  }
}
