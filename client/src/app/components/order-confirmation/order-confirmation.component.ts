import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
} from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { CurrencyPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { NgxMaskPipe } from 'ngx-mask';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { AuthCredentialsModel, PaymentMethodModel } from '../../models';
import { PurchaseItemCardComponent } from '../purchase-item-card';
import { PaymentTypeEnum } from '../../shared/enums';
import { PaymentCardModel } from '../../models/payment-card.model';
import { Cart } from '../../shared/types';
import { CartService, PaymentService } from '../../shared/services';
import { AuthService } from '../../services';


@Component({
  selector: 'order-confirmation',
  imports: [
    MatIconModule,
    CurrencyPipe,
    PurchaseItemCardComponent,
    MatButtonModule,
    NgxMaskPipe,
    MatProgressSpinnerModule,
  ],
  templateUrl: './order-confirmation.component.html',
  styleUrl: './order-confirmation.component.scss',
})
export class OrderConfirmationComponent implements OnInit, OnChanges {
  @Output('onConfirm') onConfirm = new EventEmitter<void>();
  @Input('paymentType') paymentType: PaymentTypeEnum =
    PaymentTypeEnum.CreditCard;
  @Input('paymentCard') paymentCard: PaymentCardModel | null = null;
  @Input('installmentsNumber') installmentsNumber: number = 1;
  @Input('isLoading') isLoading: boolean = false;

  authCredentials: AuthCredentialsModel | null = null;

  cart: Cart = { items: [] };
  paymentMethod: PaymentMethodModel | null = null;

  constructor(
    private cartService: CartService,
    private authService: AuthService,
    private paymentService: PaymentService
  ) {}

  ngOnInit(): void {
    this.subscribeCart();
    this.subscribeToAuthCredentialsChange();
  }

  ngOnChanges(): void {
    this.updatePaymentTypeInfo();
  }

  subscribeCart() {
    this.cartService.getCart().subscribe((cart) => {
      this.cart = cart;
    });
  }

  subscribeToAuthCredentialsChange() {
    this.authService.getAuthCredentials$().subscribe((credentials) => {
      this.authCredentials = credentials;
    });
  }

  confirmOrder() {
    this.onConfirm.emit();
  }

  updatePaymentTypeInfo() {
    this.paymentMethod = this.paymentService.getPaymentMethod(this.paymentType);
  }

  getIsCardPayment() {
    return (
      this.paymentType === PaymentTypeEnum.CreditCard ||
      this.paymentType === PaymentTypeEnum.DebitCard
    );
  }

  getTotalPrice() {
    return this.cartService.getTotalPrice();
  }

  getAuthCredentials(): AuthCredentialsModel | null {
    return this.authService.getAuthCredentials();
  }
}
