import { Directive, inject } from '@angular/core';

import { NotificationService, WebServiceConfigService } from '../../services';
import { Notification } from '../../types';
import { Observable } from 'rxjs';

export interface IActionWithInput<Input> {
  input: Input;
  actionMethod: (input: Input) => Observable<void>;
  onSucess: () => void;
  displayErrorNotification: boolean;
  errorMessage: string;
}

export interface IActionWithInputAndOutput<Input, Output> {
  input: Input;
  actionMethod: (input: Input) => Observable<Output>;
  onSucess: (output: Output) => void;
  displayErrorNotification: boolean;
  errorMessage: string;
}

@Directive({
  selector: '[baseComponentAction]',
})
export class BaseComponentActionDirective {
  isLoading: boolean = false;
  private notificationService: NotificationService =
    inject(NotificationService);

  constructor() {}

  showSuccessNotification(notification: Notification) {
    this.notificationService.showSuccessNotification({
      title: notification.title,
      description: notification.description,
    });
  }

  showErrorNotification(notification: Notification) {
    this.notificationService.showErrorNotification({
      title: notification.title,
      description: notification.description,
    });
  }

  executeActionWithInputAndOutput<Input, Output>(
    params: IActionWithInputAndOutput<Input, Output>
  ) {
    const {
      actionMethod,
      displayErrorNotification,
      errorMessage,
      input,
      onSucess,
    } = params;

    this.isLoading = true;

    actionMethod(input).subscribe({
      next: (data) => {
        this.isLoading = false;
        onSucess(data);
      },
      error: (error) => {
        if (displayErrorNotification) {
          this.showErrorNotification({
            title: errorMessage,
            description: error.error,
          });
        }

        this.isLoading = false;
      },
    });
  }

  executeActionWithInput<Input>(params: IActionWithInput<Input>) {
    const {
      actionMethod,
      displayErrorNotification,
      errorMessage,
      input,
      onSucess,
    } = params;

    this.isLoading = true;

    actionMethod(input).subscribe({
      next: (data) => {
        this.isLoading = false;
        onSucess();
      },
      error: (error) => {
        if (displayErrorNotification) {
          this.showErrorNotification({
            title: errorMessage,
            description: error.error,
          });
        }

        this.isLoading = false;
      },

      complete: () => {},
    });
  }

  executeActionWithResponse<Output>() {}

  executeActionWithoutResponse() {}
}
