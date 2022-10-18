import { Component } from "@angular/core";
import { ArrayUtil } from "src/util";

@Component({ templateUrl: "./index.component.html", styleUrls: ["./index.component.scss"] })
export class IndexComponent {
  public zindex = -1;
  public tileOrder = ["a", "b", "c", "d", "e", "f", "g", "h", "i"];

  dragstart(e: any, previousKey: string) {
    e.dataTransfer.setData("previousKey", previousKey);
    setTimeout(() => (this.zindex = 1), 100);
  }

  dragend() {
    this.zindex = -1;
  }

  drop(e: any, currentIndex: number) {
    let previousIndex = this.getOrder(e.dataTransfer.getData("previousKey"));
    if (previousIndex > currentIndex) {
      ArrayUtil.move(this.tileOrder, previousIndex, currentIndex);
    } else {
      ArrayUtil.move(this.tileOrder, previousIndex, currentIndex - 1);
    }
  }

  getOrder(key: string) {
    return this.tileOrder.indexOf(key);
  }
}
