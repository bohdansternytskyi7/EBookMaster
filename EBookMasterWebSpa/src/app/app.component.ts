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
  private subs: Subscription[] = [];

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
    this.subs.push(this.authService.isLoggedIn$.subscribe(value => this.isLoggedIn = value));
    this.subs.push(this.loadingService.message$.subscribe(msg => this.message = msg));
    this.subs.push(this.loadingService.errorMessage$.subscribe(msg => this.errorMessage = msg));
    this.subs.push(this.loadingService.isLoading$.subscribe(value => {
      this.isLoading = value;
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
