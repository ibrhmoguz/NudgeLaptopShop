import { Component, OnInit, ViewChild, Inject } from "@angular/core";
import { Subject, of } from "rxjs";
import { takeUntil, tap, catchError, finalize } from "rxjs/operators";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { LaptopService } from "../services/laptop.service";
import { Laptop } from "../models/laptop";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { LaptopConfiguration } from "../models/laptop-configuration";
import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from "@angular/material/snack-bar";

@Component({
  selector: "laptop-configuration-dialog",
  templateUrl: "./laptop-configuration-dialog.component.html",
  styleUrls: ["./laptop-configuration-dialog.component.scss"],
})
export class LaptopConfigurationDialogComponent implements OnInit {
  public laptopConfigurationList: any;
  public selectedLaptopConfigurations: LaptopConfiguration[];
  public total = 0;
  public columnsToDisplay: string[] = ["name", "value", "price", "select"];
  horizontalPosition: MatSnackBarHorizontalPosition = "center";
  verticalPosition: MatSnackBarVerticalPosition = "top";
  public loading = false;
  private unsubscribe$ = new Subject<boolean>();
  @ViewChild(MatPaginator, { static: false }) set matPaginator(
    paginator: MatPaginator
  ) {
    if (this.laptopConfigurationList) {
      this.laptopConfigurationList.paginator = paginator;
    }
  }

  constructor(
    private laptopService: LaptopService,
    private dialogRef: MatDialogRef<LaptopConfigurationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public laptop: Laptop,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.selectedLaptopConfigurations = [];
    this.total = this.laptop.price;
    this.getLaptopConfigurationList();
  }

  getLaptopConfigurationList(): void {
    this.loading = true;
    this.laptopService
      .getLaptopConfigurationList()
      .pipe(
        takeUntil(this.unsubscribe$),
        tap((data: LaptopConfiguration[]) => {
          this.laptopConfigurationList = new MatTableDataSource<
            LaptopConfiguration
          >(data);
        }),
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe();
  }

  selectConfiguration(laptopConfiguration: LaptopConfiguration): void {
    this.selectedLaptopConfigurations.push(laptopConfiguration);
    this.calculateTotal();
  }

  configurationExist(laptopConfiguration: LaptopConfiguration): boolean {
    var exist = true;
    if (!this.selectedLaptopConfigurations) {
      return exist;
    }

    this.selectedLaptopConfigurations.map((c) => {
      if (c.name === laptopConfiguration.name) {
        exist = false;
      }
    });

    return exist;
  }

  deselectConfiguration(laptopConfiguration: LaptopConfiguration): void {
    const index = this.selectedLaptopConfigurations.indexOf(
      laptopConfiguration
    );
    this.selectedLaptopConfigurations.splice(index, 1);
    this.calculateTotal();
  }

  calculateTotal(): void {
    this.total = this.laptop.price;
    this.selectedLaptopConfigurations.map((c) => {
      this.total += c.price;
    });
  }

  addToBasket(): void {
    this.laptopService
      .addToBasket({
        laptopId: this.laptop.id,
        laptopConfigurationIdList: this.selectedLaptopConfigurations.map(
          (c) => c.id
        ),
      })
      .pipe(
        takeUntil(this.unsubscribe$),
        tap((data: any) => {
          this.dialogRef.close();
          this.snackBar.open(
            `${this.laptop.name} laptop added to basket! Total:£ ${data.totalPrice}`,
            "",
            {
              duration: 2000,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            }
          );
        }),
        catchError((err, caught) => {
          console.log(err);
          if (
            err.error.indexOf("Laptop and configuration already added!") !== -1
          ) {
            this.snackBar.open(
              `Error: Laptop and configuration already added!`,
              "",
              {
                duration: 4000,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              }
            );
          }

          return of();
        }),
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe();
  }
}
