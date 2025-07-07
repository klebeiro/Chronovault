import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule }  from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [RouterModule, CommonModule, HttpClientModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  mode: 'login' | 'register' | 'recover' | 'change-password' = 'login';

  path: string | undefined = '';

  constructor(private route: ActivatedRoute, private http: HttpClient) {
    this.path = this.route.snapshot.routeConfig?.path;
    if (this.path === 'register') this.mode = 'register';
    else if (this.path === 'recover') this.mode = 'recover'
    else if (this.path === 'change-password') this.mode = 'change-password';
    else this.mode = 'login';
  }

  form = {
    email: '',
    confirmEmail: '',
    password: '',
    confirmPassword: '',
  };

  onSubmit() {
    this.http.post(`https://localhost:7115/api/User/${this.path}`, this.form).subscribe(
      response => {
        console.log('Login OK:', response);
      },
      error => {
        console.error('Erro:', error);
      }
    );
  }
}
