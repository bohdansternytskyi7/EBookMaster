import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { BorrowingService } from './borrowing.service';
import { LoadingService } from './loading.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44395/accounts';
  private _isLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private _isPremium: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  isLoggedIn$: Observable<boolean> = this._isLoggedIn.asObservable();
  isPremium$: Observable<boolean> = this._isPremium.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router,
    private borrowingService: BorrowingService,
    private loadingService: LoadingService
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

  setIsPremium(value: boolean): void {
    sessionStorage.setItem('isPremium', 'true');
    this._isPremium.next(value);
  }

  getToken(): string | null {
    return sessionStorage.getItem('accessToken');
  }

  logout(): void {
    this.loadingService.showLoading();
    this.http.post(`${this.apiUrl}/logout`, null).subscribe({
      next: (response) => {
        this.loadingService.hideLoading();
        this.loadingService.showMessage("Wylogowano pomyślnie.");
      }, error: (error) => {
        this.loadingService.hideLoading();
      }
    });
    sessionStorage.removeItem('accessToken');
    sessionStorage.removeItem('isPremium');
    this.borrowingService.clearAllSubjects();
    this._isLoggedIn.next(false);
    this._isPremium.next(false);
    this.router.navigate(['/']);
  }

  tryGetPremium(): void {
    const isPremium = sessionStorage.getItem('isPremium');
    if (isPremium === 'true')
      this._isPremium.next(true);
    else
      this._isPremium.next(false);
  }

  tryGetToken(): void {
    const token = sessionStorage.getItem('accessToken');
    if (token)
      this._isLoggedIn.next(true);
    else
      this._isLoggedIn.next(false);
  }
}
