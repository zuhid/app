import { Component } from "@angular/core";
import { TokenService } from "src/services";

@Component({ templateUrl: "./index.component.html" })
export class IndexComponent {
  constructor(public tokenService: TokenService) {}
}
