import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentModeComponent } from './payment-mode.component';

describe('PaymentModeComponent', () => {
  let component: PaymentModeComponent;
  let fixture: ComponentFixture<PaymentModeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentModeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentModeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
