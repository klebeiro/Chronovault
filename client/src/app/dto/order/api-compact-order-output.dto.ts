import { ApiCompactOrderItemOutputDTO } from './api-compact-order-item-ouput.dto';

export interface ApiCompactOrderOutputDTO {
  id: number;
  userId: number;
  createdAt: string;
  totalAmount: number;
  orderNumber: string;
  status: number;
  orderItems: ApiCompactOrderItemOutputDTO[];
}
