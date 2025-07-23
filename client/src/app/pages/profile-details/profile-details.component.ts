import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  ReactiveFormsModule,
  FormsModule,
  FormControl,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatStep, MatStepperModule } from '@angular/material/stepper';
import { RouterModule } from '@angular/router';
import { AuthPageComponent } from '../../components';
import { SharedModule } from '../../shared';
import { StateModel } from '../../models';
import { AuthService } from '../../services';
import { brazillianStates } from '../../shared/constants';
import { SelectOption } from '../../shared/types';
import { mask } from '../../shared/utils';

interface UserInfoForm {
  name: FormControl<string>;
  email: FormControl<string>;
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
  selector: 'app-profile-details',
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
    MatStepperModule,
  ],
  templateUrl: './profile-details.component.html',
  styleUrl: './profile-details.component.scss',
})
export class ProfileDetailsComponent {
  userInfoForm!: FormGroup<UserInfoForm>;
  addressForm!: FormGroup<AddressForm>;
  stateOptions: SelectOption<StateModel>[] = [];

  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.setUserInfoForm();
    this.setAddressForm();
    this.setStates();
    this.subscribeToAuthCredentialsChange();
  }

  setStates() {
    this.stateOptions = brazillianStates.map((state) => ({
      label: state.name,
      value: state,
      viewValue: state.name,
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
    if (this.userInfoForm.get(formControlName) === null) {
      return this.addressForm.get(formControlName) as FormControl;
    }

    return this.userInfoForm.get(formControlName) as FormControl;
  }

  getZipCodeMask() {
    return mask.zipCode;
  }

  subscribeToAuthCredentialsChange(){
    this.authService.getAuthCredentials$().subscribe((credentials) => {
      if (credentials) {
        console.log('credentials', credentials);

        this.userInfoForm.patchValue({
          name: credentials.user.name,
          email: credentials.user.email
        });

        this.addressForm.patchValue({
          zipCode: credentials.user.address.zipCode,
          street: credentials.user.address.street,
          number: credentials.user.address.addressNumber,
          neighborhood: '',
          city: credentials.user.address.city,
          state: credentials.user.address.state,
          complement: ''
        });
      }
    });
  }
}
