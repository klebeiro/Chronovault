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
    // simulates an http request
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
          brand: 'Rolex',
          id: item.productId,
          imagesUrls: [''],
          model: 'Rolex',
          price: item.price,
        },
        quantity: item.quantity,
      })),
      paymentCard: null,
      paymentMethod: apiOrderDTO.paymentMethod,
      total: apiOrderDTO.totalAmount,
    };

    if (apiOrderDTO.paymentMethod === PaymentTypeEnum.CreditCard) {
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
        cardNumber: '1234 5678 9012 3456',
        cvv: 123,
        expiryMonth: 12,
        expiryYear: 2023,
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
      date: new Date(apiOrderDTO.createdDate),
      items: apiOrderDTO.orderItems.map((item) => ({
        id: item.productId,
        quantity: item.quantity,
        unitPrice: 2,
        description: 'Watch',
      })),
      total: apiOrderDTO.totalAmount,
    };

    return order;
  }
}
