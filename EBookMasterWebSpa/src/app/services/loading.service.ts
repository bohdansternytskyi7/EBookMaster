import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LoadingService {
  private _isLoading = new BehaviorSubject<boolean>(false);
  private _showingMessage = new BehaviorSubject<boolean>(false);
  private _messageSubject = new BehaviorSubject<string | null>(null);
  private _errorMessageSubject = new BehaviorSubject<string | null>(null);
  private messageQueue: { message: string, error: boolean }[] = [];

  isLoading$: Observable<boolean> = this._isLoading.asObservable();
  showingMessage$: Observable<boolean> = this._showingMessage.asObservable();
  message$: Observable<string | null> = this._messageSubject.asObservable();
  errorMessage$: Observable<string | null> = this._errorMessageSubject.asObservable();

  showLoading() {
    this._isLoading.next(true);
  }

  hideLoading() {
    this._isLoading.next(false);
    if (!this._showingMessage.value)
      this.displayNextMessage();
  }

  private displayNextMessage() {
    if (this.messageQueue.length > 0) {
      this._showingMessage.next(true);
      const nextMessage = this.messageQueue.shift()!;

      if (nextMessage.error)
        this._errorMessageSubject.next(nextMessage.message);
      else
        this._messageSubject.next(nextMessage.message);

      setTimeout(() => {
        this._messageSubject.next(null);
        this._errorMessageSubject.next(null);
        this._showingMessage.next(false);
        this.displayNextMessage();
      }, 2000);
    }
  }

  showMessage(message: string) {
    if (this._isLoading.value || this._showingMessage.value) {
      this.messageQueue.push({message: message, error: false});
    } else {
      this._showingMessage.next(true);
      this._messageSubject.next(message);
      setTimeout(() => {
        this._messageSubject.next(null);
        this._showingMessage.next(false);
        this.displayNextMessage();
      }, 2000);
    }
  }

  showErrorMessage(message: string) {
    if (this._isLoading.value || this._showingMessage.value) {
      this.messageQueue.push({message: message, error: true});
    } else {
      this._showingMessage.next(true);
      this._errorMessageSubject.next(message);
      setTimeout(() => {
        this._errorMessageSubject.next(null);
        this._showingMessage.next(false);
        this.displayNextMessage();
      }, 2000);
    }
  }
}
