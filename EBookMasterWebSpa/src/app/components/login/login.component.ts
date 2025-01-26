import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoadingService } from 'src/app/services/loading.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private loadingService: LoadingService,
    private router: Router
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (!this.loginForm.valid)
      return;

    this.loadingService.showLoading();
    this.authService.login(this.loginForm.value).subscribe({
      next: (response) => {
        this.authService.saveToken(response.accessToken);
        this.authService.setLoggedIn(true);
        this.authService.setIsPremium(response.isPremium);
        this.loadingService.showMessage('Zalogowano pomyślnie.');
        this.router.navigate(['/books']);
      },
      error: (error) => {
        this.loadingService.hideLoading();
        this.loadingService.showErrorMessage('Niepoprawne dane logowania.');
      }
    });
  }
}
