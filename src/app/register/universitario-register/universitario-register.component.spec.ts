import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversitarioRegisterComponent } from './universitario-register.component';

describe('UniversitarioRegisterComponent', () => {
  let component: UniversitarioRegisterComponent;
  let fixture: ComponentFixture<UniversitarioRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UniversitarioRegisterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UniversitarioRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
