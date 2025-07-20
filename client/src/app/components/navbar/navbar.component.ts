import { Component, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { NgIf } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Cart } from '../../shared/types';

import { CartService } from '../../shared/services';
import { PurchaseItemCardComponent } from '../purchase-item-card';

@Component({
  selector: 'app-navbar',
  imports: [
    MatIconModule,
    NgIf,
    RouterModule,
    PurchaseItemCardComponent,
    MatSidenavModule,
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent implements OnInit {
  IsCartActive: boolean = false;
  cart: Cart = {
    items: [],
  };

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.addCartSubscription();
  }

  addCartSubscription() {
    this.cartService.getCart().subscribe((cart) => {
      this.cart = cart;
    });
  }

  openCartDrawer() {
    this.cartService.openCartDrawer();
  }
}
