import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuyerProductListComponent } from './buyer-product-list.component';

describe('BuyerProductListComponent', () => {
  let component: BuyerProductListComponent;
  let fixture: ComponentFixture<BuyerProductListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BuyerProductListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BuyerProductListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
