import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentComponentList } from './student-component-list';

describe('StudentComponentList', () => {
  let component: StudentComponentList;
  let fixture: ComponentFixture<StudentComponentList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentComponentList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentComponentList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
