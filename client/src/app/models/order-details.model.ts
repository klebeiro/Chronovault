import { PaymentTypeEnum } from "../shared/enums";
import { CompactWatchModel } from "./compact-watch.model";
import { PaymentCardModel } from "./payment-card.model";
import { PurchaseItemModel } from "./purchase-item.model";

export interface OrderDetailsModel {
  id: number;
  date: Date;
  orderNumber: string;
  items: PurchaseItemModel[];
  paymentMethod: PaymentTypeEnum;
  paymentCard: PaymentCardModel | null;
  total: number;
  installmentsNumber: number;
}
