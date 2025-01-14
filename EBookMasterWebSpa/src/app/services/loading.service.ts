import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LoadingService {
  private _isLoading = new BehaviorSubject<boolean>(false);
  private _messageSubject = new BehaviorSubject<string | null>(null);
  private _errorMessageSubject = new BehaviorSubject<string | null>(null);
  private messageTimeoutId: ReturnType<typeof setTimeout> | null = null;
  private errorTimeoutId: ReturnType<typeof setTimeout> | null = null;

  isLoading$ = this._isLoading.asObservable();
  message$ = this._messageSubject.asObservable();
  errorMessage$ = this._errorMessageSubject.asObservable();

  showLoading() {
    this._isLoading.next(true);
  }

  hideLoading() {
    this._isLoading.next(false);
  }

  showMessage(message: string) {
    if (this.messageTimeoutId) {
      clearTimeout(this.messageTimeoutId);
    }

    this._messageSubject.next(message);
    this.messageTimeoutId = setTimeout(() => {
      this._messageSubject.next(null);
      this.messageTimeoutId = null;
    }, 5000);
  }

  showErrorMessage(message: string) {
    if (this.errorTimeoutId) {
      clearTimeout(this.errorTimeoutId);
    }

    this._errorMessageSubject.next(message);
    this.errorTimeoutId = setTimeout(() => {
      this._errorMessageSubject.next(null);
      this.errorTimeoutId = null;
    }, 5000);
  }

  clearErrorMessage() {
    if (this.errorTimeoutId) {
      clearTimeout(this.errorTimeoutId);
      this.errorTimeoutId = null;
    }
    this._errorMessageSubject.next(null);
  }

  clearMessage() {
    if (this.messageTimeoutId) {
      clearTimeout(this.messageTimeoutId);
      this.messageTimeoutId = null;
    }
    this._messageSubject.next(null);
  }
}
