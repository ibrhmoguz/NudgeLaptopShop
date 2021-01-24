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
import { MatSnackBarModule, MatSnackBar } from "@angular/material/snack-bar";
import { MockMatDialogRef } from "../testing/mock-mat-dialogRef";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

describe("LaptopConfigurationDialogComponent", () => {
  let component: LaptopConfigurationDialogComponent;
  let fixture: ComponentFixture<LaptopConfigurationDialogComponent>;
  const laptop = { id: 1, name: "Hp", price: 123.34 };

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
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
        { provide: MatDialogRef, useClass: MockMatDialogRef },
        { provide: MAT_DIALOG_DATA, useValue: laptop },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LaptopConfigurationDialogComponent);
    component = fixture.componentInstance;
  });

  it("should create component", () => {
    expect(component).toBeTruthy();
  });

  it("should laptop configurations", () => {
    component.ngOnInit();

    expect(component.laptopConfigurationList.data.length).toBe(6);
    expect(component.laptopConfigurationList.data[0].id).toBe(1);
    expect(component.laptopConfigurationList.data[0].name).toBe("Ram");
    expect(component.laptopConfigurationList.data[0].value).toBe("8GB");
    expect(component.laptopConfigurationList.data[0].price).toBe(123.34);

    expect(component.total).toBe(laptop.price);
    expect(component.selectedLaptopConfigurations.length).toBe(0);
  });

  it("should select configuration and update total price", () => {
    component.ngOnInit();

    expect(component.total).toBe(laptop.price);
    expect(component.selectedLaptopConfigurations.length).toBe(0);

    const conf = { id: 4, name: "HDD", value: "500GB", price: 723.56 };
    component.selectConfiguration(conf);

    expect(component.total).toBe(laptop.price + conf.price);
    expect(component.selectedLaptopConfigurations.length).toBe(1);
  });

  it("should deselect configuration and update total price", () => {
    component.ngOnInit();

    expect(component.total).toBe(laptop.price);
    expect(component.selectedLaptopConfigurations.length).toBe(0);

    const conf = { id: 4, name: "HDD", value: "500GB", price: 723.56 };
    component.selectConfiguration(conf);

    expect(component.total).toBe(laptop.price + conf.price);
    expect(component.selectedLaptopConfigurations.length).toBe(1);

    component.deselectConfiguration(conf);

    expect(component.total).toBe(laptop.price);
    expect(component.selectedLaptopConfigurations.length).toBe(0);
  });

  it("should add basket", () => {
    const snackBar = fixture.debugElement.injector.get(MatSnackBar);
    spyOn(snackBar, "open");

    component.ngOnInit();
    const conf = { id: 4, name: "HDD", value: "500GB", price: 723.56 };
    component.selectConfiguration(conf);
    component.addToBasket();

    expect(snackBar.open).toHaveBeenCalledWith(
      `${laptop.name} laptop added to basket! Total:£ 1427.13`,
      "",
      {
        duration: 2000,
        horizontalPosition: "center",
        verticalPosition: "top",
      }
    );
  });

  it("should not be added same configuration", () => {
    component.ngOnInit();
    const conf = { id: 4, name: "HDD", value: "500GB", price: 723.56 };
    component.selectConfiguration(conf);

    const result = component.configurationExist(conf);

    expect(result).toBe(false);
  });

  it("should be added configuration", () => {
    component.ngOnInit();
    const conf = { id: 4, name: "HDD", value: "500GB", price: 723.56 };
    component.selectConfiguration(conf);

    const confRam = { id: 4, name: "Ram", value: "8GB", price: 123.56 };
    const result = component.configurationExist(confRam);

    expect(result).toBe(true);
  });

  it("should be added first configuration", () => {
    component.ngOnInit();
    const conf = { id: 4, name: "HDD", value: "500GB", price: 723.56 };
    const result = component.configurationExist(conf);

    expect(result).toBe(true);
  });
});
