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
import { CartService, OrderService } from '../../shared/services';
import { Cart } from '../../shared/types';
import { PaymentCardModel } from '../../models/payment-card.model';
import { BaseComponentActionDirective } from '../../shared/directives';
import { CreateOrderInputDTO } from '../../dto/order';
import { AuthService } from '../../services';
import { UserModel } from '../../models';

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
export class CheckoutComponent
  extends BaseComponentActionDirective
  implements OnInit
{
  paymentCardForm!: PaymentCardFormGroup;
  selectedPaymentMethod: PaymentTypeEnum = PaymentTypeEnum.DebitCard;
  paymentCard: PaymentCardModel | null = null;
  @ViewChild('stepper') stepper: MatStepper | null = null;

  constructor(
    private fb: FormBuilder,
    private cartService: CartService,
    private router: Router,
    private orderService: OrderService,
    private authService: AuthService
  ) {
    super();
  }

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

    this.paymentCard = {
      cardHolderName: this.paymentCardForm.value.cardHolderName || '',
      cardNumber: this.paymentCardForm.value.cardNumber || '',
    };

    this.stepper.next();
  }

  goToNextStep() {
    if (!this.stepper) {
      return;
    }

    this.stepper.next();
  }

  finishOrder() {
    const authCredentials = this.authService.getAuthCredentials();

    if (!authCredentials) {
      this.showErrorNotification({
        title: 'Erro ao criar pedido',
        description: 'Por favor, faca login para finalizar o pedido',
      });

      return;
    }

    const orderInfo = this.generateCreateOrderInput(authCredentials.user);

    this.executeActionWithInput<CreateOrderInputDTO.OrderInput>({
      actionMethod: this.orderService.createOrder.bind(this.orderService),
      displayErrorNotification: true,
      errorMessage: 'Erro ao criar pedido',
      input: orderInfo,
      onSucess: () => {
        this.showSuccessNotification({
          title: 'Pedido criado com sucesso',
          description: 'Obrigado pela compra',
        });

        this.cartService.clearCart();
        this.router.navigate(['/profile/my-orders']);
      },
    });
  }

  private generateCreateOrderInput(
    user: UserModel
  ): CreateOrderInputDTO.OrderInput {
    const { address } = user;

    const order: CreateOrderInputDTO.OrderInput = {
      address: {
        addressNumber: address.addressNumber,
        city: address.city,
        complement: '',
        country: 'BR',
        state: address.state,
        street: address.street,
        zipCode: address.zipCode,
      },
      installmentsNumber: this.paymentCardForm.value.installmentsNumber || 1,
      orderItems: [],
      paymentCard: null,
      paymentType: this.selectedPaymentMethod,
    };

    const cartItems = this.cartService.getCartItems();
    order.orderItems = cartItems.map((item) => {
      return {
        productId: item.item.id,
        quantity: item.quantity,
      };
    });

    if (this.getIsCardPayment()) {
      console.log('card payment', this.paymentCardForm.value);

      const expirationDate = this.paymentCardForm.value.expirationDate ?? '';
      const [month, year] = expirationDate.replace(' ', '').split('/');

      order.paymentCard = {
        cardHolderName: this.paymentCardForm.value.cardHolderName || '',
        expirationMonth: month ? parseInt(month) : 0,
        expirationYear: year ? parseInt(year) : 0,
        cardNumber: this.paymentCardForm.value.cardNumber || '',
        cvCode: this.paymentCardForm.value.cvCode ? parseInt(this.paymentCardForm.value.cvCode) : 0,
      };
    }

    return order;
  }
}
