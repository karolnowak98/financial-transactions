import { Component, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { NgClass, NgIf, NgStyle } from "@angular/common";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { EMPTY, finalize, Subscription} from 'rxjs';
import { catchError } from 'rxjs/operators';

import { ModalButtonConfig } from "../../../shared/interfaces/configs/modal-button-config.interface";
import { RegisterDto } from "../../../shared/interfaces/dtos/register-dto.interface";
import { LoaderService } from "../../../shared/services/loader.service";
import { AuthService } from "../../../shared/services/auth.service";
import { SharedModule } from "../../../shared/shared.module";
import { CustomValidators } from "../../../shared/utils/custom-validators";
import { ModalComponent } from "../../../shared/components/modal/modal.component";
import { ModalButtonClassType } from "../../../shared/enums/modal-button-class-type.enum";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, RouterLink, NgStyle, NgClass, SharedModule]
})

export class RegisterComponent implements OnInit, OnDestroy{
  @ViewChild(ModalComponent, { static: false }) registerModal?: ModalComponent;

  readonly authService = inject(AuthService);
  readonly loaderService = inject(LoaderService);
  readonly fb = inject(FormBuilder);
  readonly router = inject(Router);

  readonly form: FormGroup;
  readonly successModalButtons: ModalButtonConfig[] = [
    { label: 'Log in now', action: 'loginNow', classType: ModalButtonClassType.Primary },
    { label: 'Close', action: 'close', classType: ModalButtonClassType.Danger }
  ];
  readonly failureModalButtons: ModalButtonConfig[] = [
    { label: 'Try again', action: 'tryAgain', classType: ModalButtonClassType.Primary },
    { label: 'Close', action: 'close', classType: ModalButtonClassType.Danger }
  ];
  modalButtons: ModalButtonConfig[] = [
    { label: 'Log in now', action: 'loginNow', classType: ModalButtonClassType.Primary },
    { label: 'Close', action: 'close', classType: ModalButtonClassType.Danger }
  ];
  userRegisterDto : RegisterDto = { firstName: '', lastName:'', password:'', email:''};
  loadingSubscription?: Subscription;
  formSubmitted: boolean = false;
  showPassword: boolean = false;
  isLoading: boolean = false;
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

  ngOnInit(): void {
    this.loadingSubscription = this.loaderService.loadingState$.subscribe(isLoading => {
      this.isLoading = isLoading;
    });
  }

  ngOnDestroy(): void {
    this.loadingSubscription?.unsubscribe();
  }

  onModalBtnClick(action: string) {
    this.registerModal?.hideModal();

    if (action === 'tryAgain') {
      this.form.markAsDirty();
      this.onRegister();
    } else if (action === 'loginNow') {
      this.redirectToLogin();
    }
  }

  onRegister() {
    this.formSubmitted = true;

    if (this.isLoading || !this.form.valid) return;

    this.loaderService.setLoadingState(true);
    this.userRegisterDto = { ...this.form.value };

    this.authService.register(this.userRegisterDto)
      .pipe(
        catchError((errorResponse) => {
          this.modalTitle = "Error!";
          this.modalButtons = this.failureModalButtons;

          if (errorResponse.status === 0) {
            this.modalMessage = "Server does not respond!";
          } else if (errorResponse.error.errors) {
            const errorMessage = Object.keys(errorResponse.error.errors)
              .map(field => {
                const errorsForField = errorResponse.error.errors[field];
                const formattedErrors = Array.isArray(errorsForField)
                  ? errorsForField.join(', ') : errorsForField;
                return `${formattedErrors}`;
              })
              .join('<br>');
            this.modalMessage = `Couldn't register user because: <br> ${errorMessage}`;
          } else {
            this.modalMessage = errorResponse.error;
          }

          this.registerModal?.showModal();
          return EMPTY;
        }),
        finalize(() => {
          this.loaderService.setLoadingState(false);
        })
      ).subscribe({
      next: (_: any) => {
        this.modalTitle = "Success!";
        this.modalMessage = "Successfully created User";
        this.modalButtons = this.successModalButtons;
        this.registerModal?.showModal();
      }
    });
  }

  togglePasswordVisibility() {this.showPassword = !this.showPassword;}
  redirectToLogin() {this.router.navigateByUrl('/')}
}
