import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { LaptopConfigurationDialogComponent } from "./laptop-configuration-dialog.component";

describe("LaptopListComponent", () => {
  let component: LaptopConfigurationDialogComponent;
  let fixture: ComponentFixture<LaptopConfigurationDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [LaptopConfigurationDialogComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LaptopConfigurationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
