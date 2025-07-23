import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

import {
  PaymentTypeOption,
  PAYMENT_TYPE_OPTIONS,
} from './select-payment-method.types';
import {
  PaymentCardFormComponent,
  PaymentCardFormGroup,
} from '../payment-card-form/payment-card-form.component';
import { PaymentTypeEnum } from '../../shared/enums';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'select-payment-method',
  imports: [
    CommonModule,
    MatIconModule,
    PaymentCardFormComponent,
    MatButtonModule,
  ],
  templateUrl: './select-payment-method.component.html',
  styleUrl: './select-payment-method.component.scss',
})
export class SelectPaymentMethodComponent {
  @Input('paymentCardForm') paymentCardForm: PaymentCardFormGroup | null = null;
  @Input('selectedPaymentType') selectedPaymenType: PaymentTypeEnum =
    PaymentTypeEnum.DebitCard;
  @Output('onSubmitValidForm') onSubmitValidForm = new EventEmitter<void>();
  @Output('changeSelectedPaymentMethod') onChangeSelectedPaymentMethod =
    new EventEmitter<PaymentTypeEnum>();

  paymentTypeOptions: PaymentTypeOption[] = PAYMENT_TYPE_OPTIONS;

  changeSelectedPaymentOption(option: PaymentTypeOption) {
    this.onChangeSelectedPaymentMethod.emit(option.value);
  }

  getIsCardPayment() {
    return (
      this.selectedPaymenType === PaymentTypeEnum.CreditCard ||
      this.selectedPaymenType === PaymentTypeEnum.DebitCard
    );
  }

  getIsTicketPayment() {
    return this.selectedPaymenType === PaymentTypeEnum.Ticket;
  }

  getIsPixPayment() {
    return this.selectedPaymenType === PaymentTypeEnum.Pix;
  }

  emitOnSubmitValidForm() {
    if (this.getIsCardPayment() && !this.getIsValidPaymentCardForm()) {
      return;
    }

    this.onSubmitValidForm.emit();
  }

  getIsValidPaymentCardForm() {
    return (
      this.getIsCardPayment() &&
      this.paymentCardForm !== null &&
      this.paymentCardForm.valid
    );
  }
}
