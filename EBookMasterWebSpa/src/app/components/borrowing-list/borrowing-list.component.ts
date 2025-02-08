import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BookBorrowing } from '../models/book-borrowing';

@Component({
  selector: 'app-borrowing-list',
  templateUrl: './borrowing-list.component.html',
  styleUrls: ['./borrowing-list.component.scss']
})
export class BorrowingListComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public borrowings: BookBorrowing[]) { }

  ngOnInit(): void {}
}
