import { NotificationTypeEnum } from '../enums';

export interface Notification {
  title: string;
  description: string | null;
  type: NotificationTypeEnum;
}
