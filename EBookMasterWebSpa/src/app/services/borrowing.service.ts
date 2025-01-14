import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, finalize, Observable } from 'rxjs';
import { UserSubscription } from '../components/models/user-subscription';
import { LoadingService } from './loading.service';
import { BookBorrowing } from '../components/models/book-borrowing';
import { Book } from '../components/models/book';

@Injectable({
  providedIn: 'root'
})
export class BorrowingService {
  private apiUrl = 'https://localhost:44395/bookborrowing';
  private _subscriptions: BehaviorSubject<UserSubscription[]> = new BehaviorSubject<UserSubscription[]>([]);
  private _borrowings: BehaviorSubject<BookBorrowing[]> = new BehaviorSubject<BookBorrowing[]>([]);
  private _books: BehaviorSubject<Book[]> = new BehaviorSubject<Book[]>([]);

  subscriptions$: Observable<UserSubscription[]> = this._subscriptions.asObservable();
  borrowings$: Observable<BookBorrowing[]> = this._borrowings.asObservable();
  books$: Observable<Book[]> = this._books.asObservable();

  constructor(
    private http: HttpClient,
    private loadingService: LoadingService
  ) {}

  loadSubscriptions(): void {
    this.http.get<UserSubscription[]>(`${this.apiUrl}/subscriptions`).pipe(
      catchError(error => {
        this.loadingService.showErrorMessage('Wystąpił błąd podczas ładowania listy dostępnych subskrypcji.');
        return [];
      })
    ).subscribe({
        next: data => this._subscriptions.next(data as UserSubscription[])
      }
    );
  }

  loadBooks(): void {
    this.loadingService.showLoading();
    this.http.get<Book[]>(`${this.apiUrl}/books`).pipe(
      finalize(() => this.loadingService.hideLoading()),
      catchError(error => {
        this.loadingService.showErrorMessage('Wystąpił błąd podczas ładowania książek.');
        return [];
      })
    ).subscribe({
      next: data => this._books.next(data as Book[])
    });
  }

  loadBorrowingHistory(): void {
    this.loadingService.showLoading();
    this.http.get<BookBorrowing[]>(`${this.apiUrl}/history`).pipe(
      finalize(() => this.loadingService.hideLoading()),
      catchError(error => {
        this.loadingService.showErrorMessage('Wystąpił błąd podczas ładowania wypożyczeń.');
        return [];
      })
    ).subscribe({
      next: data => this._borrowings.next(data as BookBorrowing[])
    });
  }
}
