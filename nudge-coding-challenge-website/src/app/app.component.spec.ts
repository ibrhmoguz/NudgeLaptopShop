import { TestBed, async } from "@angular/core/testing";
import { RouterTestingModule } from "@angular/router/testing";
import { AppComponent } from "./app.component";
import { NudgeCodingChallengeApiService } from "./services";
import { MockNudgeCodingChallengeApiService } from "./testing/mock-nudge-coding-challenge-api.service";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";

describe("AppComponent", () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, FontAwesomeModule],
      declarations: [AppComponent],
      providers: [
        {
          provide: NudgeCodingChallengeApiService,
          useClass: MockNudgeCodingChallengeApiService,
        },
      ],
    }).compileComponents();
  }));

  it("should create the app", () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Nudge Coding Challenge'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual("Nudge Coding Challenge!");
  });

  it("should render title in a h1 tag", () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector("h1").textContent).toContain(
      "Nudge Coding Challenge!"
    );
  });
});
