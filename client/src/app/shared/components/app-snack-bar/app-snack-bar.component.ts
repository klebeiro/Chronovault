import { Component, Inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MAT_SNACK_BAR_DATA, MatSnackBarRef } from '@angular/material/snack-bar';
import { Notification } from '../../types';
import { NotificationTypeEnum } from '../../enums';
import { NgClass } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-snack-bar',
  imports: [MatIconModule, NgClass, MatButtonModule],
  templateUrl: './app-snack-bar.component.html',
  styleUrl: './app-snack-bar.component.scss',
})
export class AppSnackBarComponent {
  iconName: string = 'close';
  notification: Notification | null = null;
  templateRef: MatSnackBarRef<AppSnackBarComponent> | null = null;


  public constructor(@Inject(MAT_SNACK_BAR_DATA) public data: Notification, @Inject(MatSnackBarRef) public matSnackBarRef: MatSnackBarRef<AppSnackBarComponent>) {
    this.notification = data;
    this.templateRef = matSnackBarRef;
  }

  getIsSuccess(): boolean {
    return this.notification?.type === NotificationTypeEnum.Success;
  }

  getIsError(): boolean {
    return this.notification?.type === NotificationTypeEnum.Error;
  }

  getIsWarning(): boolean {
    return this.notification?.type === NotificationTypeEnum.Warning;
  }

  close() {
    this.templateRef?.dismiss();
  }
}
