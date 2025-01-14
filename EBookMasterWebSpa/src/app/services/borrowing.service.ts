import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, finalize, Observable } from 'rxjs';
import { UserSubscription } from '../components/models/user-subscription';
import { LoadingService } from './loading.service';

@Injectable({
  providedIn: 'root'
})
export class BorrowingService {
  private apiUrl = 'https://localhost:44395/bookborrowing';
  private _subscriptions: BehaviorSubject<UserSubscription[]> = new BehaviorSubject<UserSubscription[]>([]);

  subscriptions$: Observable<UserSubscription[]> = this._subscriptions.asObservable();

  constructor(
    private http: HttpClient,
    private loadingService: LoadingService
  ) {}

  loadSubscriptions(): void {
    this.loadingService.showLoading();
    this.http.get<UserSubscription[]>(`${this.apiUrl}/subscriptions`).pipe(
      finalize(() => this.loadingService.hideLoading()),
      catchError(error => {
        this.loadingService.showErrorMessage('Wystąpił błąd podczas ładowania listy dostępnych subskrypcji.');
        return [];
      })
    ).subscribe({
        next: data => this._subscriptions.next(data as UserSubscription[])
      }
    );
  }
}
