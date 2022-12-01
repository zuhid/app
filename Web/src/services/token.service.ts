import { Injectable } from "@angular/core";

@Injectable({ providedIn: "root" })
export class TokenService {
  get token(): any {
    let value = sessionStorage.getItem("identityToken");
    return value ? JSON.parse(value) : {};
  }
  set token(value: any) {
    sessionStorage.setItem("identityToken", JSON.stringify(value));
  }

  public isAuthenticated = () => this.token != null && this.token.length > 0;
  public fullName = () => `${this.payload().FirstName} ${this.payload().LastName}`.trim();

  private header = () => JSON.parse(atob(this.token.split(".")[0])) ?? {};
  private payload = () => JSON.parse(atob(this.token.split(".")[1])) ?? {};
  private signature = () => JSON.parse(atob(this.token.split(".")[2])) ?? {};
}
