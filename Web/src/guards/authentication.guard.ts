import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { TokenService } from "src/services";

@Injectable({ providedIn: "root" })
export class AuthenticationGuard implements CanActivate {
  constructor(public router: Router, private tokenService: TokenService) {}

  async canActivate(): Promise<boolean> {
    if (await this.tokenService.isAuthenticated()) {
      return true;
    }
    return this.router.navigate(["login"]);
  }
}
