import { Component, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { Router, RouterLink } from "@angular/router";
import { NgClass, NgIf } from "@angular/common";
import { catchError } from "rxjs/operators";
import { finalize, of, Subscription } from "rxjs";

import { AuthService } from "../../../shared/services/auth.service";
import { LoginDto } from "../../../shared/interfaces/dtos/login-dto.interface";
import { ModalComponent } from "../../../shared/components/modal/modal.component";
import { ModalButtonConfig } from "../../../shared/interfaces/configs/modal-button-config.interface";
import { SharedModule } from "../../../shared/shared.module";
import { LoaderService } from "../../../shared/services/loader.service";
import { ModalButtonClassType } from "../../../shared/enums/modal-button-class-type.enum";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: true,
  imports: [NgIf, RouterLink, ReactiveFormsModule, NgClass, SharedModule]
})

export class LoginComponent implements OnInit, OnDestroy{
  @ViewChild(ModalComponent, { static: false }) loginModal?: ModalComponent;

  readonly authService = inject(AuthService);
  readonly loaderService = inject(LoaderService);
  readonly fb = inject(FormBuilder);
  readonly router = inject(Router);

  form: FormGroup;
  loadingSubscription?: Subscription;
  userLoginDto: LoginDto = { email: '', password: '' };
  modalButtons: ModalButtonConfig[] = [
    { label: 'Try again', action: 'tryAgain', classType: ModalButtonClassType.Primary },
    { label: 'Close', action: 'close', classType: ModalButtonClassType.Danger }
  ];
  formSubmitted: boolean = false;
  showPassword: boolean = false;
  isLoading: boolean = false;
  modalMessage = '';
  modalTitle = '';

  constructor() {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });

    if (this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/transactions');
    }
  }

  ngOnInit(): void {
    this.loadingSubscription = this.loaderService.loadingState$.subscribe(isLoading => {
      this.isLoading = isLoading;
    });
  }

  ngOnDestroy(): void {
    this.loadingSubscription?.unsubscribe();
  }

  onModalBtnClick(action: string) {
    this.loginModal?.hideModal();
    if (action === 'tryAgain') {
      this.form.markAsDirty();
      this.onLogin();
    }
  }

  onLogin() {
    this.formSubmitted = true;

    if (this.isLoading || !this.form.valid) return;

    this.loaderService.setLoadingState(true);
    this.userLoginDto = { ...this.form.value };

    this.authService.login(this.userLoginDto).pipe(
      catchError((errorResponse) => {
        this.modalTitle = "Error!";
        if (errorResponse.status === 0) {
          this.modalMessage = "Server does not respond.";
        } else {
          this.modalMessage = "Invalid credentials. Please check your email and password.";
        }
        this.loginModal?.showModal();
        return of(null);
      }),
      finalize(() => {
        this.loaderService.setLoadingState(false);
      })
    ).subscribe({
        next: (jwt) => {
          if(jwt !== null)
            this.router.navigateByUrl('/transactions');
          }
        }
      )
  }

  togglePasswordVisibility() {this.showPassword = !this.showPassword;}
}
