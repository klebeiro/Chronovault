import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseItemCardComponent } from './purchase-item-card.component';

describe('PurchaseItemCardComponent', () => {
  let component: PurchaseItemCardComponent;
  let fixture: ComponentFixture<PurchaseItemCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PurchaseItemCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurchaseItemCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
