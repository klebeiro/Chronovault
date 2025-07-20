import { Component } from '@angular/core';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { OrderModel } from '../../models';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-orders',
  imports: [DatePipe, CurrencyPipe, MatButtonModule],
  templateUrl: './my-orders.component.html',
  styleUrl: './my-orders.component.scss',
})
export class MyOrdersComponent {
  orders: OrderModel[] = [
    {
      id: 1,
      date: new Date(),
      total: 100,
      items: [
        {
          id: 1,
          quantity: 1,
          unitPrice: 100,
          description: 'Watch 1',
        },
        {
          id: 2,
          quantity: 1,
          unitPrice: 100,
          description: 'Watch 2',
        },
      ],
    },
    {
      id: 2,
      date: new Date(),
      total: 100,
      items: [
        {
          id: 1,
          quantity: 1,
          unitPrice: 100,
          description: 'Watch 1',
        },
        {
          id: 2,
          quantity: 1,
          unitPrice: 100,
          description: 'Watch 2',
        },
      ],
    },
  ];

  constructor(private router: Router) {}

  goToOrderDetails(orderId: number) {
    this.router.navigate(['profile', 'order-details', orderId]);
  }
}
