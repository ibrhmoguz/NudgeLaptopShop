import { async, TestBed, getTestBed } from "@angular/core/testing";
import { HttpClientModule } from "@angular/common/http";
import { LaptopService } from "../services";
import {
  HttpClientTestingModule,
  HttpTestingController,
} from "@angular/common/http/testing";
import { config } from "../models/config";

describe("LaptopService", () => {
  let injector: TestBed;
  let service: LaptopService;
  let httpMock: HttpTestingController;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [LaptopService],
    });
    injector = getTestBed();
    service = injector.get(LaptopService);
    httpMock = injector.get(HttpTestingController);
  }));

  afterEach(() => {
    httpMock.verify();
  });

  it("should create service", () => {
    expect(service).toBeTruthy();
  });

  it("should get laptop list", () => {
    const laptops = [
      { id: 1, name: "Hp", price: 123.34 },
      { id: 2, name: "Dell", price: 234.67 },
      { id: 3, name: "Macbook Pro", price: 345.78 },
      { id: 4, name: "Lenovo", price: 456.67 },
    ];
    service.getLaptopList().subscribe((result) => {
      expect(result).toEqual(laptops);
    });

    const req = httpMock.expectOne(`${config.API_URL}/LaptopShop/laptop/list`);
    expect(req.request.method).toBe("GET");
    req.flush(laptops);
  });

  it("should get laptop configurations", () => {
    const laptopConfigurations = [
      { id: 1, name: "Ram", value: "8GB", price: 123.34 },
      { id: 2, name: "Ram", value: "16GB", price: 223.34 },
      { id: 3, name: "HDD", value: "500GB", price: 423.56 },
      { id: 4, name: "HDD", value: "500GB", price: 723.56 },
      { id: 5, name: "Color", value: "Red", price: 567.56 },
      { id: 6, name: "Color", value: "Blue", price: 345.56 },
    ];
    service.getLaptopConfigurationList().subscribe((result) => {
      expect(result).toEqual(laptopConfigurations);
    });

    const req = httpMock.expectOne(
      `${config.API_URL}/LaptopShop/configuration/list`
    );
    expect(req.request.method).toBe("GET");
    req.flush(laptopConfigurations);
  });
});
