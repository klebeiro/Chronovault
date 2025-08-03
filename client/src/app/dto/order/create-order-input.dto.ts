import { PaymentTypeEnum } from '../../shared/enums';


export namespace CreateOrderInputDTO {
  export interface OrderInput {
    orderItems: OrderItem[];
    paymentCard: PaymentCard | null;
    paymentType: PaymentTypeEnum;
    installmentsNumber: number;
    address: ShipppingAddress;
  }

  export interface OrderItem {
    productId: number;
    quantity: number;
  }

  export interface PaymentCard {
    expirationYear: number;
    expirationMonth: number;
    cardHolderName: string;
    cvCode: number;
    cardNumber: string;
  }

  export interface ShipppingAddress {
    street: string;
    addressNumber: string;
    city: string;
    zipCode: string;
    state: string;
    country: string;
    complement: string;
  }
}
