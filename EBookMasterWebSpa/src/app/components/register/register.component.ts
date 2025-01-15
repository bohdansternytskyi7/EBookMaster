import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoadingService } from 'src/app/services/loading.service';
import { finalize } from 'rxjs/operators';
import { AuthService } from 'src/app/services/auth.service';
import { UserSubscription } from '../models/user-subscription';
import { SubscriptionType } from 'src/app/enums/subscription-type';
import { SubscriptionPeriod } from 'src/app/enums/subscription-period';
import { BorrowingService } from 'src/app/services/borrowing.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['../login/login.component.scss']
})
export class RegisterComponent implements OnInit, OnDestroy {
  @Input() subscriptions: UserSubscription[] = [];

  private subs: Subscription[] = [];

  registerForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private loadingService: LoadingService,
    private borrowingService: BorrowingService,
    private router: Router
  ) {
    this.registerForm = this.formBuilder.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      subscriptionId: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.borrowingService.loadSubscriptions();
    this.subs.push(this.borrowingService.subscriptions$.subscribe(subscriptions => this.subscriptions = subscriptions));
  }

  ngOnDestroy(): void {
    this.subs.forEach(subscription => subscription.unsubscribe());
  }

  onSubmit(): void {
    if (!this.registerForm.valid)
      return;

    this.loadingService.showLoading();
    this.authService.register(this.registerForm.value).pipe(
      finalize(() => this.loadingService.hideLoading())
    ).subscribe({
      next: (response) => {
        this.loadingService.showMessage('Rejestracja udana.');
        this.router.navigate(['/login']);
      },
      error: (error) => {
        this.loadingService.showErrorMessage('Wystąpił błąd podczas rejestracji.');
      }
    });
  }

  getSubscriptionTypeName(type: SubscriptionType): string {
    return SubscriptionType[type];
  }

  getSubscriptionPeriodName(period: SubscriptionPeriod): string {
    return SubscriptionPeriod[period];
  }

  trackBySubscriptionId(index: number, subscription: UserSubscription): number {
    return subscription.id;
  }
}
