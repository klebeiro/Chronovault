import { Component, OnInit, ViewChild } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import {
  MatStep,
  MatStepper,
  MatStepperModule,
} from '@angular/material/stepper';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { SharedModule } from '../../shared';
import {
  AppPageComponent,
  OrderConfirmationComponent,
  PurchaseItemsComponent,
  SelectPaymentMethodComponent,
} from '../../components';

import {
  PaymentCardForm,
  PaymentCardFormGroup,
} from '../../components/payment-card-form';
import { PaymentTypeEnum } from '../../shared/enums';
import { CartService } from '../../shared/services';
import { Cart } from '../../shared/types';

@Component({
  selector: 'app-checkout',
  imports: [
    SharedModule,
    AppPageComponent,
    MatIconModule,
    CommonModule,
    MatStep,
    MatStepperModule,
    SelectPaymentMethodComponent,
    PurchaseItemsComponent,
    OrderConfirmationComponent,
  ],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss',
})
export class CheckoutComponent implements OnInit {
  paymentCardForm!: PaymentCardFormGroup;
  selectedPaymentMethod: PaymentTypeEnum = PaymentTypeEnum.DebitCard;
  @ViewChild('stepper') stepper: MatStepper | null = null;

  constructor(
    private fb: FormBuilder,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.setPaymentCardForm();
    this.subscribeToCart();
  }

  subscribeToCart() {
    this.cartService.getCart().subscribe((cart) => {
      this.checkEmptyCart(cart);
    });
  }

  checkEmptyCart(cart: Cart) {
    if (cart.items.length === 0) {
      this.router.navigate(['']);
    }
  }

  setPaymentCardForm() {
    this.paymentCardForm = this.fb.group<PaymentCardForm>({
      cardNumber: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      expirationDate: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      cvCode: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      cardHolderName: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      installmentsNumber: this.fb.control(null, {
        nonNullable: true,
        validators: [Validators.required],
      }),
    });
  }

  changeSelectedPaymentType(paymentType: PaymentTypeEnum) {
    this.selectedPaymentMethod = paymentType;
  }

  getIsCardPayment() {
    return (
      this.selectedPaymentMethod === PaymentTypeEnum.DebitCard ||
      this.selectedPaymentMethod === PaymentTypeEnum.CreditCard
    );
  }

  submitValidPaymentForm() {
    if (!this.stepper) {
      return;
    }

    this.stepper.next();
  }

  goToNextStep() {
    if (!this.stepper) {
      return;
    }

    this.stepper.next();
  }


  finishOrder(){
    console.log("finish order");
  }
}
