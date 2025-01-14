import { Book } from "./book";

export interface Author {
  id: number;
  name: string;
  surname: string;
  dateOfBirth: Date;
  nationality: string;
  books: Book[] | undefined;
}
