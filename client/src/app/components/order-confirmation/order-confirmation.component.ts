import { Component, EventEmitter, Output } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { CurrencyPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

import { PurchaseItemModel } from '../../models';
import { PurchaseItemCardComponent } from '../purchase-item-card';

const fakeImages = [
  'https://xelorwatches.com/wp-content/uploads/2023/09/PATEK-PHILIPPE-NAUTILUS-%E2%80%93-57111A-001-%E2%80%93-STEEL-40mm.png',
  'https://th.bing.com/th/id/OIP.s-fAAGrOYb0g7m2pjyygawHaHa?cb=iwc2&rs=1&pid=ImgDetMain',
];

@Component({
  selector: 'order-confirmation',
  imports: [
    MatIconModule,
    CurrencyPipe,
    PurchaseItemCardComponent,
    MatButtonModule,
  ],
  templateUrl: './order-confirmation.component.html',
  styleUrl: './order-confirmation.component.scss',
})
export class OrderConfirmationComponent {
  @Output("onConfirm") onConfirm = new EventEmitter<void>()

  purchaseItems: PurchaseItemModel[] = [
    {
      quantity: 1,
      item: {
        id: 1,
        model: 'Rolex',
        brand: 'Rolex',
        price: 1000,
        imagesUrls: fakeImages,
      },
    },
    {
      quantity: 1,
      item: {
        id: 2,
        model: 'New Rolex',
        brand: 'New Rolex',
        price: 1000,
        imagesUrls: [fakeImages[1]],
      },
    },
  ];

  confirmOrder(){
    this.onConfirm.emit();
  }
}
