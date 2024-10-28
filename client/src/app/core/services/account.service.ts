import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Address, User } from '../../shared/models/user';
import { map, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);

  login(values: any): Observable<void> {
    let params = new HttpParams();
    params = params.append('useCookies', true);
    return this.http.post<void>(`${this.baseUrl}/login`, values, {
      params,
    });
  }

  register(values: any): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/account/register`, values);
  }

  getUserInfo(): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/account/user-info`).pipe(
      map((user) => {
        this.currentUser.set(user);
        return user;
      })
    );
  }

  logOut(): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/account/logout`, {});
  }

  updateAddress(address: Address): Observable<Address> {
    return this.http
      .post<Address>(`${this.baseUrl}/account/address`, address)
      .pipe(
        tap((address) => {
          this.currentUser.update((user) => {
            if (user) user.address = address;
            return user;
          });

          return address;
        })
      );
  }

  getAuthStatus(): Observable<{ isAuthenticated: boolean }> {
    return this.http.get<{ isAuthenticated: boolean }>(
      `${this.baseUrl}/account/auth-status`
    );
  }
}
