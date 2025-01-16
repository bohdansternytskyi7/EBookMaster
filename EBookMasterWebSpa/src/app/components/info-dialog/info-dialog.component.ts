import { Component, Inject } from '@angular/core';
import { BookBorrowing } from '../models/book-borrowing';
import { Author } from '../models/author';
import { BorrowingService } from 'src/app/services/borrowing.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Category } from '../models/category';

@Component({
  selector: 'app-info-dialog',
  templateUrl: './info-dialog.component.html',
  styleUrls: ['./info-dialog.component.scss']
})
export class InfoDialogComponent {
  constructor(
    private borrowingService: BorrowingService,
    @Inject(MAT_DIALOG_DATA) public borrowing: BookBorrowing
  ) {}

  getAuthors(authors: Author[] | undefined): string {
    return this.borrowingService.getAuthors(authors);
  }

  getCategories(categories: Category[] | undefined): string {
    return this.borrowingService.getCategories(categories);
  }
}
