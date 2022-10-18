import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "camelCase" })
export class CamelCasePipe implements PipeTransform {
  transform(value: string, ...args: unknown[]): unknown {
    return value.replace(/\s+/g, "").replace(/^\w/, c => c.toLowerCase());
  }
}
