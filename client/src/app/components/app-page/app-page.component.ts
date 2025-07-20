import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDrawer, MatSidenavModule } from '@angular/material/sidenav';

import { NavbarComponent } from '../navbar/navbar.component';
import { FooterComponent } from '../footer/footer.component';
import { CartService } from '../../shared/services';
import { Cart } from '../../shared/types';
import { CartComponent } from '../cart/cart.component';

@Component({
  selector: 'app-page',
  imports: [NavbarComponent, FooterComponent, MatSidenavModule, CartComponent],
  templateUrl: './app-page.component.html',
  styleUrl: './app-page.component.scss',
})
export class AppPageComponent implements OnInit {
  cart: Cart = { items: [] };
  @ViewChild("drawer") drawer: MatDrawer | undefined;
  @Input("fixedElementsMarginActive") fixedElementsMarginActive: boolean = true;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.subscribeOpenCartDrawer();
  }

  subscribeOpenCartDrawer() {
    this.cartService.getIsCartDrawerOpened().subscribe((isOpen) => {
      if (isOpen) {
        this.drawer?.open();
      } else{
        this.drawer?.close();
      }

    })
  }
}
