import { Component } from "@angular/core";
import { NudgeCodingChallengeApiService } from "./services";
import { take } from "rxjs/operators";
import { faThumbsUp, faThumbsDown } from "@fortawesome/free-regular-svg-icons";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent {
  public faThumbsUp = faThumbsUp;
  public faThumbsDown = faThumbsDown;
  public title = "Nudge - Laptop Shop Code Challenge!";
  public nudgeCodingChallengeApiIsActive = false;
  public nudgeCodingChallengeApiActiveIcon = this.faThumbsDown;
  public nudgeCodingChallengeApiActiveIconColour = "red";

  constructor(
    private nudgeCodingChallengeApiService: NudgeCodingChallengeApiService
  ) {
    nudgeCodingChallengeApiService
      .getHealth()
      .pipe(take(1))
      .subscribe(
        (apiHealth) => {
          this.nudgeCodingChallengeApiIsActive = apiHealth === "Healthy";
          this.nudgeCodingChallengeApiActiveIcon = this
            .nudgeCodingChallengeApiIsActive
            ? this.faThumbsUp
            : this.faThumbsUp;
          this.nudgeCodingChallengeApiActiveIconColour = this
            .nudgeCodingChallengeApiIsActive
            ? "green"
            : "red";
        },
        (_) => {
          this.nudgeCodingChallengeApiIsActive = false;
          this.nudgeCodingChallengeApiActiveIcon = this.faThumbsDown;
          this.nudgeCodingChallengeApiActiveIconColour = "red";
        }
      );
  }
}
