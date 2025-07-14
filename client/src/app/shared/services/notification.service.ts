import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Notification } from '../types';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor(private matSnackBar: MatSnackBar) {}

  showNotification(notification: Notification) {
    this.matSnackBar.open(notification.title, 'Close', {
      duration: 3000,
    });
  }
  showSuccessNotification(notification: Notification) {
    this.matSnackBar.open(notification.title, 'Close', {
      duration: 3000,
      panelClass: 'success',
    });
  }

  showErrorNotification(notification: Notification) {
    this.matSnackBar.open(notification.title, 'Close', {
      duration: 3000,
      panelClass: 'error',
    });
  }
}
