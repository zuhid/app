import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { CacheService, IdentityService } from "src/services";

@Injectable({ providedIn: "root" })
export class AuthGuard implements CanActivate {
  constructor(public router: Router, private identityService: IdentityService, private cacheService: CacheService) {}

  async canActivate(): Promise<boolean> {
    let identityToken = this.cacheService.identityToken;
    if (identityToken != null && !identityToken.requireTfa && identityToken.token != null && identityToken.token.length > 0) {
      return true;
    }
    return this.router.navigate(["login"]);
  }
}
