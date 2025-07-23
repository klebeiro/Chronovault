import { Component, OnInit } from '@angular/core';
import { PurchaseItemCardComponent } from '../purchase-item-card';
import { Cart } from '../../shared/types';
import { CartService } from '../../shared/services';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { AuthService } from '../../services';

@Component({
  selector: 'app-cart',
  imports: [PurchaseItemCardComponent, MatIconModule, MatButtonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit{
  cart: Cart = { items: [] }

  constructor(private cartService: CartService, private router: Router, private authService: AuthService) {

  }

  ngOnInit(): void {
    this.subscribeCart();
  }

  subscribeCart() {
    this.cartService.getCart().subscribe((cart) => {
      this.cart = cart
    });
  }

  closeCartDrawer() {
    this.cartService.closeCartDrawer();
  }

  goToCheckout() {
    if(!this.authService.getIsAuthenticated()) {
      this.router.navigate(['login']);
      return;
    }

    this.router.navigate(['checkout']);
  }
}
