import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatButtonModule } from "@angular/material/button";
import { MatDialogModule, MatDialog } from "@angular/material/dialog";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { HttpClientModule } from "@angular/common/http";
import { LaptopListComponent } from "./laptop-list.component";
import { LaptopConfigurationDialogComponent } from "./../laptop-configuration-dialog/laptop-configuration-dialog.component";
import { LaptopService } from "../services";
import { MockLaptopService } from "../testing/mock-laptop.service";
import { RouterTestingModule } from "@angular/router/testing";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { CdkTableModule } from "@angular/cdk/table";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MockMatDialog } from "../testing/mock-mat-dialog";

describe("LaptopListComponent", () => {
  let component: LaptopListComponent;
  let fixture: ComponentFixture<LaptopListComponent>;

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
      declarations: [LaptopListComponent, LaptopConfigurationDialogComponent],
      providers: [
        {
          provide: LaptopService,
          useClass: MockLaptopService,
        },
        {
          provide: MatDialog,
          useClass: MockMatDialog,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LaptopListComponent);
    component = fixture.componentInstance;
  });

  it("should create component", () => {
    expect(component).toBeTruthy();
  });

  it("should load laptop list", () => {
    component.ngOnInit();

    expect(component.laptopList.data.length).toBe(4);
    expect(component.laptopList.data[0].id).toBe(1);
    expect(component.laptopList.data[0].name).toBe("Hp");
    expect(component.laptopList.data[0].price).toBe(123.34);
  });

  it("should open laptop configuration dialog", () => {
    const dialog = fixture.debugElement.injector.get(MatDialog);
    const dialogSpy = spyOn(dialog, "open").and.returnValue({});

    const laptop = { id: 2, name: "Dell", price: 234.67 };
    component.openLaptopConfigurationModal(laptop);

    expect(dialogSpy).toHaveBeenCalled();
  });
});
