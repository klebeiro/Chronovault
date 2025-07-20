import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseItensComponent } from './purchase-itens.component';

describe('PurchaseItensComponent', () => {
  let component: PurchaseItensComponent;
  let fixture: ComponentFixture<PurchaseItensComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PurchaseItensComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurchaseItensComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
