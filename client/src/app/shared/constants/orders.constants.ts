import { OrderModel } from '../../models';

export const MOCK_ORDERS: OrderModel[] = [
  {
    id: 1,
    date: new Date(),
    items: [
      {
        id: 1,
        description: 'Rolex Explorer Pro 2025',
        unitPrice: 58990,
        quantity: 1,
      },
      {
        id: 2,
        description: 'Rolex Explorer Pro 2025',
        unitPrice: 58990,
        quantity: 1,
      },
      {
        id: 3,
        description: 'Rolex Explorer Pro 2025',
        unitPrice: 58990,
        quantity: 1,
      },
    ],
    total: 58990,
  },
  {
    id: 2,
    date: new Date(),
    items: [
      {
        id: 1,
        description: 'Seiko 1965',
        unitPrice: 1900,
        quantity: 3,
      },
      {
        id: 2,
        description: 'Seiko 456',
        unitPrice: 1900,
        quantity: 1,
      },
    ],
    total: 58990,
  },
];
