import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { BorrowingPageComponent } from './components/borrowing-page/borrowing-page.component';
import { RegisterComponent } from './components/register/register.component';
import { BookPageComponent } from './components/book-page/book-page.component';

const routes: Routes = [
  { path: 'home', component: HomePageComponent },
  { path: 'books', component: BookPageComponent },
  { path: 'borrowings', component: BorrowingPageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
