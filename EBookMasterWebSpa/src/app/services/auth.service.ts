import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44395/accounts';
  private _isLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  isLoggedIn$: Observable<boolean> = this._isLoggedIn.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  login(credentials: { email: string, password: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials);
  }

  register(user: { name: string, surname: string, email: string, password: string, subscription: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, user);
  }

  setLoggedIn(value: boolean): void {
    this._isLoggedIn.next(value);
  }

  saveToken(token: string): void {
    sessionStorage.setItem('accessToken', token);
    setTimeout(() => {
      this.logout();
    }, 900000);
  }

  getToken(): string | null {
    return sessionStorage.getItem('accessToken');
  }

  logout(): void {
    sessionStorage.removeItem('accessToken');
    this._isLoggedIn.next(false);
    this.router.navigate(['/']);
  }

  tryGetToken(): void {
    const token = sessionStorage.getItem('accessToken');
    if (token) {
      this._isLoggedIn.next(true);
      return;
    }
    this._isLoggedIn.next(false);
  }
}
