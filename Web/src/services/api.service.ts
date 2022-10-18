import { Injectable } from "@angular/core";
import { CacheService } from "./cache.service";

@Injectable({ providedIn: "root" })
export class ApiService {
  constructor(private cacheService: CacheService) {}

  getHeader(): HeadersInit {
    let identityToken = this.cacheService.identityToken;
    return identityToken == null
      ? { "Content-Type": "application/json" }
      : {
          "Content-Type": "application/json",
          Authorization: `Bearer ${identityToken.token}`,
        };
  }

  async get(url: string, errorMessage?: string, cache = false): Promise<any> {
    var response = await fetch(url, {
      method: "get",
      headers: this.getHeader(),
    });
    return await response.json();
  }

  async post(url: string, model: any, errorMessage?: string, cache = false): Promise<any> {
    var response = await fetch(url, {
      method: "post",
      headers: this.getHeader(),
      body: JSON.stringify(model),
    });
    return await response.json();
  }

  async put(url: string, model: any, errorMessage?: string, cache = false): Promise<any> {
    var response = await fetch(url, {
      method: "put",
      headers: this.getHeader(),
      body: JSON.stringify(model),
    });
    return await response.json();
  }

  async delete(url: string, errorMessage?: string, cache = false): Promise<any> {
    var response = await fetch(url, {
      method: "delete",
      headers: this.getHeader(),
    });
    return await response.json();
  }

  async getAsset(url: string): Promise<any> {
    var response = await fetch(url, {
      cache: "force-cache",
    });
    return await response.json();
  }
}
