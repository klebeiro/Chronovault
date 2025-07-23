import { PaymentTypeEnum } from "../shared/enums";

export interface PaymentMethodModel {
  name: string;
  type: PaymentTypeEnum;
  icon: string;
}