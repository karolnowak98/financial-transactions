import { TransactionCategoryType } from "../../../enums/transaction-category-type.enum";

export interface TransactionDto {
  id: string;
  userId: string;
  amount: number;
  dateTime: Date;
  description: string | null;
  categoryType: TransactionCategoryType | null;
}
