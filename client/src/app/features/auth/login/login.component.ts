import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { Router, RouterLink } from "@angular/router";
import { catchError } from "rxjs/operators";
import { of } from "rxjs";
import { NgIf } from "@angular/common";

import { AuthService } from "../../../shared/services/auth.service";
import { UserLoginDto } from "../../../shared/interfaces/dtos/user-login-dto.interface";

@Component({
  selector: 'app-login',
  styleUrls: ['./login.component.css'],
  templateUrl: './login.component.html',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, RouterLink]
})

export class LoginComponent {

  private authService = inject(AuthService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  userLoginDto: UserLoginDto = { email: '', password: '' };

  form: FormGroup;
  formSubmitted: boolean = false;
  showPassword: boolean = false;

  constructor() {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });

    if (this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/Transactions');
    }
  }

  onLogin() {
    this.formSubmitted = true;

    if (!this.form.valid) return;

    this.userLoginDto = { ...this.form.value };
    this.authService.login(this.userLoginDto).pipe(
      catchError((errorResponse) => {
        if(errorResponse.error instanceof ErrorEvent) {
          alert(errorResponse.error.message);
        } else {
          if(errorResponse.status === 0){
            alert("Api doesn't respond!");
          }
        }
        return of(null);
      })
    ).subscribe({
      next: (response) => {
        if (response && response.success) {
          this.router.navigateByUrl('/Transactions');
        }
      }
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}
