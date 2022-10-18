import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "gridList" })
export class GridListPipe implements PipeTransform {
  transform(value: number, ...args: unknown[]): unknown {
    return Math.floor(window.innerWidth / value);
  }
}
