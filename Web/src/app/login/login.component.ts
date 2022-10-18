import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Login } from "src/models";
import { IdentityService } from "src/services";

@Component({ templateUrl: "./login.component.html" })
export class LoginComponent {
  public model: Login = {
    email: "admin@company.com",
    password: "P@ssw0rd",
    rememberMe: true,
  };

  constructor(private identityService: IdentityService, private router: Router) {}

  async login() {
    this.identityService.login(this.model).then(loginResponse => this.router.navigate([loginResponse.landingPage]));
  }
}
