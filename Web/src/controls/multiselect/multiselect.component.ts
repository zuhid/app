import { Component, Input, forwardRef } from "@angular/core";
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from "@angular/forms";
import { ApiService } from "src/services";
import { BaseControlComponent } from "../baseControl";

@Component({
  selector: "[zhd=multiselect]",
  templateUrl: "./multiselect.component.html",
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => MultiselectComponent), multi: true }],
})
export class MultiselectComponent extends BaseControlComponent implements ControlValueAccessor {
  @Input() field!: string; // the field of the model bound to this control
  @Input() label!: string; // label for the field
  @Input() forTable: boolean = false; // render control to be displayed inside table
  @Input() listUrl!: string;
  options: string[] = [];

  get filteredOptions() {
    let selectedItems = this.text?.split(",") ?? [];
    return this.options.filter(n => !selectedItems.includes(n));
  }

  constructor(private apiService: ApiService) {
    super();
  }

  ngOnInit(): void {
    this.apiService.get(this.listUrl, "", true).then(res => {
      this.options = res.map((n: any) => n.text ?? n);
      // this.filteredOptions = this.options;
    });
  }

  // Standard implementation begin
  private _text = "";
  get text(): string {
    return this._text;
  }
  set text(v: string | null) {
    if (this._text !== v) {
      this._text = v || "";
      this.onChangeCallback(v);
    }
  }

  writeValue(v: string) {
    if (this._text !== v) {
      this._text = v;
    }
  }
  // Standard implementation end
}
