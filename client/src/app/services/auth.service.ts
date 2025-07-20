import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { LoginInputDTO, LoginOutputDTO, RegisterInputDTO } from '../dto/auth';
import { WebServiceConfigService } from '../shared/services';
import { AuthCredentialsModel, UserModel } from '../models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authCredentials: BehaviorSubject<AuthCredentialsModel | null> =
    new BehaviorSubject<AuthCredentialsModel | null>(null);

  constructor(
    private httpClient: HttpClient,
    private webServiceConfigService: WebServiceConfigService
  ) {}

  public login(input: LoginInputDTO): Observable<LoginOutputDTO> {
    const completeApiUrl =
      this.webServiceConfigService.getCompleteApiUrl('/auth/login');

    // return this.httpClient.post<LoginOutputDTO>(completeApiUrl, {
    //   email: input.email,
    //   password: input.password,
    // });

    // simulate login
    return new Observable<LoginOutputDTO>((observer) => {
      observer.next({
        id: 1,
        email: input.email,
        name: 'John Doe',
        token: 'token',
        address: {
          addressNumber: '123',
          city: 'City',
          state: 'State',
          street: 'Street',
          zipCode: '12345',
        },
      });
    });
  }

  public register(input: RegisterInputDTO): Observable<void> {
    const completeApiUrl =
      this.webServiceConfigService.getCompleteApiUrl('/auth/register');

    return new Observable((observer) => {
      observer.next();
    });
  }

  public setAuthCredentials(credentials: AuthCredentialsModel) {
    this.authCredentials.next(credentials);
  }

  public removeAuthCredentials() {
    this.authCredentials.next(null);
  }

  public getAuthCredentials$(): Observable<AuthCredentialsModel | null> {
    return this.authCredentials;
  }

  public getAuthCredentials(): AuthCredentialsModel | null {
    return this.authCredentials.value;
  }

  public getIsAuthenticated(): boolean {
    return this.getAuthCredentials() !== null;
  }
}
