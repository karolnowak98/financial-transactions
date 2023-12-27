import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { Router, RouterLink } from "@angular/router";
import { NgIf } from "@angular/common";

import { AuthService } from "../../../shared/services/auth.service";
import { LoginDto } from "../../../shared/interfaces/dtos/login-dto.interface";

@Component({
  selector: 'app-login',
  styleUrls: ['./login.component.css'],
  templateUrl: './login.component.html',
  standalone: true,
  imports: [NgIf, RouterLink, ReactiveFormsModule]
})

export class LoginComponent {

  private authService = inject(AuthService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  userLoginDto: LoginDto = { email: '', password: '' };

  form: FormGroup;
  formSubmitted: boolean = false;
  showPassword: boolean = false;

  constructor() {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });

    if (this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/transactions');
    }
  }

  onLogin() {
    this.formSubmitted = true;

    if (!this.form.valid) return;

    this.userLoginDto = { ...this.form.value };
    this.authService.login(this.userLoginDto)
      .subscribe({
        next: (jwt) => {
          if(jwt !== null)
            this.router.navigateByUrl('/transactions');
          }
        }
      )
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}