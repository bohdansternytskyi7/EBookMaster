import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Book } from '../models/book';
import { BorrowingService } from 'src/app/services/borrowing.service';
import { Author } from '../models/author';
import { Category } from '../models/category';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-book-page',
  templateUrl: './book-page.component.html',
  styleUrls: ['./book-page.component.scss']
})
export class BookPageComponent implements OnInit, OnDestroy {
  private subs: Subscription[] = [];

  books: Book[] = [];
  userIsPremium: boolean = false;

  constructor(
    private authService: AuthService,
    private borrowingService: BorrowingService
  ) { }

  ngOnInit(): void {
    this.borrowingService.loadBooks();
    this.subs.push(this.authService.isPremium$.subscribe(isPremium => this.userIsPremium = isPremium));
    this.subs.push(this.borrowingService.books$.subscribe(books => this.books = books));
  }

  ngOnDestroy(): void {
    this.subs.forEach(subscription => subscription.unsubscribe());
  }

  getAuthors(authors: Author[] | undefined): string {
    return this.borrowingService.getAuthors(authors);
  }

  getCategories(categories: Category[] | undefined): string {
    if (!categories) return '';
    return categories.map(category => category.name).join(', ');
  }

  toggleBorrow(book: Book): void {
    if (book.borrowed)
      this.borrowingService.returnBook(book.id);
    else
      this.borrowingService.borrowBook(book.id);
  }
}
