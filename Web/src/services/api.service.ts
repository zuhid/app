import { Injectable } from "@angular/core";
import { CacheService } from "./cache.service";
import { ToastService } from "./toast.service";
import { TokenService } from "./token.service";

@Injectable({ providedIn: "root" })
export class ApiService {
  private promiseList = new Map<string, Promise<any>>();

  constructor(private tokenService: TokenService, public toastService: ToastService, private cacheService: CacheService) {}

  async get(url: string, errorMessage?: string, cache = false): Promise<any> {
    // if there is no cacheKey, then return the data from api
    if (!cache) {
      return await this.fetchApi("get", url, null, errorMessage);
    }
    let promise = null;
    // return the data from storage if it exists
    const modelCache = await this.cacheService.get(url);
    if (modelCache != null) {
      return modelCache;
    }
    promise = this.promiseList.get(url);

    // if promise does not exist then create it
    if (promise == null) {
      // do NOT put 'await' clause here, which is done in a few lines down
      promise = this.fetchApi("get", url, null, errorMessage);
      // save the promise to avoid parallel get calls
      this.promiseList.set(url, promise);
    }
    // wait for the promise to finish
    try {
      var model = await promise;
      // save the data in storage
      if (model != null) {
        this.cacheService.set(url, model);
      }
      return model;
    } finally {
      // remove the item from promiseList since it is already saved in storage, or exception is thrown
      this.promiseList.delete(url);
    }
  }

  async post(url: string, model: any, errorMessage?: string): Promise<any> {
    return await this.fetchApi("post", url, model, errorMessage);
  }

  async put(url: string, model: any, errorMessage?: string): Promise<any> {
    return await this.fetchApi("put", url, model, errorMessage);
  }

  async delete(url: string, errorMessage?: string): Promise<any> {
    return await this.fetchApi("delete", url, null, errorMessage);
  }

  async getAsset(url: string): Promise<any> {
    var response = await fetch(url, {
      cache: "force-cache",
    });
    return await response.json();
  }

  private async fetchApi(method: string, url: string, model: any, errorMessage?: string): Promise<any> {
    let identityToken = await this.tokenService.token;
    return fetch(url, {
      method: method,
      headers: {
        "Content-Type": "application/json",
        Authorization: identityToken ? `Bearer ${identityToken}` : "",
      },
      body: model ? JSON.stringify(model) : null,
    }).then((response) => {
      if (response.ok) {
        return response.json().then((m) => m);
      } else {
        this.toastService.error(errorMessage ? errorMessage : `Failed to ${method}  ${url}`);
        return null;
      }
    });
  }
}
