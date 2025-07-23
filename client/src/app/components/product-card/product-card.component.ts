import { Component, Input } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';

import { SharedModule } from '../../shared';
import { CompactWatchModel } from '../../models';
import { CartService } from '../../shared/services';

@Component({
  selector: 'app-product-card',
  imports: [CurrencyPipe, MatIconModule, SharedModule],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css'
})
export class ProductCardComponent {
  @Input("product") product: CompactWatchModel = {
    id: 0,
    model: "",
    brand: "",
    price: 0,
    imagesUrls: []
  };
  productImgIndex: number = 0;

  constructor(private router: Router, private cartService: CartService) {}

  nextImage() {
    this.productImgIndex = (this.productImgIndex + 1) % this.product.imagesUrls.length;
  }

  clickCartButton() {
    if(this.cartService.getIsItemInCart(this.product.id)){
      this.cartService.removeItemFromCart(this.product.id);
      return;
    };

    this.cartService.addItemToCart({
      item: {
        brand: this.product.brand,
        id: this.product.id,
        imagesUrls: this.product.imagesUrls,
        model: this.product.model,
        price: this.product.price
      },
      quantity: 1
    });
  }

  isInCart(){
    return this.cartService.getIsItemInCart(this.product.id);
  }

  previousImage() {
    this.productImgIndex = (this.productImgIndex + 1) % this.product.imagesUrls.length;
  }

  onProductClick() {
    this.router.navigate(['product', this.product.id]);
  }
}
