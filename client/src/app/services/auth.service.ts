import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';

import {
  ApiLoginInputDTO,
  ApiLoginOutputDTO,
  ApiRegisterInputDTO,
  LoginInputDTO,
  LoginOutputDTO,
  RegisterInputDTO,
} from '../dto/auth';
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
      this.webServiceConfigService.getCompleteApiUrl('User/login');

    const requestBody: ApiLoginInputDTO = {
      email: input.email,
      password: input.password,
    };

    return this.httpClient.post<ApiLoginOutputDTO>(completeApiUrl, requestBody).pipe(
      map<ApiLoginOutputDTO, LoginOutputDTO>((apiResponse) => {
        return {
          id: apiResponse.user.id,
          email: apiResponse.user.email,
          name: apiResponse.user.name,
          token: apiResponse.token,
          address: {
            addressNumber: apiResponse.user.address.number,
            city: apiResponse.user.address.city,
            state: apiResponse.user.address.state,
            street: apiResponse.user.address.street,
            zipCode: apiResponse.user.address.zipCode,
          }
        }
      })
    );
  }

  public register(input: RegisterInputDTO): Observable<void> {
    const completeApiUrl =
      this.webServiceConfigService.getCompleteApiUrl('User/create');

    const requestBody: ApiRegisterInputDTO = {
      name: input.name,
      email: input.email,
      password: input.password,
      birthDate: '2023-01-01',
      cpf: '12345678901',
      passwordConfirmation: input.confirmPassword,
      phone: '88992146067',
      address: {
        city: input.address.city,
        complement: input.address.complement,
        country: '',
        number: input.address.addressNumber,
        state: input.address.state,
        street: input.address.street,
        zipCode: input.address.zipCode,
      },
    };

    return this.httpClient.post<void>(completeApiUrl, requestBody);
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
