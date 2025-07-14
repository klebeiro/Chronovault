import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';

import { AuthPageComponent } from '../../components';

import { SharedModule } from '../../shared/shared.module';

import { AuthService } from '../../services';
import { NotificationService } from '../../shared/services';

interface RegisterForm {
  name: FormControl<string>;
  email: FormControl<string>;
  password: FormControl<string>;
  confirmPassword: FormControl<string>;
}

@Component({
  selector: 'app-register',
  imports: [
    RouterModule,
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    MatButtonModule,
    SharedModule,
    AuthPageComponent,
    MatProgressSpinnerModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup<RegisterForm>;
  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.setregisterForm();
  }

  setregisterForm() {
    this.registerForm = this.fb.nonNullable.group<RegisterForm>({
      name: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      email: this.fb.control('', {
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
    });
  }

  getFormControl(formControlName: keyof RegisterForm): FormControl {
    return this.registerForm.get(formControlName) as FormControl;
  }

  onSubmit() {
    if (!this.registerForm.valid) {
      return;
    }

    const formData = this.registerForm.value;
    this.isLoading = true;

    this.authService
      .login({
        email: formData.email!,
        password: formData.password!,
      })
      .subscribe({
        next: (data) => {
          console.log('data');
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
}
