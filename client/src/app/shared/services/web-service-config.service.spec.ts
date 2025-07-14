import { TestBed } from '@angular/core/testing';

import { WebServiceConfigService } from './web-service-config.service';

describe('WebServiceConfigService', () => {
  let service: WebServiceConfigService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WebServiceConfigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
