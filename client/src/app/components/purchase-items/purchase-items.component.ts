import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

import { PurchaseItemCardComponent } from '../purchase-item-card';
import { Cart } from '../../shared/types';
import { CartService } from '../../shared/services';

@Component({
  selector: 'purchase-items',
  imports: [CurrencyPipe, MatButtonModule, PurchaseItemCardComponent],
  templateUrl: './purchase-items.component.html',
  styleUrl: './purchase-items.component.scss',
})
export class PurchaseItemsComponent implements OnInit {
  cart: Cart = { items: [] };
  @Output('onConfirm') onConfirm = new EventEmitter<void>();

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.subscribeCart();
  }

  subscribeCart() {
    this.cartService.getCart().subscribe((cart) => {
      this.cart = cart;
    });
  }

  confirm() {
    this.onConfirm.emit();
  }

  getTotalPrice() {
    return this.cart.items.reduce(
      (acc, item) => acc + item.item.price * item.quantity,
      0
    );
  }
}
