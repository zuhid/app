import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class CacheService {
  private cacheList = new Map();

  // get the value from session storage if page is refressed or navigation done manually
  constructor() {
    const value = sessionStorage.getItem("cacheService");
    if (value != null) {
      this.cacheList = new Map(Object.entries(JSON.parse(value)));
    }
  }

  get identityToken(): any {
    return this.getItem("identityToken");
  }
  set identityToken(item: any) {
    this.setItem("identityToken", item);
  }

  getItem(key: string): any {
    let orginalCacheTime = this.cacheList.get(key);
    if (orginalCacheTime) {
      // if the item has been cached for longer than the cacheDuration, then do not return the item
      if (Date.now() - orginalCacheTime < environment.cacheDuration) {
        let value = sessionStorage.getItem(key);
        return value != null ? JSON.parse(value) : null;
      } else {
        // if the key has lasted past the duration, then remove it from the cacheList and storage
        this.cacheList.delete(key);
        sessionStorage.removeItem(key);
      }
    }
    return null;
  }
  setItem(key: string, value: any) {
    sessionStorage.setItem(key, JSON.stringify(value));
    // update the cache key
    this.cacheList.set(key, Date.now());
    sessionStorage.setItem("cacheService", JSON.stringify(Object.fromEntries(this.cacheList)));
  }
}
