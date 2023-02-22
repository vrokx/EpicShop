import { TestBed } from '@angular/core/testing';

import { DisplayAllProductsService } from './display-all-products.service';

describe('DisplayAllProductsService', () => {
  let service: DisplayAllProductsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DisplayAllProductsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
