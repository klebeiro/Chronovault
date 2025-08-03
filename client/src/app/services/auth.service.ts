import { HttpClient } from '@angular/common/http';
import { inject, Injectable, InjectionToken, PLATFORM_ID } from '@angular/core';
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
import { DOCUMENT, isPlatformBrowser } from '@angular/common';

function mockLocalStorage(): Storage {
  return {
    length: 0,
    key: () => null,
    getItem: () => null,
    setItem: () => null,
    removeItem: () => null,
    clear: () => null,
  };
}

const LOCAL_STORAGE_TOKEN = new InjectionToken<Storage>('LOCAL_STORAGE_TOKEN', {
  factory: () => {
    const platformId = inject(PLATFORM_ID);
    const isBrowser = isPlatformBrowser(platformId);
    const document = inject(DOCUMENT);

    if (isBrowser) {
      return document.defaultView?.localStorage ?? mockLocalStorage();
    } else {
      return mockLocalStorage();
    }
  },
});

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _storage = inject(LOCAL_STORAGE_TOKEN);
  readonly enabled = isPlatformBrowser(inject(PLATFORM_ID));

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

    return this.httpClient
      .post<ApiLoginOutputDTO>(completeApiUrl, requestBody)
      .pipe(
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
            },
          };
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
    this._storage.setItem('authCredentials', JSON.stringify(credentials));
  }

  public removeAuthCredentials() {
    this.authCredentials.next(null);
    this._storage.removeItem('authCredentials');
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

  public checkStoredAuthCredentials() {
    const storedCredentials = this._storage.getItem('authCredentials');

    if (storedCredentials !== null) {
      this.setAuthCredentials(JSON.parse(storedCredentials));
    }
  }
}
