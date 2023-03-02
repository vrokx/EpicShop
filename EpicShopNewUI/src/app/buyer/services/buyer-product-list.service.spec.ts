import { TestBed } from '@angular/core/testing';

import { BuyerProductListService } from './buyer-product-list.service';

describe('BuyerProductListService', () => {
  let service: BuyerProductListService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BuyerProductListService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
