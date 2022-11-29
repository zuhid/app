import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({ providedIn: "root" })
export class CacheService {
  private cacheList = new Map();

  // get the value from session storage if page is refressed or navigation done manually
  constructor() {
    const value = localStorage.getItem("cacheService");
    if (value != null) {
      this.cacheList = new Map(Object.entries(JSON.parse(value)));
    }
  }

  get(key: string): any {
    let orginalCacheTime = this.cacheList.get(key);
    if (orginalCacheTime) {
      // if the item has been cached for longer than the cacheDuration, then do not return the item
      if (Date.now() - orginalCacheTime < environment.cacheDuration) {
        let value = localStorage.getItem(key);
        return value != null ? JSON.parse(value) : null;
      } else {
        // if the key has lasted past the duration, then remove it from the cacheList and storage
        this.cacheList.delete(key);
        localStorage.removeItem(key);
      }
    }
    return null;
  }
  set(key: string, value: any) {
    localStorage.setItem(key, JSON.stringify(value));
    // update the cache key
    this.cacheList.set(key, Date.now());
    localStorage.setItem("cacheService", JSON.stringify(Object.fromEntries(this.cacheList)));
  }
}

// @Injectable({ providedIn: "root" })
// export class CacheService {
//   private db: any;
//   private store = "storage";

//   constructor() {
//     this.init().then();
//   }

//   async init(): Promise<any> {
//     return new Promise((resolve) => {
//       let self = this;
//       const request = window.indexedDB.open("Web", 1);

//       request.onsuccess = (event: any) => {
//         self.db = event.target.result;
//         resolve(null);
//       };

//       request.onerror = (event: any) => {};

//       request.onupgradeneeded = (event: any) => {
//         self.db = event.target.result;
//         self.db.createObjectStore(self.store, { keyPath: "key" });
//         resolve(null);
//       };
//     });
//   }

//   async get(key: any): Promise<any> {
//     return new Promise(async (resolve) => {
//       if (!this.db) {
//         await this.init();
//       }
//       if (this.db) {
//         let result = this.db.transaction(this.store, "readwrite").objectStore(this.store).get(key);
//         result.onsuccess = (event: any) => resolve(event.target.result ? event.target.result.value : null);
//       }
//       return Promise.resolve(null);
//     });
//   }

//   set(data: object) {
//     this.db.transaction(this.store, "readwrite").objectStore(this.store).add(data);
//   }
// }
