import { Book } from "./book";

export interface BookBorrowing {
  id: number;
  borrowingDate: Date;
  returnDate: Date | undefined;
  book: Book;
}
