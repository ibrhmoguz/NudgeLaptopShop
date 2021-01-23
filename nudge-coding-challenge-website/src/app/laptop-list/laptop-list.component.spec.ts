import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatButtonModule } from "@angular/material/button";
import { MatDialogModule } from "@angular/material/dialog";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { HttpClientModule } from "@angular/common/http";
import { LaptopListComponent } from "./laptop-list.component";
import { LaptopConfigurationDialogComponent } from "./../laptop-configuration-dialog/laptop-configuration-dialog.component";
import { LaptopService } from "../services";
import { MockLaptopService } from "../testing/mock-laptop.service";
import { RouterTestingModule } from "@angular/router/testing";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { CdkTableModule } from "@angular/cdk/table";

describe("LaptopListComponent", () => {
  let component: LaptopListComponent;
  let fixture: ComponentFixture<LaptopListComponent>;

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
      declarations: [LaptopListComponent, LaptopConfigurationDialogComponent],
      providers: [
        {
          provide: LaptopService,
          useClass: MockLaptopService,
        },
      ],
    }).compileComponents();
  }));

  it("should create component", () => {
    fixture = TestBed.createComponent(LaptopListComponent);
    component = fixture.componentInstance;
    expect(component).toBeTruthy();
  });
});
