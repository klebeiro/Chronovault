import { Injectable } from '@angular/core';
import { Cart } from '../types';
import { BehaviorSubject } from 'rxjs';
import { PurchaseItemModel } from '../../models';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cart: BehaviorSubject<Cart> = new BehaviorSubject<Cart>({
    items: [],
  });

  private isCartDrawerOpened: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  
  constructor() {}

  getCart(): BehaviorSubject<Cart> {
    return this.cart;
  }

  addItemToCart(item: PurchaseItemModel) {
    this.cart.getValue().items.push(item);
    this.cart.next(this.cart.getValue());
  }

  removeItemFromCart(itemId: number) {
    this.cart.getValue().items = this.cart
      .getValue()
      .items.filter((item) => item.item.id !== itemId);

    this.cart.next(this.cart.getValue());
  }

  getIsItemInCart(itemId: number): boolean {
    return this.cart.getValue().items.some((item) => item.item.id === itemId);
  }

  openCartDrawer() {
    this.isCartDrawerOpened.next(true);
  }

  closeCartDrawer() {
    this.isCartDrawerOpened.next(false);
  }

  getIsCartDrawerOpened(): BehaviorSubject<boolean> {
    return this.isCartDrawerOpened;
  }

  cartIsEmpty(): boolean {
    return this.cart.getValue().items.length === 0;
  }

  
}
