import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from './services/auth.service';
import { LoadingService } from './services/loading.service';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  private subs: Subscription[] = [];

  isLoggedIn: boolean = false;
  isLoading: boolean = false;
  message: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private authService: AuthService,
    private loadingService: LoadingService,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.authService.tryGetToken();
    this.authService.tryGetPremium();
    this.subs.push(this.authService.isLoggedIn$.subscribe(isLoggedIn => this.isLoggedIn = isLoggedIn));
    this.subs.push(this.loadingService.message$.subscribe(message => this.message = message));
    this.subs.push(this.loadingService.errorMessage$.subscribe(errorMessage => this.errorMessage = errorMessage));
    this.subs.push(this.loadingService.isLoading$.subscribe(isLoading => {
      this.isLoading = isLoading;
      this.changeDetectorRef.detectChanges();
    }));
  }

  ngOnDestroy(): void {
    this.subs.forEach(sub => sub.unsubscribe());
  }

  logout(): void {
    this.authService.logout();
  }
}
