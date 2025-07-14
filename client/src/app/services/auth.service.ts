import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { LoginInputDTO, LoginOutputDTO } from '../dto/auth';
import { WebServiceConfigService } from '../shared/services';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private httpClient: HttpClient,
    private webServiceConfigService: WebServiceConfigService
  ) {}

  public login(input: LoginInputDTO): Observable<LoginOutputDTO> {
    const completeApiUrl =
      this.webServiceConfigService.getCompleteApiUrl('/auth/login');

    return this.httpClient.post<LoginOutputDTO>(completeApiUrl, {
      email: input.email,
      password: input.password,
    });
  }
}
