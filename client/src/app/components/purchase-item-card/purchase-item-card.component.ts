import { Component, Input } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

import { PurchaseItemModel } from '../../models';
import { CartService } from '../../shared/services';

@Component({
  selector: 'purchase-item-card',
  imports: [CurrencyPipe, MatIconModule, MatButtonModule],
  templateUrl: './purchase-item-card.component.html',
  styleUrl: './purchase-item-card.component.scss'
})
export class PurchaseItemCardComponent {
  @Input("purchaseItem") purchaseItem: PurchaseItemModel | null = null;
  @Input("showRemoveButton") showRemoveButton: boolean = true;

  constructor(private cartService: CartService){

  }

  removeItemFromCart() {
    if(!this.purchaseItem) {
      return;
    }

    this.cartService.removeItemFromCart(this.purchaseItem.item.id);
  }

  getTotalPrice() {
    if(!this.purchaseItem) {
      return 0;
    }

    return this.purchaseItem.item.price * this.purchaseItem.quantity;
  }
}
