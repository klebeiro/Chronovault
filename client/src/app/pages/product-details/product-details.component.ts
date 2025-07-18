import { Component, OnInit } from '@angular/core';
import { AppPageComponent } from '../../components';
import { CommonModule, CurrencyPipe, NgOptimizedImage } from '@angular/common';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { WatchDetailsModel, WatchImageModel } from '../../models';
import { WatchService } from '../../services';
import { SharedModule } from '../../shared';
import { WatchSpec } from './product-details.types';

@Component({
  selector: 'app-product-details',
  imports: [
    AppPageComponent,
    CurrencyPipe,
    CommonModule,
    MatChipsModule,
    NgOptimizedImage,
    MatIconModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    SharedModule
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

  constructor(private watchService: WatchService) {}

  ngOnInit(): void {
    this.loadWatchDetails();
  }

  loadWatchDetails(){
    this.isLoading = true;

    this.watchService.getWatchById(1).subscribe({
      next: (data) => {
        this.watchDetails = data;
        this.selectedImage = this.watchDetails.images[0];
        this.generateWatchSpecs(data);
      },
      error: (error) => {
      },
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
        description: this.watchService.getCategoryDescription(watchDetails.category),
      },
      {
        title: 'Gênero',
        description: this.watchService.getGenderDescription(watchDetails.gender),
      },
      {
        title: 'Resistencia à água',
        description: this.watchService.getWaterResistanceDescription(watchDetails.waterResistance),
      },
    ]

    this.technicalSpecs = [
      {
        title: 'Movimento',
        description: this.watchService.getMovementDescription(watchDetails.movementType),
      },
      {
        title: 'Material da Caixa',
        description: this.watchService.getCaseMaterialDescription(watchDetails.caseMaterial),
      },
      {
        title: 'Material da Pulseira',
        description: this.watchService.getStrapMaterialDescription(watchDetails.strapMaterial)  ,
      },
    ]
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
}
