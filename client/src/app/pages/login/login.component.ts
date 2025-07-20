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
  Validators
} from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';

import { AuthPageComponent } from '../../components';

import { SharedModule } from '../../shared/shared.module';

import { AuthService } from '../../services';
import { CartService, NotificationService } from '../../shared/services';
import { LoginOutputDTO } from '../../dto/auth';

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
    MatProgressSpinnerModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup<LoginForm>;

  mode: 'login' | 'register' | 'recover' | 'change-password' = 'login';
  path: string | undefined = '';

  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private notificationService: NotificationService,
    private router: Router,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    this.setLoginForm();
  }

  form = {
    email: '',
    confirmEmail: '',
    password: '',
    confirmPassword: '',
  };

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
    this.isLoading = true;

    this.authService
      .login({
        email: formData.email!,
        password: formData.password!,
      })
      .subscribe({
        next: (data) => {
          this.signIn(data);
        },
        error: (error) => {
          this.notificationService.showErrorNotification({
            title: 'Erro ao fazer login',
            description: error.error,
          });
          this.isLoading = false;
        },
        complete: () => {
          this.isLoading = false;
        }
      });
  }

  signIn(loginOutputDTO: LoginOutputDTO){
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
        }
      }
    })

    if(this.cartService.cartIsEmpty()){
      this.router.navigate(['/']);
    } else {
      this.router.navigate(['/checkout']);
    }
  }
}
