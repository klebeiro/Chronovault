import { TestBed } from '@angular/core/testing';

import { FormErrorServiceService } from './form-error-service.service';

describe('FormErrorServiceService', () => {
  let service: FormErrorServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormErrorServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
