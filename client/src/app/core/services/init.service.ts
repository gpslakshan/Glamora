import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { forkJoin, Observable, of } from 'rxjs';
import { Cart } from '../../shared/models/cart';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  private cartService = inject(CartService);
  private accountService = inject(AccountService);

  init() {
    const cartId = localStorage.getItem('cart_id');
    const cart$ = cartId ? this.cartService.getCart(cartId) : of(null);

    return forkJoin({
      cart: cart$,
      user: this.accountService.getUserInfo(),
    });
  }
}
