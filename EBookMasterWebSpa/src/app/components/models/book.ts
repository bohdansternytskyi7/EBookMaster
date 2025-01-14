import { Author } from "./author";
import { BookBorrowing } from "./book-borrowing";
import { Category } from "./category";
import { PublishingHouse } from "./publishing-house";
import { Recommendation } from "./recommendation";
import { Series } from "./series";

export interface Book {
  id: number;
  title: string;
  publicationYear: Date;
  publishingHouse: PublishingHouse;
  series: Series | undefined;
  authors: Author[] | undefined;
  categories: Category[] | undefined;
  bookBorrowings: BookBorrowing[] | undefined;
  recommendations: Recommendation[] | undefined;
  borrowed: boolean;
  notAllowed: boolean;
}
