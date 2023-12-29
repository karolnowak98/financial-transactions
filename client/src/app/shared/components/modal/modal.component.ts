import { Component, ElementRef, EventEmitter, inject, Input, Output, ViewChild } from '@angular/core';
import { ModalButtonConfig } from "../../interfaces/configs/modal-button-config.interface";
import { LoaderService } from "../../services/loader.service";

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html'
})
export class ModalComponent {
  @ViewChild('modal', { static: false }) modal?: ElementRef;
  @Input() modalTitle: string = '';
  @Input() modalMessage: string = '';
  @Input() modalButtons: ModalButtonConfig[] = [];
  @Output() buttonClicked: EventEmitter<string> = new EventEmitter<string>();

  protected readonly loaderService = inject(LoaderService);
  isModalVisible: boolean = false;

  hideModal(){
    if (this.isModalVisible) {
      (this.modal?.nativeElement as HTMLElement).style.display = 'none';
      document.body.classList.remove('modal-open');
      document.querySelector('.modal-backdrop')?.classList.remove('show');
      (document.querySelector('.modal-backdrop') as HTMLElement).style.display = 'none';
      document.querySelector('.modal-backdrop')?.remove();
      this.isModalVisible = false;
    }
  }

  showModal(){
    if (!this.isModalVisible) {
      (this.modal?.nativeElement as HTMLElement).style.display = 'block';
      (this.modal?.nativeElement as HTMLElement).classList.add('show');
      document.body.classList.add('modal-open');
      let element = document.createElement('div');
      element.className = 'modal-backdrop fade show';
      element.style.display = 'block';
      document.body.appendChild(element);
      this.isModalVisible = true;
    }
  }

  onButtonClicked(action: string) {this.buttonClicked.emit(action);}
}
