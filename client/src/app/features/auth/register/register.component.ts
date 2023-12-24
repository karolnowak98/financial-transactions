import {Component, inject} from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { NgIf } from "@angular/common";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthService } from "../../../shared/services/auth.service";
import { CustomValidators}  from "../../../shared/utils/custom-validators";
import { RegisterDto } from "../../../shared/interfaces/dtos/register-dto.interface";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  standalone: true,
  imports: [
    NgIf,
    ReactiveFormsModule,
    RouterLink
  ],
  styleUrls: ['./register.component.css']
})

export class RegisterComponent {

  private authService = inject(AuthService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  userRegisterDto : RegisterDto = { firstName: '', lastName:'', password:'', email:''};
  form: FormGroup;
  formSubmitted: boolean = false;
  showPassword: boolean = false;
  modalMessage = '';
  modalTitle = '';

  constructor() {
    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(10), CustomValidators.password()]]
    });

    if (this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/Transactions');
    }
  }

  onRegister() {
    this.formSubmitted = true;

    if (!this.form.valid) return;

    this.userRegisterDto = { ...this.form.value };
    this.authService.register(this.userRegisterDto).pipe(
      catchError((errorResponse) => {
        this.modalTitle = "Error!";
        this.modalMessage = errorResponse.error.message;
        return of(null);
      })
    ).subscribe({
      next: (response) => {
        if (response && response.success) {
          this.modalTitle = "Success!";
          this.modalMessage = "Successfully created User";
        }
      }
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  redirectToLogin() {

    this.router.navigateByUrl('/')
  }

  tryAgain() {
    this.formSubmitted = false;
    this.form.reset();
  }
}
