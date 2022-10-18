import { Component, ContentChildren, Input, QueryList } from "@angular/core";
import { TableColComponent } from "../table-col/table-col.component";

@Component({
  selector: "zhd-table",
  templateUrl: "./table.component.html",
})
export class TableComponent {
  @ContentChildren(TableColComponent) tableColComponent!: QueryList<TableColComponent>;
  @Input() datalist!: any[];
  add() {
    this.datalist.splice(0, 0, {});
  }
}
