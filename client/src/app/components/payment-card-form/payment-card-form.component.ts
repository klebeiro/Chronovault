import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { SharedModule } from '../../shared';
import { mask } from '../../shared/utils';
import { MatButtonModule } from '@angular/material/button';
import { SelectOption } from '../../shared/types';
import { PAYMENT_INSTALLMENT_OPTIONS } from './payment-card-form.constants';

export interface PaymentCardForm {
  cardNumber: FormControl<string>;
  expirationDate: FormControl<string>;
  cvCode: FormControl<string>;
  cardHolderName: FormControl<string>;
  installmentsNumber: FormControl<number | null>;
}

export type PaymentCardFormGroup = FormGroup<PaymentCardForm>;

@Component({
  selector: 'payment-card-form',
  imports: [ReactiveFormsModule, FormsModule, SharedModule, MatButtonModule],
  templateUrl: './payment-card-form.component.html',
  styleUrl: './payment-card-form.component.scss',
})
export class PaymentCardFormComponent {
  @Input('paymentCardForm') paymentCardForm: PaymentCardFormGroup | null =
    null;
  @Output("onSubmitValidForm") onSubmitValidForm = new EventEmitter<void>();

  installmentNumberOptions: SelectOption<number>[] =
    PAYMENT_INSTALLMENT_OPTIONS;

  getFormControl(controlName: string): FormControl {
    if (!this.paymentCardForm) {
      throw new Error('paymentCardForm is not defined');
    }

    return this.paymentCardForm.get(controlName) as FormControl;
  }

  getMask(maskName: string): string {
    switch (maskName) {
      case 'zipCode':
        return mask.zipCode;
      case 'cardNumber':
        return mask.cardNumber;
      case 'expirationDate':
        return mask.expirationDate;
      case 'cvCode':
        return mask.cvCode;
      default:
        return '';
    }
  }

  submitForm(){
    if(this.paymentCardForm?.valid){
      this.onSubmitValidForm.emit();
    }
  }

  emitOnSubmitValidForm(){
    if(this.paymentCardForm?.valid){
      this.onSubmitValidForm.emit();
    }
  }
}
