import { Component, Input } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { SharedModule } from '../../shared';
import { CompactWatchModel } from '../../models';

@Component({
  selector: 'app-product-card',
  imports: [NgOptimizedImage, CurrencyPipe, MatIconModule, SharedModule],
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

  constructor(private router: Router) {}

  nextImage() {
    this.productImgIndex = (this.productImgIndex + 1) % this.product.imagesUrls.length;
  }

  previousImage() {
    this.productImgIndex = (this.productImgIndex + 1) % this.product.imagesUrls.length;
  }

  onProductClick() {
    this.router.navigate(['product', this.product.id]);
  }
}
