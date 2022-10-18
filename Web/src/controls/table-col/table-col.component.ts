import { Directive, Input } from "@angular/core";

@Directive({ selector: "zhd-table-col" })
export class TableColComponent {
  @Input() label!: string;
  @Input() type!: string;
  @Input() value!: string;
}
