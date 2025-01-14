import { Book } from "./book";

export interface Series {
  id: number;
  name: string;
  isOver: boolean;
  firstBookPublicationDate: Date;
  books: Book[] | undefined;
}
