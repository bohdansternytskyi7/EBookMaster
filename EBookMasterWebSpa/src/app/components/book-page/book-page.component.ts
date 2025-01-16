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
  filteredBooks: Book[] = [];
  userIsPremium: boolean = false;
  filter: string = '';

  constructor(
    private authService: AuthService,
    private borrowingService: BorrowingService
  ) { }

  ngOnInit(): void {
    this.borrowingService.loadBooks();
    this.subs.push(this.authService.isPremium$.subscribe(isPremium => this.userIsPremium = isPremium));
    this.subs.push(this.borrowingService.books$.subscribe(books => {
      this.books = books;
      this.applyFilter();
    }));
  }

  ngOnDestroy(): void {
    this.subs.forEach(subscription => subscription.unsubscribe());
  }

  getAuthors(authors: Author[] | undefined): string {
    return this.borrowingService.getAuthors(authors);
  }

  getCategories(categories: Category[] | undefined): string {
    return this.borrowingService.getCategories(categories);
  }

  toggleBorrow(book: Book): void {
    if (book.borrowed)
      this.borrowingService.returnBook(book.id);
    else
      this.borrowingService.borrowBook(book.id);
  }

  applyFilter(): void {
    const query = this.filter.toLowerCase();
    this.filteredBooks = this.books.filter(book =>
      this.getAuthors(book.authors).toLowerCase().includes(query)
      || book.title.toLowerCase().includes(query)
      || this.getCategories(book.categories).toLowerCase().includes(query)
      || book.series?.name.toLowerCase().includes(query)
      || book.publishingHouse.name.toLowerCase().includes(query)
    );
  }
}
