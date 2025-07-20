import { Injectable } from '@angular/core';
import {
  WatchCategoryEnum,
  CaseMaterialEnum,
  CaseSizeEnum,
  MovementTypeEnum,
  ProductGenderEnum,
  StrapMaterialEnum,
  WaterResistanceEnum,
} from '../shared/enums';
import { CompactWatchModel, WatchDetailsModel } from '../models';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { WebServiceConfigService } from '../shared/services';
import { MostViewedWatchOutputDTO } from '../dto/watches';
import { MOCK_WATCHES } from '../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class WatchService {
  constructor(
    private webServiceConfigService: WebServiceConfigService,
    private httpClient: HttpClient
  ) {}

  getCategoryDescription(value: WatchCategoryEnum): string {
    switch (value) {
      case WatchCategoryEnum.Classic:
        return 'Clássico';
      case WatchCategoryEnum.Sport:
        return 'Esportivo';
      case WatchCategoryEnum.Casual:
        return 'Casual';
      case WatchCategoryEnum.Formal:
        return 'Formal';
      case WatchCategoryEnum.Luxury:
        return 'Luxo';
      case WatchCategoryEnum.Smartwatch:
        return 'Smartwatch';
      case WatchCategoryEnum.Vintage:
        return 'Vintage';
      case WatchCategoryEnum.Diving:
        return 'Mergulho';
      default:
        return 'Desconhecido';
    }
  }

  getCaseMaterialDescription(value: CaseMaterialEnum): string {
    switch (value) {
      case CaseMaterialEnum.StainlessSteel:
        return 'Aço Inoxidável';
      case CaseMaterialEnum.Gold:
        return 'Ouro';
      case CaseMaterialEnum.RoseGold:
        return 'Ouro Rosa';
      case CaseMaterialEnum.Silver:
        return 'Prata';
      case CaseMaterialEnum.Titanium:
        return 'Titânio';
      case CaseMaterialEnum.Aluminum:
        return 'Alumínio';
      case CaseMaterialEnum.Ceramic:
        return 'Cerâmica';
      case CaseMaterialEnum.Plastic:
        return 'Plástico';
      case CaseMaterialEnum.Bronze:
        return 'Bronze';
      case CaseMaterialEnum.Carbon:
        return 'Carbono';
      default:
        return 'Desconhecido';
    }
  }

  getCaseSizeDescription(value: CaseSizeEnum): string {
    switch (value) {
      case CaseSizeEnum.Small:
        return 'Pequeno (até 36mm)';
      case CaseSizeEnum.Medium:
        return 'Médio (37-40mm)';
      case CaseSizeEnum.Large:
        return 'Grande (41-44mm)';
      case CaseSizeEnum.ExtraLarge:
        return 'Extra Grande (45mm+)';
      default:
        return 'Desconhecido';
    }
  }

  getMovementDescription(value: MovementTypeEnum): string {
    switch (value) {
      case MovementTypeEnum.Quartz:
        return 'Quartzo';
      case MovementTypeEnum.Automatic:
        return 'Automático';
      case MovementTypeEnum.Manual:
        return 'Manual';
      case MovementTypeEnum.Solar:
        return 'Solar';
      case MovementTypeEnum.Kinetic:
        return 'Cinético';
      case MovementTypeEnum.Digital:
        return 'Digital';
      case MovementTypeEnum.Hybrid:
        return 'Híbrido';
      default:
        return 'Desconhecido';
    }
  }

  getGenderDescription(value: ProductGenderEnum): string {
    switch (value) {
      case ProductGenderEnum.Male:
        return 'Masculino';
      case ProductGenderEnum.Female:
        return 'Feminino';
      case ProductGenderEnum.Unisex:
        return 'Unissex';
      default:
        return 'Desconhecido';
    }
  }

  getStrapMaterialDescription(value: StrapMaterialEnum): string {
    switch (value) {
      case StrapMaterialEnum.Leather:
        return 'Couro';
      case StrapMaterialEnum.StainlessSteel:
        return 'Aço Inoxidável';
      case StrapMaterialEnum.Silicone:
        return 'Silicone';
      case StrapMaterialEnum.Nylon:
        return 'Nylon';
      case StrapMaterialEnum.Rubber:
        return 'Borracha';
      case StrapMaterialEnum.Fabric:
        return 'Tecido';
      case StrapMaterialEnum.Titanium:
        return 'Titânio';
      case StrapMaterialEnum.Ceramic:
        return 'Cerâmica';
      case StrapMaterialEnum.Gold:
        return 'Ouro';
      case StrapMaterialEnum.Silver:
        return 'Prata';
      default:
        return 'Desconhecido';
    }
  }

  getWaterResistanceDescription(value: WaterResistanceEnum): string {
    switch (value) {
      case WaterResistanceEnum.None:
        return 'Não possui resistência à água';
      case WaterResistanceEnum.ThirtyMeters:
        return '30m - Respingos';
      case WaterResistanceEnum.FiftyMeters:
        return '50m - Natação';
      case WaterResistanceEnum.OneHundredMeters:
        return '100m - Snorkeling';
      case WaterResistanceEnum.TwoHundredMeters:
        return '200m - Mergulho Recreativo';
      case WaterResistanceEnum.ThreeHundredMeters:
        return '300m - Mergulho Profissional';
      case WaterResistanceEnum.FiveHundredMetersPlus:
        return '500m+ - Mergulho Extremo';
      default:
        return 'Desconhecido';
    }
  }

  getWatchById(id: number): Observable<WatchDetailsModel> {
    const fakeImages = [
      'https://xelorwatches.com/wp-content/uploads/2023/09/PATEK-PHILIPPE-NAUTILUS-%E2%80%93-57111A-001-%E2%80%93-STEEL-40mm.png',
      'https://th.bing.com/th/id/OIP.s-fAAGrOYb0g7m2pjyygawHaHa?cb=iwc2&rs=1&pid=ImgDetMain',
    ];

    return new Observable<WatchDetailsModel>((observer) => {
      setTimeout(() => {
        const watchDetails = MOCK_WATCHES.find(
          (watch) => watch.id === id
        )

        if(!watchDetails) {
          observer.error(new Error('Watch not found'));
          return;
        }

        observer.next(watchDetails);
        observer.complete();
      }, 1000);
    });
  }

  getMostViewedWaches(): Observable<CompactWatchModel[]> {
    const completeApiUrl = this.webServiceConfigService.getCompleteApiUrl(
      '/products/most-viewed'
    );

    const fakeImages = [
      'https://xelorwatches.com/wp-content/uploads/2023/09/PATEK-PHILIPPE-NAUTILUS-%E2%80%93-57111A-001-%E2%80%93-STEEL-40mm.png',
      'https://th.bing.com/th/id/OIP.s-fAAGrOYb0g7m2pjyygawHaHa?cb=iwc2&rs=1&pid=ImgDetMain',
    ];

    const mostViewedProductOutputDtos: MostViewedWatchOutputDTO[] = MOCK_WATCHES.map(
      (product) => ({
        id: product.id,
        model: product.model,
        brand: product.brand,
        price: product.price,
        imagesUrls: product.images.map((image) => image.url),
      })
    )

    const mostViewProducts: CompactWatchModel[] =
      mostViewedProductOutputDtos.map((product) => ({
        id: product.id,
        model: product.model,
        brand: product.brand,
        price: product.price,
        imagesUrls: product.imagesUrls,
      }));

    // return this.httpClient.get<Product[]>(completeApiUrl).pipe(
    //   map((products) => {
    //     return products.map((product) =>
    //       this.mapMostViewedProductOutputDtoToProduct(product)
    //     );
    //   })
    // );

    // simulate api call
    return new Observable((observer) => {
      setTimeout(() => {
        observer.next(mostViewProducts);
        observer.complete();
      }, 3000);
    });
  }

  private mapMostViewedWatchOutputDtoToCompactWatchModel(
    mostViewedWatchOutputDto: MostViewedWatchOutputDTO
  ): CompactWatchModel {
    return {
      id: mostViewedWatchOutputDto.id,
      model: mostViewedWatchOutputDto.model,
      brand: mostViewedWatchOutputDto.brand,
      price: mostViewedWatchOutputDto.price,
      imagesUrls: mostViewedWatchOutputDto.imagesUrls,
    };
  }
}
