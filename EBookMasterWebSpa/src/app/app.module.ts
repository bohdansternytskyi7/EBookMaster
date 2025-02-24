import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { InfoDialogComponent } from './components/info-dialog/info-dialog.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list'
import { BorrowingListComponent } from './components/borrowing-list/borrowing-list.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomePageComponent,
    BorrowingPageComponent,
    BookPageComponent,
    InfoDialogComponent,
    BorrowingListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    CommonModule,
    NoopAnimationsModule,
    MatDialogModule,
    MatButtonModule,
    MatListModule
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
