import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HomePageComponent } from './components/home-page/home-page.component';
import { BorrowingPageComponent } from './components/borrowing-page/borrowing-page.component';
import { RegisterComponent } from './components/register/register.component';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';
import { BorrowingService } from './services/borrowing.service';
import { LoadingService } from './services/loading.service';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { BookPageComponent } from './components/book-page/book-page.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomePageComponent,
    BorrowingPageComponent,
    BookPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    CommonModule
  ],
  providers: [
    AuthService,
    BorrowingService,
    LoadingService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
