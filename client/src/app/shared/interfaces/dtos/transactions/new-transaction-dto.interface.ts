import { TransactionCategoryType } from "../../../enums/transaction-category-type.enum";

export interface NewTransactionDto {
  amount: number;
  dateTime: Date;
  description: string | null;
  categoryType: TransactionCategoryType | null;
}
