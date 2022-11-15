import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { Toast } from "src/models/controls/toast";

@Injectable({ providedIn: "root" })
export class ToastService {
  toastEvent: Observable<Toast>;
  private _toastSubject = new Subject<Toast>();

  constructor() {
    this.toastEvent = this._toastSubject.asObservable();
  }

  error(message: string) {
    this._toastSubject.next({
      type: "error",
      message: message,
    });
  }
}
