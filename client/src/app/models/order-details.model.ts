import { PaymentTypeEnum } from "../shared/enums";
import { CompactWatchModel } from "./compact-watch.model";
import { PaymentCardModel } from "./payment-card.model";

export interface OrderDetailsModel {
  id: number;
  date: Date;
  items: CompactWatchModel[];
  paymentMethod: PaymentTypeEnum;
  paymentCard: PaymentCardModel;
  total: number;
}

