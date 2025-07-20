import { Component, OnInit } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { AppPageComponent } from '../../components';
import { WatchDetailsModel, WatchImageModel } from '../../models';
import { WatchService } from '../../services';
import { SharedModule } from '../../shared';

import { WatchSpec } from './product-details.types';
import { CartService } from '../../shared/services';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-details',
  imports: [
    AppPageComponent,
    CurrencyPipe,
    CommonModule,
    MatChipsModule,
    MatIconModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    SharedModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss',
})
export class ProductDetailsComponent implements OnInit {
  selectedImage: WatchImageModel | null = null;
  watchDetails: WatchDetailsModel | null = null;
  isLoading: boolean = true;
  generalSpecs: WatchSpec[] = [];
  technicalSpecs: WatchSpec[] = [];
  isInCart: boolean = false;

  constructor(
    private watchService: WatchService,
    private cartService: CartService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.subscribeToRouteChange();
  }

  subscribeToRouteChange() {
    this.isLoading = true;

    this.route.paramMap.subscribe((params) => {
      const watchId = params.get('id') ? Number(params.get('id')) : null;

      if (watchId) {
        this.loadWatchDetails(watchId);
        this.subscribeCart(watchId);
      }
    });
  }

  checkWatchInCart(watchId: number) {
    this.isInCart = this.cartService.getIsItemInCart(watchId);
    console.log(this.isInCart);
  }

  loadWatchDetails(watchId: number) {
    this.isLoading = true;

    this.watchService.getWatchById(watchId).subscribe({
      next: (data) => {
        this.watchDetails = data;
        this.selectedImage = this.watchDetails.images[0];
        this.generateWatchSpecs(data);
      },
      error: (error) => {},
      complete: () => {
        this.isLoading = false;
      },
    });
  }

  private generateWatchSpecs(watchDetails: WatchDetailsModel) {
    this.generalSpecs = [];
    this.technicalSpecs = [];

    this.generalSpecs = [
      {
        title: 'Categoria',
        description: this.watchService.getCategoryDescription(
          watchDetails.category
        ),
      },
      {
        title: 'Gênero',
        description: this.watchService.getGenderDescription(
          watchDetails.gender
        ),
      },
      {
        title: 'Resistencia à água',
        description: this.watchService.getWaterResistanceDescription(
          watchDetails.waterResistance
        ),
      },
    ];

    this.technicalSpecs = [
      {
        title: 'Movimento',
        description: this.watchService.getMovementDescription(
          watchDetails.movementType
        ),
      },
      {
        title: 'Material da Caixa',
        description: this.watchService.getCaseMaterialDescription(
          watchDetails.caseMaterial
        ),
      },
      {
        title: 'Material da Pulseira',
        description: this.watchService.getStrapMaterialDescription(
          watchDetails.strapMaterial
        ),
      },
    ];
  }

  subscribeCart(watchId: number) {
    this.cartService.getCart().subscribe((cart) => {
      console.log(cart);
      console.log("wc", watchId);
      this.isInCart = cart.items.some((item) => item.item.id === watchId);
    });
  }

  nextImage() {
    if (this.selectedImage && this.watchDetails) {
      const selectedImageIndex = this.watchDetails.images.indexOf(
        this.selectedImage
      );

      if (selectedImageIndex === this.watchDetails.images.length - 1) {
        this.selectedImage = this.watchDetails.images[0];
      } else {
        this.selectedImage = this.watchDetails.images[selectedImageIndex + 1];
      }
    }
  }

  previousImage() {
    if (this.selectedImage && this.watchDetails) {
      const selectedImageIndex = this.watchDetails.images.indexOf(
        this.selectedImage
      );

      if (selectedImageIndex === 0) {
        this.selectedImage =
          this.watchDetails.images[this.watchDetails.images.length - 1];
      } else {
        this.selectedImage = this.watchDetails.images[selectedImageIndex - 1];
      }
    }
  }

  selectImage(image: WatchImageModel) {
    this.selectedImage = image;
  }

  goToCheckout() {
    if (!this.watchDetails) {
      return;
    }

    console.log(this.isInCart);

    if (!this.isInCart) {
      this.addCurrentWatchInCart(this.watchDetails);
    }

    this.router.navigate(['checkout']);
  }

  addRemoveItemCart() {
    if (!this.watchDetails) {
      return;
    }

    if (this.isInCart) {
      this.cartService.removeItemFromCart(this.watchDetails.id);
    } else {
      this.addCurrentWatchInCart(this.watchDetails);
    }

    this.checkWatchInCart(this.watchDetails.id);
  }

  addCurrentWatchInCart(watchDetails: WatchDetailsModel) {
    console.log("wacth;::: ", watchDetails);

    this.cartService.addItemToCart({
      item: {
        brand: watchDetails.brand,
        id: watchDetails.id,
        imagesUrls: watchDetails.images.map((image) => image.url),
        model: watchDetails.model,
        price: watchDetails.price,
      },
      quantity: 1,
    });
  }
}
