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
  public isAuthenticated(): boolean {
    return this.token != null && this.token.length > 0;
  }
  public get fullName() {
    return `${this.payload.FirstName} ${this.payload.LastName}`.trim();
  }
  private get header() {
    return JSON.parse(atob(this.token.split(".")[0])) ?? {};
  }
  private get payload() {
    return JSON.parse(atob(this.token.split(".")[1])) ?? {};
  }
  private get signature() {
    return JSON.parse(atob(this.token.split(".")[2])) ?? {};
  }
}
