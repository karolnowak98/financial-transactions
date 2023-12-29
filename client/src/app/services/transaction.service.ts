import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

import { environment } from "../shared/utils/environment";
import { NewTransactionDto } from "../shared/interfaces/dtos/transactions/new-transaction-dto.interface";
import { TransactionDto } from "../shared/interfaces/dtos/transactions/transaction-dto.interface";

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  readonly http = inject(HttpClient);
  readonly httpUrl = environment.httpsUrl + '/transactions';
  readonly headers = new HttpHeaders({
    Authorization: `Bearer ${localStorage.getItem('jwt')}`
  });

  getAllTransactions(): Observable<any> {
    return this.http.get<any>(this.httpUrl, { headers: this.headers });
  }

  addTransaction(newTransaction: NewTransactionDto): Observable<TransactionDto> {
    return this.http.post<TransactionDto>(this.httpUrl + '/create', newTransaction, { headers: this.headers });
  }

  deleteTransaction(transactionId: string): Observable<any> {
    const url = `${this.httpUrl}/delete/${transactionId}`;
    return this.http.delete<any>(url, { headers: this.headers });
  }
}
