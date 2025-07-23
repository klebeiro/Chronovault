import { CurrencyPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { PurchaseItemCardComponent } from '../../components/purchase-item-card';
import {
  OrderDetailsModel,
  PaymentMethodModel,
  PurchaseItemModel,
} from '../../models';
import { BaseComponentActionDirective } from '../../shared/directives';
import { OrderService, PaymentService } from '../../shared/services';
import { ActivatedRoute } from '@angular/router';
import { PaymentCardModel } from '../../models/payment-card.model';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { PaymentTypeEnum } from '../../shared/enums';
import { AuthService } from '../../services';

@Component({
  selector: 'app-order-details',
  imports: [
    MatIconModule,
    CurrencyPipe,
    PurchaseItemCardComponent,
    MatButtonModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.scss',
})
export class OrderDetailsComponent
  extends BaseComponentActionDirective
  implements OnInit
{
  purchaseItems: PurchaseItemModel[] = [
    {
      quantity: 1,
      item: {
        id: 1,
        model: 'Rolex',
        brand: 'Rolex',
        price: 1000,
        imagesUrls: [''],
      },
    },
    {
      quantity: 1,
      item: {
        id: 2,
        model: 'New Rolex',
        brand: 'New Rolex',
        price: 1000,
        imagesUrls: [''],
      },
    },
  ];

  orderDetails: OrderDetailsModel | null = null;
  paymentMethodDetails: PaymentMethodModel | null = null;
  paymentCard: PaymentCardModel | null = null;


  constructor(
    private orderService: OrderService,
    private route: ActivatedRoute,
    private paymentService: PaymentService,
    private authService: AuthService
  ) {
    super();
  }

  ngOnInit(): void {
    this.subscribeToRouteChange();
  }

  subscribeToRouteChange() {
    this.isLoading = true;

    this.route.paramMap.subscribe((params) => {
      const orderId = params.get('id') || 0;
      this.getOrderDetails(Number(orderId));
    });
  }

  getOrderDetails(orderId: number) {
    this.executeActionWithoutInputWithOutput<OrderDetailsModel>({
      actionMethod: () => {
        return this.orderService.getOrderDetails(orderId);
      },
      displayErrorNotification: true,
      errorMessage: 'Erro ao buscar detalhes do pedido',
      onSucess: (orderDetails) => {
        this.orderDetails = orderDetails;
        this.setPaymentMethodDetails(orderDetails);
        console.log(orderDetails);
      },
    });
  }

  setPaymentMethodDetails(orderDetails: OrderDetailsModel) {
    this.paymentMethodDetails = this.paymentService.getPaymentMethod(
      orderDetails.paymentMethod
    );

    if (
      this.paymentService.getIsCardPayment(orderDetails.paymentMethod) &&
      orderDetails.paymentCard
    ) {
      this.paymentCard = {
        cardHolderName: orderDetails.paymentCard.cardHolderName,
        cardNumber: orderDetails.paymentCard.cardHolderName,
      };
    }
  }

  getIsCardPayment(): boolean {
    if(this.paymentMethodDetails == null) {
      return false;
    }

    return this.paymentService.getIsCardPayment(this.paymentMethodDetails.type);
  }

  getTotalPrice(): number {
    if(this.orderDetails == null) {
      return 0;
    }

    return this.orderDetails.items.reduce((total, item) => {
      return total + item.item.price * item.quantity;
    }, 0) || 0;
  }

  getAddressDescription() {
    const authCredentials = this.authService.getAuthCredentials();

    if(authCredentials == null) {
      return '';
    }

    return `${authCredentials.user.address.street}, ${authCredentials.user.address.addressNumber}, ${authCredentials.user.address.city}`
  }
}
