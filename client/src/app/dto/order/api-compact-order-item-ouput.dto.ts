export interface ApiCompactOrderItemOutputDTO {
  id: number;
  productId: number;
  quantity: number;
  price: number;

  productInformation: {
    id: number;
    model: string;
    brand: string;
    description: string;
    price: number;
    category: number;
    gender: number;
    movementType: number;
    caseMaterial: number;
    strapMaterial: number;
    waterResistance: number;
    stockQuantity: number;
    isActive: boolean;
    isFeatured: boolean;
    isOnSale: boolean;
    images: string[];
  };
}
