import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class CustomValidators {
  static password(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value: string = control.value || '';
      const hasLowerCase = /[a-z]/.test(value);
      const hasUpperCase = /[A-Z]/.test(value);
      const hasDigit = /\d/.test(value);
      const hasSpecialChar = /[@$!%*?&]/.test(value);

      if (!hasLowerCase) {
        return { lowercase: true };
      }

      if (!hasUpperCase) {
        return { uppercase: true };
      }

      if (!hasDigit) {
        return { digit: true };
      }

      if (!hasSpecialChar) {
        return { specialChar: true };
      }

      return null;
    };
  }
}
