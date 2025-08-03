import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Notification } from '../types';
import { AppSnackBarComponent } from '../components';
import { NotificationTypeEnum } from '../enums';

const SNACK_BAR_DURATION = 10000;

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor(private matSnackBar: MatSnackBar) {}

  showNotification(notification: Omit<Notification, 'type'>) {
    this.matSnackBar.openFromComponent<AppSnackBarComponent, Notification>(
      AppSnackBarComponent,
      {
        duration: SNACK_BAR_DURATION,
        panelClass: ['notification-snackbar', 'warning'],
        data: {
          title: notification.title,
          description: notification.description,
          type: NotificationTypeEnum.Warning,
        },
      }
    );
  }
  showSuccessNotification(notification: Omit<Notification, 'type'>) {
    this.matSnackBar.openFromComponent<AppSnackBarComponent, Notification>(
      AppSnackBarComponent,
      {
        duration: SNACK_BAR_DURATION,
        panelClass: ['notification-snackbar', 'sucess'],
        data: {
          title: notification.title,
          description: notification.description,
          type: NotificationTypeEnum.Success,
        },
      }
    );
  }

  showErrorNotification(notification: Omit<Notification, 'type'>) {
    this.matSnackBar.openFromComponent<AppSnackBarComponent, Notification>(
      AppSnackBarComponent,
      {
        duration: SNACK_BAR_DURATION,
        panelClass: ['notification-snackbar', 'error'],
        data: {
          title: notification.title,
          description: notification.description,
          type: NotificationTypeEnum.Error,
        },
      }
    );
  }
}
