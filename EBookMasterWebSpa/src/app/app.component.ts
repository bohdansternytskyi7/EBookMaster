import { ChangeDetectorRef, Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from './services/auth.service';
import { LoadingService } from './services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  private subscriptions: Subscription[] = [];

  isLoggedIn: boolean = false;
  isAdmin: boolean = false;
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
    this.subscriptions.push(this.authService.isLoggedIn$.subscribe(value => this.isLoggedIn = value));
    this.subscriptions.push(this.loadingService.message$.subscribe(msg => this.message = msg));
    this.subscriptions.push(this.loadingService.errorMessage$.subscribe(msg => this.errorMessage = msg));
    this.subscriptions.push(this.loadingService.isLoading$.subscribe(value => {
      this.isLoading = value;
      this.changeDetectorRef.detectChanges();
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  logout(): void {
    this.authService.logout();
  }
}
