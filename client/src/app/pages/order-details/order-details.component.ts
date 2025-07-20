import { CurrencyPipe } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { PurchaseItemCardComponent } from '../../components/purchase-item-card';
import { PurchaseItemModel } from '../../models';

@Component({
  selector: 'app-order-details',
  imports: [   MatIconModule,
    CurrencyPipe,
    PurchaseItemCardComponent,
    MatButtonModule,],
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.scss'
})
export class OrderDetailsComponent {
  purchaseItems: PurchaseItemModel[] = [
    {
      quantity: 1,
      item: {
        id: 1,
        model: 'Rolex',
        brand: 'Rolex',
        price: 1000,
        imagesUrls: [""],
      },
    },
    {
      quantity: 1,
      item: {
        id: 2,
        model: 'New Rolex',
        brand: 'New Rolex',
        price: 1000,
        imagesUrls: [""],
      },
    },
  ];

}
