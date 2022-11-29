import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Login, LoginResponse } from "src/models";
import { ApiService } from "src/services";
import { TokenService } from "./token.service";

@Injectable({ providedIn: "root" })
export class IdentityService {
  constructor(private apiService: ApiService, private tokenService: TokenService) {}

  async login(model: Login) {
    let loginResponse: LoginResponse = await this.apiService.post(`${environment.identityApi}/login`, model);
    this.tokenService.token = loginResponse.token;
  }
}
