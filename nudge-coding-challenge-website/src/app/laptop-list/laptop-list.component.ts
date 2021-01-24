import { Component, OnInit, ViewChild } from "@angular/core";
import { Subject, of } from "rxjs";
import { takeUntil, tap, catchError, finalize } from "rxjs/operators";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { LaptopService } from "../services/laptop.service";
import { Laptop } from "../models/laptop";
import { MatDialog } from "@angular/material/dialog";
import { LaptopConfigurationDialogComponent } from "./../laptop-configuration-dialog/laptop-configuration-dialog.component";

@Component({
  selector: "laptop-list",
  templateUrl: "./laptop-list.component.html",
  styleUrls: ["./laptop-list.component.scss"],
})
export class LaptopListComponent implements OnInit {
  public laptopList: any = null;
  public columnsToDisplay: string[] = ["name", "price", "select"];
  public loading = false;
  private unsubscribe$ = new Subject<boolean>();
  @ViewChild(MatPaginator, { static: false }) set matPaginator(
    paginator: MatPaginator
  ) {
    if (this.laptopList) {
      this.laptopList.paginator = paginator;
    }
  }

  constructor(
    private laptopService: LaptopService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getLaptopList();
  }

  getLaptopList() {
    this.loading = true;
    this.laptopService
      .getLaptopList()
      .pipe(
        takeUntil(this.unsubscribe$),
        tap((data: Laptop[]) => {
          this.laptopList = new MatTableDataSource<Laptop>(data);
        }),
        catchError((error: any) => {
          console.error(error);
          return of();
        }),
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe();
  }

  openLaptopConfigurationModal(laptop: Laptop) {
    this.dialog.open(LaptopConfigurationDialogComponent, {
      data: laptop,
    });
  }
}
