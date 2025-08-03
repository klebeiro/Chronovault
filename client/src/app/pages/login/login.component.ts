import { Component, OnInit } from '@angular/core';
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
import { CartService, NotificationService } from '../../shared/services';
import { LoginInputDTO, LoginOutputDTO } from '../../dto/auth';
import { BaseComponentActionDirective } from '../../shared/directives';

interface LoginForm {
  email: FormControl<string>;
  password: FormControl<string>;
}

@Component({
  selector: 'app-login',
  imports: [
    RouterModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatButtonModule,
    SharedModule,
    AuthPageComponent,
    MatProgressSpinnerModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent
  extends BaseComponentActionDirective
  implements OnInit
{
  loginForm!: FormGroup<LoginForm>;

  mode: 'login' | 'register' | 'recover' | 'change-password' = 'login';
  path: string | undefined = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private cartService: CartService
  ) {
    super();
  }

  ngOnInit(): void {
    this.setLoginForm();
  }

  setLoginForm() {
    this.loginForm = this.fb.nonNullable.group<LoginForm>({
      email: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      password: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
    });
  }

  getFormControl(formControlName: keyof LoginForm): FormControl {
    return this.loginForm.get(formControlName) as FormControl;
  }

  onSubmit() {
    if (!this.loginForm.valid) {
      return;
    }

    const formData = this.loginForm.value;

    this.executeActionWithInputAndOutput<LoginInputDTO, LoginOutputDTO>({
      input: {
        email: formData.email!,
        password: formData.password!,
      },
      actionMethod: this.authService.login.bind(this.authService),
      displayErrorNotification: true,
      onSucess: (loginOutputDTO) => this.signIn(loginOutputDTO),
      errorMessage: 'Erro ao fazer login',
    });
  }
  
  signIn(loginOutputDTO: LoginOutputDTO) {
    this.authService.setAuthCredentials({
      token: loginOutputDTO.token,
      user: {
        id: loginOutputDTO.id,
        email: loginOutputDTO.email,
        name: loginOutputDTO.name,
        address: {
          street: loginOutputDTO.address.street,
          addressNumber: loginOutputDTO.address.addressNumber,
          city: loginOutputDTO.address.city,
          state: loginOutputDTO.address.state,
          zipCode: loginOutputDTO.address.zipCode,
        },
      },
    });

    if (this.cartService.cartIsEmpty()) {
      this.router.navigate(['/']);
    } else {
      this.router.navigate(['/checkout']);
    }
  }
}
