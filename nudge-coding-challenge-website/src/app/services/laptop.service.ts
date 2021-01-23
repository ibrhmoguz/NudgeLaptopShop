import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Laptop } from "./../models/laptop";
import { LaptopConfiguration } from "../models/laptop-configuration";
import { BasketItem } from "../models/basket-item";

@Injectable({
  providedIn: "root",
})
export class LaptopService {
  constructor(private httpClient: HttpClient) {}

  public getLaptopList(): Observable<Laptop[]> {
    return this.httpClient
      .get("https://localhost:5001/LaptopShop/laptop/list")
      .pipe(map((data: any) => data));
  }

  public getLaptopConfigurationList(): Observable<LaptopConfiguration[]> {
    return this.httpClient
      .get("https://localhost:5001/LaptopShop/configuration/list")
      .pipe(map((data: any) => data));
  }

  public addToBasket(basketItem: BasketItem): Observable<any> {
    console.log(basketItem);

    return this.httpClient
      .post("https://localhost:5001/LaptopShop/basket/add", basketItem)
      .pipe(map((data: any) => data));
  }
}
