import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { Product } from './shared/models/product';
import { ShopService } from './core/services/shop.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  private shopService = inject(ShopService);
  title = 'Glamora';
  products: Product[] = [];

  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: (res) => {
        this.products = res.items;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
