import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { config } from "../models/config";

@Injectable({
  providedIn: "root",
})
export class NudgeCodingChallengeApiService {
  constructor(private httpClient: HttpClient) {}

  public getHealth(): Observable<string> {
    return this.httpClient.get(`${config.API_URL}/health`, {
      responseType: "text",
    });
  }
}
