import { Injectable } from '@angular/core';
import { environment } from '../../../environment';

@Injectable({
  providedIn: 'root'
})
export class WebServiceConfigService {

  constructor() { }

  public getApiUrl(): string {
    return environment.apiURL;
  }

  public getCompleteApiUrl(url: string): string {
    return this.getApiUrl() + url;
  }
}
