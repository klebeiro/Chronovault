import { Injectable } from '@angular/core';
import { PaymentMethodModel } from '../../models';
import { PaymentTypeEnum } from '../enums';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  private paymentMethods: PaymentMethodModel[] = [
    {
      icon: 'credit_card',
      name: 'Cartão de crédito',
      type: PaymentTypeEnum.CreditCard,
    },
    {
      icon: 'credit_card',
      name: 'Cartão de débito',
      type: PaymentTypeEnum.DebitCard,
    },
    {
      icon: 'qr_code',
      name: 'Pix',
      type: PaymentTypeEnum.Pix,
    },
    {
      icon: 'account_balance',
      name: 'Boleto',
      type: PaymentTypeEnum.Ticket,
    },
  ];

  constructor() {}

  getPaymentMethods(): PaymentMethodModel[] {
    return this.paymentMethods;
  }

  getPaymentMethod(type: PaymentTypeEnum): PaymentMethodModel {
    const paymentMethod = this.paymentMethods.find(
      (method) => method.type === type
    );

    if (!paymentMethod) {
      throw new Error('Payment method not found');
    }

    return paymentMethod;
  }

  getIsCardPayment(type: PaymentTypeEnum): boolean {
    return (
      type === PaymentTypeEnum.CreditCard || type === PaymentTypeEnum.DebitCard
    );
  }
}
