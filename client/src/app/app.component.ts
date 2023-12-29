import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

import { SharedModule } from "./shared/shared.module";
import { LoaderService } from "./shared/services/loader.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  imports: [CommonModule, RouterOutlet, SharedModule]
})
export class AppComponent implements OnInit {
  readonly loaderService = inject(LoaderService);
  readonly title = 'Financial Transactions';

  ngOnInit(): void {
    this.loaderService.loadingState$.subscribe(isLoading => {
      const style = document.documentElement.style;

      if (isLoading) {
        style.setProperty('cursor', 'wait');
      } else {
        style.removeProperty('cursor');
      }
    });
  }
}
