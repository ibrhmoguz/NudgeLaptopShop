import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { LaptopConfigurationDialogComponent } from "./laptop-configuration-dialog.component";
import { LaptopService } from "../services";
import { MockLaptopService } from "../testing/mock-laptop.service";
import { RouterTestingModule } from "@angular/router/testing";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { HttpClientModule } from "@angular/common/http";
import { CdkTableModule } from "@angular/cdk/table";
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatButtonModule } from "@angular/material/button";
import {
  MatDialogModule,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from "@angular/material/dialog";
import { MatSnackBarModule } from "@angular/material/snack-bar";

describe("LaptopConfigurationDialogComponent", () => {
  let component: LaptopConfigurationDialogComponent;
  let fixture: ComponentFixture<LaptopConfigurationDialogComponent>;
  const laptop = { id: 1, name: "Hp", price: 123.34 };

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        FontAwesomeModule,
        HttpClientModule,
        CdkTableModule,
        MatTableModule,
        MatPaginatorModule,
        MatButtonModule,
        MatDialogModule,
        MatSnackBarModule,
      ],
      declarations: [LaptopConfigurationDialogComponent],
      providers: [
        {
          provide: LaptopService,
          useClass: MockLaptopService,
        },
        { provide: MatDialogRef, useValue: {} },
        { provide: MAT_DIALOG_DATA, useValue: laptop },
      ],
    }).compileComponents();
  }));

  it("should create component", () => {
    fixture = TestBed.createComponent(LaptopConfigurationDialogComponent);
    component = fixture.componentInstance;
    expect(component).toBeTruthy();
  });
});
