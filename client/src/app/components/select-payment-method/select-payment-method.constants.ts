import { PaymentTypeEnum } from '../../shared/enums';
import { PaymentTypeOption } from './select-payment-method.types';

export const PAYMENT_TYPE_OPTIONS: PaymentTypeOption[] = [
  {
    label: 'Crédito',
    value: PaymentTypeEnum.CreditCard,
    iconName: 'credit_card',
  },
  {
    label: 'Débito',
    value: PaymentTypeEnum.DebitCard,
    iconName: 'credit_card',
  },
  {
    label: 'Boleto',
    value: PaymentTypeEnum.Ticket,
    iconName: 'account_balance',
  },
  { label: 'PIX', value: PaymentTypeEnum.Pix, iconName: 'qr_code' },
];
