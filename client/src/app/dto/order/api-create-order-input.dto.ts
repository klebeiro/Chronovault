export namespace ApiCreateOrderInputDTO {
  export interface OrderInput {
    orderItems: OrderItem[];
    creditCardInformation: CreditCardInformation | null;
    paymentMethod: number;
    shippingAddress: ShippingAddress;
  } 

  export interface OrderItem {
    productId: number;
    quantity: number;
  }

  export interface CreditCardInformation {
    cardholderName: string;
    cardNumber: string;
    cvv: number;
    expiryMonth: number;
    expiryYear: number;
    installmentCount: number;
  }

  export interface ShippingAddress {
    street: string;
    number: string;
    city: string;
    zipCode: string;
    state: string;
    country: string;
    complement: string;
  }
}
