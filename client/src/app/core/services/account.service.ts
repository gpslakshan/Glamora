import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Address, User } from '../../shared/models/user';
import { Observable } from 'rxjs';

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
      withCredentials: true,
    });
  }

  register(values: any): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/account/register`, values);
  }

  getUserInfo(): void {
    this.http
      .get<User>(`${this.baseUrl}/account/user-info`, { withCredentials: true })
      .subscribe({
        next: (user) => this.currentUser.set(user),
      });
  }

  logOut(): Observable<void> {
    return this.http.post<void>(
      `${this.baseUrl}/account/logout`,
      {},
      { withCredentials: true }
    );
  }

  updateAddress(address: Address): Observable<Address> {
    return this.http.post<Address>(`${this.baseUrl}/account/address`, address);
  }
}
