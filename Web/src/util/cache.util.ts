export class CacheUtil {
  static get(key: string) {
    let value = sessionStorage.getItem(key);
    return value ? JSON.parse(value) : null;
  }

  static set(key: string, value: any) {
    sessionStorage.setItem(key, JSON.stringify(value));
  }
}
