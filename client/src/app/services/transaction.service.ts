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
  readonly getAllUrl = environment.httpsUrl + '/transactions';
  readonly addTransactionUrl = environment.httpsUrl + '/transactions/create';
  readonly deleteTransactionUrl = environment.httpsUrl + '/transactions/delete';

  getAllTransactions() : Observable<any> {
    const token = localStorage.getItem('jwt');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    })

    return this.http.get<any>(this.getAllUrl, { headers });
  }

  addTransaction(newTransaction: NewTransactionDto): Observable<TransactionDto> {
    const token = localStorage.getItem('jwt');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.post<TransactionDto>(this.addTransactionUrl, newTransaction, { headers });
  }

  deleteTransaction(transactionId: string): Observable<any> {
    const url = `${this.deleteTransactionUrl}/${transactionId}`;
    const token = localStorage.getItem('jwt');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.delete<any>(url, { headers });
  }
}
