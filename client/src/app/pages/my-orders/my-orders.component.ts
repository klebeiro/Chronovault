import { Component, OnInit } from '@angular/core';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

import { OrderModel } from '../../models';
import { BaseComponentActionDirective } from '../../shared/directives';
import { OrderService } from '../../shared/services';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-my-orders',
  imports: [DatePipe, CurrencyPipe, MatButtonModule, MatProgressSpinnerModule],
  templateUrl: './my-orders.component.html',
  styleUrl: './my-orders.component.scss',
})
export class MyOrdersComponent extends BaseComponentActionDirective implements OnInit {
  orders: OrderModel[] = [];

  constructor(private router: Router, private orderService: OrderService) {
    super();
  }

  ngOnInit(): void {
    this.getOrders();
  }

  goToOrderDetails(orderId: number) {
    this.router.navigate(['profile', 'order-details', orderId]);
  }

  getOrders() {
    this.executeActionWithoutInputWithOutput<OrderModel[]>({
      actionMethod: this.orderService.getOrders.bind(this.orderService),
      displayErrorNotification: true,
      errorMessage: 'Erro ao buscar pedidos',
      onSucess: (orders) => {
        this.orders = orders;
      },
    });
  }
}
