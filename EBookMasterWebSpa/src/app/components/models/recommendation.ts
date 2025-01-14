import { Book } from "./book";

export interface Recommendation {
  id: number;
  recommendedBooks: Book[] | undefined;
  issueDate: Date;
  description: string;
}
