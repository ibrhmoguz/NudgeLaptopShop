import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Laptop } from "./../models/laptop";
import { LaptopConfiguration } from "../models/laptop-configuration";
import { BasketItem } from "../models/basket-item";
import { config } from "../models/config";

@Injectable({
  providedIn: "root",
})
export class LaptopService {
  constructor(private httpClient: HttpClient) {}

  public getLaptopList(): Observable<Laptop[]> {
    return this.httpClient
      .get(`${config.API_URL}/LaptopShop/laptop/list`)
      .pipe(map((data: any) => data));
  }

  public getLaptopConfigurationList(): Observable<LaptopConfiguration[]> {
    return this.httpClient
      .get(`${config.API_URL}/LaptopShop/configuration/list`)
      .pipe(map((data: any) => data));
  }

  public addToBasket(basketItem: BasketItem): Observable<any> {
    return this.httpClient
      .post(`${config.API_URL}/LaptopShop/basket/add`, basketItem)
      .pipe(map((data: any) => data));
  }
}
