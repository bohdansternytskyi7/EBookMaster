import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { BorrowingService } from 'src/app/services/borrowing.service';
import { BookBorrowing } from '../models/book-borrowing';
import { Author } from '../models/author';
import { MatDialog } from '@angular/material/dialog';
import { InfoDialogComponent } from '../info-dialog/info-dialog.component';

@Component({
  selector: 'app-borrowing-page',
  templateUrl: './borrowing-page.component.html',
  styleUrls: ['./borrowing-page.component.scss']
})
export class BorrowingPageComponent implements OnInit, OnDestroy {
  private subs: Subscription[] = [];

  borrowings: BookBorrowing[] = [];

  constructor(
    public dialog: MatDialog,
    private borrowingService: BorrowingService
  ) { }

  ngOnInit(): void {
    this.borrowingService.loadBorrowingHistory();
    this.subs.push(this.borrowingService.borrowings$.subscribe(borrowings => this.borrowings = borrowings));
  }

  ngOnDestroy(): void {
    this.subs.forEach(subscription => subscription.unsubscribe());
  }

  getAuthors(authors: Author[] | undefined): string {
    return this.borrowingService.getAuthors(authors);
  }

  openDialog(borrowing: BookBorrowing): void {
    this.dialog.open(InfoDialogComponent, {
      data: borrowing
    });
  }
}
