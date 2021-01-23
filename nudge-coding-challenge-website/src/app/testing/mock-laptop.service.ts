import { Injectable } from "@angular/core";
import { of, Observable } from "rxjs";
import { Laptop } from "../models/laptop";
import { LaptopConfiguration } from "../models/laptop-configuration";
import { BasketItem } from "../models/basket-item";

@Injectable({
  providedIn: "root",
})
export class MockLaptopService {
  public getLaptopList(): Observable<Laptop[]> {
    return of([
      { id: 1, name: "Hp", price: 123.34 },
      { id: 2, name: "Dell", price: 234.67 },
      { id: 3, name: "Macbook Pro", price: 345.78 },
      { id: 4, name: "Lenovo", price: 456.67 },
    ]);
  }

  public getLaptopConfigurationList(): Observable<LaptopConfiguration[]> {
    return of([
      { id: 1, name: "Ram", value: "8GB", price: 123.34 },
      { id: 2, name: "Ram", value: "16GB", price: 223.34 },
      { id: 3, name: "HDD", value: "500GB", price: 423.56 },
      { id: 4, name: "HDD", value: "500GB", price: 723.56 },
      { id: 5, name: "Color", value: "Red", price: 567.56 },
      { id: 6, name: "Color", value: "Blue", price: 345.56 },
    ]);
  }

  public addToBasket(basketItem: BasketItem): Observable<any> {
    return of({
      basketItems: [
        {
          laptop: { id: 2, name: "Dell", price: 234.67 },
          LaptopConfigurations: [
            { id: 1, name: "Ram", value: "8GB", price: 123.34 },
            { id: 4, name: "HDD", value: "500GB", price: 723.56 },
            { id: 6, name: "Color", value: "Blue", price: 345.56 },
          ],
        },
      ],
      totalPrice: 1427.13,
    });
  }
}
