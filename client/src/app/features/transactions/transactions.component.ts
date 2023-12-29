import { Component, ElementRef, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { DatePipe, NgClass, NgForOf, NgIf } from "@angular/common";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";

import { TransactionService } from "../../services/transaction.service";
import { TransactionDto } from "../../shared/interfaces/dtos/transactions/transaction-dto.interface";
import { NewTransactionDto } from "../../shared/interfaces/dtos/transactions/new-transaction-dto.interface";
import { LoaderService } from "../../shared/services/loader.service";
import { EMPTY, finalize, Subscription } from "rxjs";
import { catchError } from "rxjs/operators";
import { ToastrService } from 'ngx-toastr';
import { ToastComponent } from "../../shared/components/toast/toast.component";

@Component({
  selector: 'app-transactions',
  styleUrls: ['./transactions.component.css'],
  templateUrl: './transactions.component.html',
  standalone: true,
  imports: [NgForOf, NgIf, DatePipe, ReactiveFormsModule, NgClass, ToastComponent]
})
export class TransactionsComponent implements OnInit, OnDestroy {
  @ViewChild('newTransactionModal', {static: false}) newTransactionModal?: ElementRef;

  readonly transactionService = inject(TransactionService);
  readonly loaderService = inject(LoaderService);
  readonly toastsService = inject(ToastrService);
  readonly fb = inject(FormBuilder);
  readonly showIncome = this.fb.control(true);
  readonly showExpense = this.fb.control(true);
  readonly selectedCategory = this.fb.control('');
  readonly selectedDateFilter = this.fb.control('');

  loadingSubscription?: Subscription;
  newTransactionForm: FormGroup;
  transactions: TransactionDto[] = [];
  categories: string[] = [];
  formSubmitted: boolean = false;
  isLoading: boolean = false;

  constructor() {
    this.newTransactionForm = this.fb.group({
      amount: ['', [
        Validators.required,
        Validators.min(-9999999),
        Validators.max(9999999),
        Validators.pattern(/^(-?\d+(\.\d{1,2})?)?$/),
      ]],
      description: [''],
      categoryType: [null]
    });
  }

  ngOnInit(): void {
    this.loadTransactions();
    this.loadingSubscription = this.loaderService.loadingState$.subscribe(isLoading => {
      this.isLoading = isLoading;
    });
  }

  ngOnDestroy(): void {
    this.loadingSubscription?.unsubscribe();
  }

  hideModal(){
    (this.newTransactionModal?.nativeElement as HTMLElement).style.display = 'none';
    document.body.classList.remove('modal-open');
    document.querySelector('.modal-backdrop')?.classList.remove('show');
    (document.querySelector('.modal-backdrop') as HTMLElement).style.display = 'none';
    document.querySelector('.modal-backdrop')?.remove();
  }

  showModal(){
    (this.newTransactionModal?.nativeElement as HTMLElement).style.display = 'block';
    (this.newTransactionModal?.nativeElement as HTMLElement).classList.add('show');
    document.body.classList.add('modal-open');
    let element = document.createElement('div');
    element.className = 'modal-backdrop fade show';
    element.style.display = 'block';
    document.body.appendChild(element);
  }

  loadTransactions(): void {
    if(this.isLoading) return;

    this.loaderService.setLoadingState(true);

    this.transactionService.getAllTransactions()
      .pipe(
        catchError((errorResponse) => {
          this.handleHttpError(errorResponse);
          return EMPTY;
        }),
        finalize(() => {
          this.loaderService.setLoadingState(false);
        })
      )
      .subscribe((transactionsDtos) => {
        if (transactionsDtos != null) {
          this.transactions = transactionsDtos;
          this.categories = this.extractCategories(this.transactions);
        }
      });
  }

  onAddTransaction(): void {
    this.formSubmitted = true;

    if(this.isLoading || !this.newTransactionForm.valid) return;

    this.startLoading();

    const newTransactionDto: NewTransactionDto = {
      amount: this.newTransactionForm.get('amount')!.value,
      dateTime: new Date(),
      categoryType: this.newTransactionForm.get('categoryType')!.value === ''
        ? null : this.newTransactionForm.get('categoryType')!.value,
      description: this.newTransactionForm.get('description')!.value ?? null
    }

    this.transactionService.addTransaction(newTransactionDto).pipe(
      catchError((errorResponse) => {
        this.handleHttpError(errorResponse);
        return EMPTY;
      }),
      finalize(() => {
        this.stopLoading();
      })
    ).subscribe(()=>{
      this.showSuccessToast('Successfully add new transaction.');
      this.stopLoading();
      this.loadTransactions();
    });
    this.newTransactionForm.reset();
    this.hideModal();
  }

  onDeleteTransaction(transactionId: string): void {
    if(this.isLoading) return;

    this.startLoading();

    this.transactionService.deleteTransaction(transactionId).pipe(
      catchError((errorResponse) => {
        this.handleHttpError(errorResponse);
        return EMPTY;
      }),
      finalize(() => {
        this.stopLoading();
      })
    ).subscribe(()=>{
      this.showSuccessToast('Successfully delete transaction.');
      this.stopLoading();
      this.loadTransactions();
    });
  }

  getTotalAmount(): number {
    return this.filteredTransactions().reduce((sum, transaction) => sum + transaction.amount, 0);
  }

  extractCategories(transactions: TransactionDto[]): string[] {
    const uniqueCategories = new Set<string>();
    transactions.forEach(transaction => {
      if (transaction.categoryType) {
        uniqueCategories.add(transaction.categoryType);
      }
    });
    return Array.from(uniqueCategories);
  }

  filteredTransactions(): TransactionDto[] {
    return this.transactions
      .filter(transaction => {
        const isIncome = transaction.amount >= 0;
        const isUncategorized = !transaction.categoryType;

        return ((this.showIncome.value && isIncome) || (this.showExpense.value && !isIncome)) &&
          (!this.selectedCategory.value ||
            (isUncategorized && this.selectedCategory.value === 'Uncategorized') ||
            (!isUncategorized && transaction.categoryType === this.selectedCategory.value));
      })
      .filter(transaction => this.filterByDate(transaction));
  }

  filterByDate(transaction: TransactionDto): boolean {
    if (!this.selectedDateFilter) {
      return true;
    }

    const currentDate = new Date();
    switch (this.selectedDateFilter.value) {
      case 'thisYear':
        return new Date(transaction.dateTime).getFullYear() === currentDate.getFullYear();
      case 'thisMonth':
        return new Date(transaction.dateTime).getMonth() === currentDate.getMonth();
      case 'thisWeek':
        const startOfWeek = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() - currentDate.getDay());
        const endOfWeek = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() + (6 - currentDate.getDay()));
        const transactionDate = new Date(transaction.dateTime);
        return transactionDate >= startOfWeek && transactionDate <= endOfWeek;
      default:
        return true;
    }
  }

  handleHttpError(error: any): void {
    if (error.status === 0) {
      this.showErrorToast('Server does not respond!');
    }
  }

  showSuccessToast(message: string): void {this.toastsService.success(message, 'Success!');}

  showErrorToast(message: string): void {this.toastsService.error(message, 'Error!');}

  startLoading(): void {this.loaderService.setLoadingState(true);}

  stopLoading(): void {this.loaderService.setLoadingState(false);}
}
