import { Component, Input, forwardRef } from "@angular/core";
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from "@angular/forms";
import { BaseControlComponent } from "../baseControl";

@Component({
  selector: "[zhd=select]",
  templateUrl: "./select.component.html",
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => SelectComponent), multi: true }],
})
export class SelectComponent extends BaseControlComponent implements ControlValueAccessor {
  @Input() field!: string; // the field of the model bound to this control
  @Input() label!: string; // label for the field
  @Input() forTable: boolean = false; // render control to be displayed inside table

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

  selected?: string;
  options: string[] = ["dashboard", "identity/user", "chart", "page/page1", "page/page2"];

  public onBlur(e: any) {
    let matchedResult = this.options.filter(element => element.includes(this.text));
    this.text = matchedResult.length == 1 ? matchedResult[0] : "";
  }
}
