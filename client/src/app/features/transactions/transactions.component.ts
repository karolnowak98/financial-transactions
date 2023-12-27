import {Component, ElementRef, inject, OnInit, ViewChild} from '@angular/core';
import {DatePipe, NgClass, NgForOf, NgIf} from "@angular/common";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";

import {TransactionService} from "../../services/transaction.service";
import {TransactionDto} from "../../shared/interfaces/dtos/transactions/transaction-dto.interface";
import {NewTransactionDto} from "../../shared/interfaces/dtos/transactions/new-transaction-dto.interface";

@Component({
  selector: 'app-transactions',
  styleUrls: ['./transactions.component.css'],
  templateUrl: './transactions.component.html',
  standalone: true,
  imports: [NgForOf, NgIf, DatePipe, ReactiveFormsModule, NgClass]
})
export class TransactionsComponent implements OnInit {
  @ViewChild('newTransactionModal', {static: false}) newTransactionModal?: ElementRef;

  private transactionService = inject(TransactionService);
  private fb = inject(FormBuilder);

  showIncome = this.fb.control(true);
  showExpense = this.fb.control(true);
  selectedCategory = this.fb.control('');
  selectedDateFilter = this.fb.control('');

  newTransactionForm: FormGroup;
  formSubmitted: boolean = false;

  transactions: TransactionDto[] = [];
  categories: string[] = [];

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
    this.transactionService.getAllTransactions()
      .subscribe((transactionsDtos) => {
        this.transactions = transactionsDtos;
        this.categories = this.extractCategories(this.transactions);
      });
  }

  onAddTransaction(): void {
    this.formSubmitted = true;

    if(!this.newTransactionForm.valid) return;

    const newTransactionDto: NewTransactionDto = {
      amount: this.newTransactionForm.get('amount')!.value,
      dateTime: new Date(),
      categoryType: this.newTransactionForm.get('categoryType')!.value === ''
        ? null : this.newTransactionForm.get('categoryType')!.value,
      description: this.newTransactionForm.get('description')!.value ?? null
    }

    //TODO Add error handling
    this.transactionService.addTransaction(newTransactionDto).subscribe();
    this.newTransactionForm.reset();
    this.hideModal();
  }

  onDeleteTransaction(transactionId: string): void {
    this.transactionService.deleteTransaction(transactionId).subscribe();
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
        return (this.showIncome.value && isIncome) || (this.showExpense.value && !isIncome);
      })
      .filter(transaction => !this.selectedCategory.value || transaction.categoryType === this.selectedCategory.value)
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
}
