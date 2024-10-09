import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [ProductItemComponent, MatButtonModule, MatIconModule],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog);
  title = 'Glamora';
  products: Product[] = [];
  selectedBrands: string[] = [];
  selectedTypes: string[] = [];

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.shopService.getProducts().subscribe({
      next: (res) => {
        this.products = res.items;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  openFiltersDialog(): void {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.selectedBrands,
        selectedTypes: this.selectedTypes,
      },
    });

    dialogRef.afterClosed().subscribe({
      next: (result) => {
        if (result) {
          console.log(result);
          this.selectedBrands = result.selectedBrands;
          this.selectedTypes = result.selectedTypes;

          this.shopService
            .getProducts(this.selectedBrands, this.selectedTypes)
            .subscribe({
              next: (res) => {
                this.products = res.items;
              },
              error: (err) => {
                console.log(err);
              },
            });
        }
      },
    });
  }
}
