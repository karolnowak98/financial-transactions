import { Component, inject } from '@angular/core';

import { AuthService } from "../../services/auth.service";

@Component({
  selector: 'app-app-header',
  templateUrl: './app-header.component.html'
})
export class AppHeaderComponent {

  public authService = inject(AuthService);
}
