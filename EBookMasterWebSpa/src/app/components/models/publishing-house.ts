import { Book } from "./book";

export interface PublishingHouse {
  id: number;
  name: string;
  country: string;
  foundationDate: Date;
  books: Book[] | undefined;
}
