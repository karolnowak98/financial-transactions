import { NgModule } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';

import { AppHeaderComponent } from "./components/app-header/app-header.component";
import { AppFooterComponent } from "./components/app-footer/app-footer.component";
import { ModalComponent } from "./components/modal/modal.component";
import { LoadingDirective } from "./directives/loading.directive";

@NgModule({
  declarations: [AppFooterComponent, AppHeaderComponent, ModalComponent, LoadingDirective],
  exports: [
    AppFooterComponent,
    AppHeaderComponent,
    ModalComponent,
    LoadingDirective
  ],
  imports: [
    CommonModule,
    NgOptimizedImage
  ]
})
export class SharedModule { }
