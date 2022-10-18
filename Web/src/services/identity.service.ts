import { Injectable } from "@angular/core";
import { Login, LoginResponse } from "src/models";
import { ApiService, CacheService } from "src/services";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class IdentityService {
  constructor(private apiService: ApiService, private cacheService: CacheService) {}

  async login(model: Login): Promise<LoginResponse> {
    let loginResponse: LoginResponse = await this.apiService.post(`${environment.identityApi}/login`, model);
    this.cacheService.identityToken = loginResponse;
    return loginResponse;
  }
}
