import {Component, inject} from '@angular/core';
import { AuthService } from "../../services/auth.service";

@Component({
  selector: 'app-app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.css']
})
export class AppHeaderComponent {

  public authService = inject(AuthService);
}
