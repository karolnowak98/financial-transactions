import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ToastContainerDirective, ToastrService } from "ngx-toastr";

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  standalone: true,
  imports: [ToastContainerDirective],
})
export class ToastComponent implements OnInit{
  @ViewChild(ToastContainerDirective, {static: true}) toastContainer?: ToastContainerDirective;

  readonly toastrService = inject(ToastrService);

  ngOnInit() {
    this.toastrService.overlayContainer = this.toastContainer;
  }
}
