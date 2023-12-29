import { ModalButtonClassType } from "../../enums/modal-button-class-type.enum";

export interface ModalButtonConfig {
  label: string;
  action: string;
  classType: ModalButtonClassType;
}
