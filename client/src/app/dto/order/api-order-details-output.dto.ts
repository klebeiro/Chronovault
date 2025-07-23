export namespace ApiOrderDetailsOutputDTO {
  export interface Order {
    id: number;
    orderNumber: string;
    userId: number;
    orderItems: OrderItem[];
    totalAmount: number;
    status: number;
    paymentStatus: number;
    paymentDate: any;
    paymentMethod: number;
    createdAt: string;
    updatedAt: any;
    shippingAddress: ShippingAddress;
    cardholderName: string;
    lastFourDigits: string;
    paymentToken: string;
    expiryMonth: number;
    expiryYear: number;
    installmentCount: number;
  }

  export interface OrderItem {
    id: number;
    productId: number;
    quantity: number;
    price: number;
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
