export interface OrderModel {
  id: number;
  date: Date;
  items: OrderItemModel[];
  total: number;
}

export interface OrderItemModel {
  id: number;
  quantity: number;
  unitPrice: number;
  description: string;
}