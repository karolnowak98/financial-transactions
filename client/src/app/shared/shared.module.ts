import { NgModule } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { AppHeaderComponent } from "./components/app-header/app-header.component";
import { AppFooterComponent } from "./components/app-footer/app-footer.component";

@NgModule({
  declarations: [AppFooterComponent, AppHeaderComponent],
  exports: [
    AppFooterComponent,
    AppHeaderComponent
  ],
  imports: [
    CommonModule,
    NgOptimizedImage
  ]
})
export class SharedModule { }
