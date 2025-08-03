import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { OrderDetailsModel, OrderModel } from '../../models';
import { MOCK_ORDERS } from '../constants';
import { PaymentTypeEnum } from '../enums';
import {
  ApiCreateOrderInputDTO,
  ApiOrderDetailsOutputDTO,
  CreateOrderInputDTO,
} from '../../dto/order';
import { WebServiceConfigService } from './web-service-config.service';
import { PaymentService } from './payment.service';
import { ApiCompactOrderOutputDTO } from '../../dto/order/api-compact-order-output.dto';
import { AuthService } from '../../services';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(
    private webServiceConfigService: WebServiceConfigService,
    private paymentService: PaymentService,
    private httpClient: HttpClient,
    private authService: AuthService
  ) {}

  gerOrders(): Observable<OrderModel[]> {
    // simulates an http request
    return new Observable<OrderModel[]>((observer) => {
      observer.next(MOCK_ORDERS);
      observer.complete();
    });
  }

  getOrderDetails(orderId: number): Observable<OrderDetailsModel> {
    const completeApiUrl = this.webServiceConfigService.getCompleteApiUrl(
      `Order/${orderId}/details`
    );

    return this.httpClient
      .get<ApiOrderDetailsOutputDTO.Order>(completeApiUrl)
      .pipe(map((order) => this.mapApiOrderDetailsToModel(order)));
  }

  mapApiOrderDetailsToModel(
    apiOrderDTO: ApiOrderDetailsOutputDTO.Order
  ): OrderDetailsModel {
    const order: OrderDetailsModel = {
      id: apiOrderDTO.id,
      orderNumber: apiOrderDTO.orderNumber,
      date: new Date(apiOrderDTO.createdAt),
      installmentsNumber: apiOrderDTO.installmentCount,
      items: apiOrderDTO.orderItems.map((item) => ({
        item: {
          brand: item.productInformation.brand,
          id: item.id,
          imagesUrls: item.productInformation.images,
          model: item.productInformation.model,
          price: item.productInformation.price,
        },
        quantity: item.quantity,
      })),
      paymentCard: null,
      paymentMethod: apiOrderDTO.paymentMethod,
      total: apiOrderDTO.totalAmount,
    };

    if (apiOrderDTO.paymentMethod === PaymentTypeEnum.CreditCard || apiOrderDTO.paymentMethod === PaymentTypeEnum.DebitCard) {
      order.paymentCard = {
        cardHolderName: apiOrderDTO.cardholderName,
        cardNumber: `**** **** **** ${apiOrderDTO.lastFourDigits}`,
      };
    }

    return order;
  }

  createOrder(input: CreateOrderInputDTO.OrderInput): Observable<void> {
    const orderItems: ApiCreateOrderInputDTO.OrderItem[] = input.orderItems.map(
      (item) => ({
        productId: item.productId,
        quantity: item.quantity,
      })
    );

    const requestBody: ApiCreateOrderInputDTO.OrderInput = {
      creditCardInformation: null,
      orderItems,
      paymentMethod: input.paymentType,
      shippingAddress: {
        city: input.address.city,
        complement: input.address.complement,
        country: input.address.country,
        number: input.address.addressNumber,
        state: input.address.state,
        street: input.address.street,
        zipCode: input.address.zipCode,
      },
    };

    if (this.paymentService.getIsCardPayment(input.paymentType)) {
      if (!input.paymentCard) {
        throw new Error('Payment card is required');
      }

      requestBody.creditCardInformation = {
        cardholderName: input.paymentCard.cardHolderName,
        cardNumber: input.paymentCard.cardNumber,
        cvv: input.paymentCard.cvCode,
        expiryMonth: input.paymentCard.expirationMonth,
        expiryYear: 2000 + input.paymentCard.expirationYear,
        installmentCount: input.installmentsNumber,
      };
    }

    const completeApiUrl =
      this.webServiceConfigService.getCompleteApiUrl('Order/create');

    return this.httpClient.post<void>(completeApiUrl, requestBody);
  }

  getOrders(): Observable<OrderModel[]> {
    const authCredentials = this.authService.getAuthCredentials();

    if (!authCredentials) {
      throw new Error('User not found');
    }

    const completeApiUrl = this.webServiceConfigService.getCompleteApiUrl(
      `Order/from-user/${authCredentials.user.id}`
    );

    return this.httpClient
      .get<ApiCompactOrderOutputDTO[]>(completeApiUrl)
      .pipe(
        map((orders) =>
          orders.map((order) => this.mapApiCompactOrderToModel(order))
        )
      );
  }

  mapApiCompactOrderToModel(apiOrderDTO: ApiCompactOrderOutputDTO): OrderModel {
    const order: OrderModel = {
      id: apiOrderDTO.id,
      orderNumber: apiOrderDTO.orderNumber,
      date: new Date(apiOrderDTO.createdAt),
      items: apiOrderDTO.orderItems.map((item) => ({
        id: item.id,
        quantity: item.quantity,
        unitPrice: item.price,
        description: item.productInformation.model,
      })),
      total: apiOrderDTO.totalAmount,
    };

    console.log(order);

    return order;
  }
}
