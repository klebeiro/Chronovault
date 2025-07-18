import { CaseMaterialEnum, MovementTypeEnum, ProductGenderEnum, StrapMaterialEnum, WatchCategoryEnum, WaterResistanceEnum } from "../shared/enums";

export interface WatchImageModel {
  id: number;
  url: string;
  order: number;
}

export interface WatchDetailsModel {
  id: number;
  model: string;
  brand: string;
  description: string;
  price: number;
  category: WatchCategoryEnum;
  gender: ProductGenderEnum;
  movementType: MovementTypeEnum;
  caseMaterial: CaseMaterialEnum;
  strapMaterial: StrapMaterialEnum;
  waterResistance: WaterResistanceEnum;
  warranty: string | null;
  stockQuantity: number;
  isActive: boolean;
  isFeatured: boolean;
  isOnSale: boolean;
  images: WatchImageModel[];
}
