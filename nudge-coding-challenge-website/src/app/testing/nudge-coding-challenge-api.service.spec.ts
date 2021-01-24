import { async, TestBed, getTestBed } from "@angular/core/testing";
import { NudgeCodingChallengeApiService } from "../services";
import {
  HttpClientTestingModule,
  HttpTestingController,
} from "@angular/common/http/testing";
import { config } from "../models/config";
import { error } from "@angular/compiler/src/util";

describe("NudgeCodingChallengeApiService", () => {
  let injector: TestBed;
  let service: NudgeCodingChallengeApiService;
  let httpMock: HttpTestingController;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [NudgeCodingChallengeApiService],
    });
    injector = getTestBed();
    service = injector.get(NudgeCodingChallengeApiService);
    httpMock = injector.get(HttpTestingController);
  }));

  afterEach(() => {
    httpMock.verify();
  });

  it("should create service", () => {
    expect(service).toBeTruthy();
  });

  it("should get health", () => {
    service.getHealth().subscribe((result) => {
      expect(result).toEqual("Healthy");
    });

    const req = httpMock.expectOne(`${config.API_URL}/health`);
    expect(req.request.method).toBe("GET");
  });
});
