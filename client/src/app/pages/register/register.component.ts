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
import { MatStep, MatStepperModule } from '@angular/material/stepper';
import { mask } from '../../shared/utils';
import { StateModel } from '../../models';
import { brazillianStates } from '../../shared/constants';
import { SelectOption } from '../../shared/types';

interface UserInfoForm {
  name: FormControl<string>;
  email: FormControl<string>;
  password: FormControl<string>;
  confirmPassword: FormControl<string>;
}

interface AddressForm {
  zipCode: FormControl<string>;
  street: FormControl<string>;
  number: FormControl<string>;
  neighborhood: FormControl<string>;
  city: FormControl<string>;
  state: FormControl<string | null>;
  complement: FormControl<string | null>;
} 

@Component({
  selector: 'app-register',
  imports: [
    RouterModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatButtonModule,
    SharedModule,
    AuthPageComponent,
    MatProgressSpinnerModule,
    MatStep,
    MatStepperModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  userInfoForm!: FormGroup<UserInfoForm>;
  addressForm!: FormGroup<AddressForm>;
  stateOptions: SelectOption<StateModel>[] = [];

  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.setUserInfoForm();
    this.setAddressForm();
    this.setStates();
  }

  setStates(){
    this.stateOptions = brazillianStates.map((state) => ({
      label: state.name,
      value: state,
      viewValue: state.name
    }));
  }

  setUserInfoForm() {
    this.userInfoForm = this.fb.nonNullable.group<UserInfoForm>({
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

  setAddressForm() {
    this.addressForm = this.fb.group<AddressForm>({
      zipCode: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      street: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      number: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      neighborhood: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      city: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      state: this.fb.control(null, {
        validators: [Validators.required],
      }),
      complement: this.fb.control(''),
    });
  }

  getFormControl(formControlName: string): FormControl {
    if(this.userInfoForm.get(formControlName) === null) {
      return this.addressForm.get(formControlName) as FormControl;
    }

    return this.userInfoForm.get(formControlName) as FormControl;
  }

  getZipCodeMask() {
    return mask.zipCode;
  }

  onSubmit() {
    if (!this.userInfoForm.valid) {
      return;
    }

    const formData = this.userInfoForm.value;
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
