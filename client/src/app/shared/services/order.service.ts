import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CompactWatchModel, OrderDetailsModel, OrderModel } from '../../models';
import { MOCK_ORDERS, MOCK_WATCHES } from '../constants';
import { PaymentTypeEnum } from '../enums';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor() {}

  gerOrders(): Observable<OrderModel[]> {
    // simulates an http request
    return new Observable<OrderModel[]>((observer) => {
      observer.next(MOCK_ORDERS);
      observer.complete();
    });
  }

  getOrderDetails(orderId: number): Observable<OrderDetailsModel> {
    // simulates an http request
    return new Observable<OrderDetailsModel>((observer) => {
      const order = MOCK_ORDERS.find((order) => order.id === orderId);

      if (!order) {
        observer.error('Order not found');
        observer.complete();
        return;
      }

      const orderItemIds = order.items.map((item) => item.id);

      const orderItems = MOCK_WATCHES.filter((watch) =>
        orderItemIds.includes(watch.id)
      );
      const orderItemDetails: CompactWatchModel[] = orderItems.map((item) => ({
        id: item.id,
        model: item.model,
        brand: item.brand,
        price: item.price,
        imagesUrls: item.images.map((image) => image.url),
      }));

      const orderDetails: OrderDetailsModel = {
        id: order.id,
        date: order.date,
        items: orderItemDetails,
        total: order.total,
        paymentCard: {
          cardHolderName: 'John Doe',
          cardNumber: '1234 5678 9012 3456',
        },
        paymentMethod: PaymentTypeEnum.CreditCard,
      };

      observer.next(orderDetails);
      observer.complete();
    });
  }
}
