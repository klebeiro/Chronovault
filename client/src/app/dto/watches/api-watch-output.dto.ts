export interface ApiWatchOutputDTO {
  id: number;
  model: string;
  brand: string;
  description: string;
  price: number;
  category: number;
  gender: number;
  movementType: number;
  caseMaterial: string;
  strapMaterial: string;
  waterResistance: string;
  stockQuantity: number;
  isActive: boolean;
  isFeatured: boolean;
  isOnSale: boolean;
  images: string[];
}
