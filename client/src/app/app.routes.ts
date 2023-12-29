import { Routes } from '@angular/router';

import { RegisterComponent } from "./features/auth/register/register.component";
import { LoginComponent } from "./features/auth/login/login.component";
import { TransactionsComponent } from "./features/transactions/transactions.component";

export const routes: Routes = [
  { path: 'auth/login', component: LoginComponent },
  { path: 'auth/register', component: RegisterComponent },
  { path: 'transactions', component: TransactionsComponent },
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' }
];
