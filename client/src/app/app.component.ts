import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/product';
import { PagedResult } from './shared/models/paged-result';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:7073/api';
  title = 'Glamora';
  products: Product[] = [];

  ngOnInit(): void {
    this.http
      .get<PagedResult<Product>>(
        `${this.baseUrl}/products?PageNumber=1&PageSize=5`
      )
      .subscribe({
        next: (res) => {
          this.products = res.items;
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
}
