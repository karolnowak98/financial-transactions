import { ApplicationConfig } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { provideHttpClient } from "@angular/common/http";
import { provideAnimations } from "@angular/platform-browser/animations";

import { routes } from './app.routes';
import { provideToastr } from "ngx-toastr";

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes, withComponentInputBinding()),
    provideToastr({toastClass:'custom-toast', positionClass: 'toast-top-center', timeOut: 4000}),
    provideAnimations(), provideHttpClient()],
};
