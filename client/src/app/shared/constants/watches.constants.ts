import { WatchDetailsModel } from '../../models';
import {
  WatchCategoryEnum,
  ProductGenderEnum,
  MovementTypeEnum,
  CaseMaterialEnum,
  StrapMaterialEnum,
  WaterResistanceEnum,
} from '../enums';

export const MOCK_WATCHES: WatchDetailsModel[] = [
  {
    id: 1,
    model: 'Explorer Pro 2025',
    brand: 'Rolex',
    description:
      'Relógio de luxo esportivo, ideal para aventuras extremas com resistência superior.',
    price: 58990,
    category: WatchCategoryEnum.Sport,
    gender: ProductGenderEnum.Male,
    movementType: MovementTypeEnum.Automatic,
    caseMaterial: CaseMaterialEnum.StainlessSteel,
    strapMaterial: StrapMaterialEnum.Rubber,
    waterResistance: WaterResistanceEnum.ThreeHundredMeters,
    warranty: '5 anos',
    stockQuantity: 3,
    isActive: true,
    isFeatured: true,
    isOnSale: false,
    images: [
      {
        id: 1,
        url: 'https://http2.mlstatic.com/D_NQ_NP_917733-MLU77174275232_072024-F.jpg',
        order: 1,
      },
      {
        id: 2,
        url: 'https://m.media-amazon.com/images/I/510rxLMyIRL._AC_SL1000_.jpg',
        order: 2,
      },
    ],
  },
  {
    id: 2,
    model: 'Vintage 1965',
    brand: 'Seiko',
    description:
      'Modelo clássico com visual retrô e mecanismo manual para colecionadores.',
    price: 2490,
    category: WatchCategoryEnum.Vintage,
    gender: ProductGenderEnum.Unisex,
    movementType: MovementTypeEnum.Manual,
    caseMaterial: CaseMaterialEnum.Silver,
    strapMaterial: StrapMaterialEnum.Leather,
    waterResistance: WaterResistanceEnum.ThirtyMeters,
    warranty: '2 anos',
    stockQuantity: 15,
    isActive: true,
    isFeatured: false,
    isOnSale: true,
    images: [
      {
        id: 3,
        url: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTIJUAg4mAk2MjzJr9tEQqb2ROf1wP43GeNOQ&s',
        order: 1,
      },
    ],
  },
  {
    id: 3,
    model: 'Chrono Tech S1',
    brand: 'Tag Heuer',
    description: 'Design moderno com movimento solar e pulseira de titânio.',
    price: 8790,
    category: WatchCategoryEnum.Casual,
    gender: ProductGenderEnum.Male,
    movementType: MovementTypeEnum.Solar,
    caseMaterial: CaseMaterialEnum.Titanium,
    strapMaterial: StrapMaterialEnum.Titanium,
    waterResistance: WaterResistanceEnum.OneHundredMeters,
    warranty: '3 anos',
    stockQuantity: 7,
    isActive: true,
    isFeatured: true,
    isOnSale: true,
    images: [
      {
        id: 4,
        url: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQuQjqlINAHQ6RSM-oluLQLhAirOIrGtED6Zg&s',
        order: 1,
      },
    ],
  },
  {
    id: 4,
    model: 'Smart Fit Pro',
    brand: 'Apple',
    description:
      'Smartwatch com recursos fitness, notificações e integração total com iOS.',
    price: 3999,
    category: WatchCategoryEnum.Smartwatch,
    gender: ProductGenderEnum.Female,
    movementType: MovementTypeEnum.Digital,
    caseMaterial: CaseMaterialEnum.Aluminum,
    strapMaterial: StrapMaterialEnum.Silicone,
    waterResistance: WaterResistanceEnum.FiftyMeters,
    warranty: '1 ano',
    stockQuantity: 25,
    isActive: true,
    isFeatured: false,
    isOnSale: false,
    images: [
      {
        id: 5,
        url: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRuaBeOd2w6ZvE4pFmQaZKYYgOCDrULmUWSkw&s',
        order: 1,
      },
    ],
  },
  {
    id: 5,
    model: 'Oceanic Dive Master',
    brand: 'Citizen',
    description:
      'Feito para mergulho extremo, com resistência à água de 500m+.',
    price: 12990,
    category: WatchCategoryEnum.Diving,
    gender: ProductGenderEnum.Male,
    movementType: MovementTypeEnum.Kinetic,
    caseMaterial: CaseMaterialEnum.Ceramic,
    strapMaterial: StrapMaterialEnum.Rubber,
    waterResistance: WaterResistanceEnum.FiveHundredMetersPlus,
    warranty: '3 anos',
    stockQuantity: 2,
    isActive: true,
    isFeatured: true,
    isOnSale: false,
    images: [
      {
        id: 7,
        url: 'https://a-static.mlcdn.com.br/1500x1500/relogio-masculino-aventureiros-modernos-encontrem-seu-parceiro-perfeito-aqui-kp/shop2sell/relogiomasculino26/11208b57128ab7666d02b7fd482c590e.jpeg',
        order: 1,
      },
    ],
  },
];
