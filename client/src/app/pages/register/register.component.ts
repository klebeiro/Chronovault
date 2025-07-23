import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
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

import { SharedModule } from '../../shared/shared.module';

import { AuthService } from '../../services';
import { MatStep, MatStepper, MatStepperModule } from '@angular/material/stepper';
import { mask } from '../../shared/utils';
import { StateModel } from '../../models';
import { brazillianStates } from '../../shared/constants';
import { SelectOption } from '../../shared/types';
import { BaseComponentActionDirective } from '../../shared/directives';
import { RegisterInputDTO } from '../../dto/auth';

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
  state: FormControl<StateModel | null>;
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
    MatStepperModule,
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent
  extends BaseComponentActionDirective
  implements OnInit
{
  userInfoForm!: FormGroup<UserInfoForm>;
  addressForm!: FormGroup<AddressForm>;
  stateOptions: SelectOption<StateModel>[] = [];
  @ViewChild('stepper') stepper: MatStepper | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    super();
  }

  ngOnInit(): void {
    this.setUserInfoForm();
    this.setAddressForm();
    this.setStates();
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
    if (this.userInfoForm.get(formControlName) === null) {
      return this.addressForm.get(formControlName) as FormControl;
    }

    return this.userInfoForm.get(formControlName) as FormControl;
  }

  getZipCodeMask() {
    return mask.zipCode;
  }

  submitUserInfoForm() {
    if(this.userInfoForm.valid && this.stepper) {
      this.stepper.next();
    }
  }

  registerUser() {
    if (!this.userInfoForm.valid || !this.addressForm.valid) {
      this.showErrorNotification({
        title: 'Erro ao fazer cadastro',
        description: 'Preencha todos os campos',
      });

      return;
    }

    const userFormInfo = this.userInfoForm.value;
    const addressFormInfo = this.addressForm.value;

    const registerInput: RegisterInputDTO = {
      name: userFormInfo.name!,
      email: userFormInfo.email!,
      password: userFormInfo.password!,
      confirmPassword: userFormInfo.confirmPassword!,
      address: {
        zipCode: addressFormInfo.zipCode!,
        street: addressFormInfo.street!,
        addressNumber: addressFormInfo.number!,
        neighborhood: addressFormInfo.neighborhood!,
        city: addressFormInfo.city!,
        state: addressFormInfo.state?.abbreviation!,
        complement: addressFormInfo.complement || null,
      },
    };

    console.log('Regioster: ', registerInput);

    this.executeActionWithInput<RegisterInputDTO>({
      actionMethod: this.authService.register.bind(this.authService),
      displayErrorNotification: true,
      errorMessage: 'Erro ao fazer cadastro',
      input: registerInput,
      onSucess: () => {
        this.showSuccessNotification({
          title: 'Cadastro realizado com sucesso',
          description: 'Faça login para continuar',
        })
        this.router.navigate(['/login']);
      },
    });
  }
}
