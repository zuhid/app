import { Component, Input, forwardRef } from "@angular/core";
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from "@angular/forms";
import { BaseControlComponent } from "../baseControl";

@Component({
  selector: "[zhd=checkbox]",
  templateUrl: "./checkbox.component.html",
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => CheckboxComponent), multi: true }],
})
export class CheckboxComponent extends BaseControlComponent implements ControlValueAccessor {
  @Input() field!: string; // the field of the model bound to this control
  @Input() label!: string; // label for the field
  @Input() forTable: boolean = false; // render control to be displayed inside table

  // Standard implementation begin
  private _checked = "";
  get checked(): string {
    return this._checked;
  }
  set checked(v: string) {
    if (this._checked !== v) {
      this._checked = v;
      this.onChangeCallback(v);
    }
  }

  writeValue(v: string) {
    if (this._checked !== v) {
      this._checked = v;
    }
  }
  // Standard implementation end
}
