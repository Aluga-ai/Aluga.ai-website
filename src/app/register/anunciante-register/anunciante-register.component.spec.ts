import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnuncianteRegisterComponent } from './anunciante-register.component';

describe('AnuncianteRegisterComponent', () => {
  let component: AnuncianteRegisterComponent;
  let fixture: ComponentFixture<AnuncianteRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AnuncianteRegisterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AnuncianteRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
