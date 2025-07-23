import { Component, OnInit, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { AuthPageComponent } from '../../components';
import { SharedModule } from '../../shared';
import { AuthService } from '../../services';
import { NotificationService } from '../../shared/services';

interface RequestPasswordRecoveryForm {
  email: FormControl<string>;
}

interface ConfirmationCodeForm {
  code: FormControl<string>;
  password: FormControl<string>;
  confirmPassword: FormControl<string>;
}

enum FormStep {
  REQUEST_PASSWORD_RECOVERY = 0,
  CONFIRMATION_CODE = 1,
}

@Component({
  selector: 'forgot-password',
  imports: [
    RouterModule,
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    MatButtonModule,
    SharedModule,
    AuthPageComponent,
    MatProgressSpinnerModule,
    MatStepperModule,
  ],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss',
})
export class ForgotPasswordComponent {
  requestPasswordRecoveryForm!: FormGroup<RequestPasswordRecoveryForm>;
  confirmationCodeForm!: FormGroup<ConfirmationCodeForm>;
  isLoading: boolean = false;
  isLinear: boolean = false;
  currentFormStep: FormStep = FormStep.REQUEST_PASSWORD_RECOVERY;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.setRequestPasswordRecoveryForm();
    this.setConfirmationCodeForm();
  }

  setRequestPasswordRecoveryForm() {
    this.requestPasswordRecoveryForm =
      this.fb.nonNullable.group<RequestPasswordRecoveryForm>({
        email: this.fb.control('', {
          nonNullable: true,
          validators: [Validators.required],
        }),
      });
  }

  setConfirmationCodeForm() {
    this.confirmationCodeForm = this.fb.nonNullable.group<ConfirmationCodeForm>(
      {
        code: this.fb.control('', {
          nonNullable: true,
          validators: [Validators.required],
        }),
        password: this.fb.control('', {
          nonNullable: true,
          validators: [Validators.required],
        }),
        confirmPassword: this.fb.control('', {
          nonNullable: true,
          validators: [Validators.required],
        }),
      }
    );
  }

  getRequestPasswordFormFormControl(name: keyof RequestPasswordRecoveryForm) {
    return this.requestPasswordRecoveryForm.get(name) as FormControl;
  }

  getConfirmationCodeFormFormControl(name: keyof ConfirmationCodeForm) {
    return this.confirmationCodeForm.get(name) as FormControl;
  }

  getIsRequestPasswordRecoveryStep() {
    return this.currentFormStep === FormStep.REQUEST_PASSWORD_RECOVERY;
  }

  getIsConfirmationCodeStep() {
    return this.currentFormStep === FormStep.CONFIRMATION_CODE;
  }

  onSubmitRequestPasswordRecovery() {
    if (!this.requestPasswordRecoveryForm.valid) {
      return;
    }

    const formData = this.requestPasswordRecoveryForm.value;
    this.isLoading = true;

    this.authService
      .login({
        email: formData.email!,
        password: '',
      })
      .subscribe({
        next: (data) => {
          this.isLoading = false;
        },
        error: (error) => {
          this.notificationService.showErrorNotification({
            title: 'Erro ao fazer login',
            description: error.error,
          });
          this.isLoading = false;
        },
      });
  }

  onSubmitChangePassword() {
    if (!this.confirmationCodeForm.valid) {
      return;
    }
  }
}
