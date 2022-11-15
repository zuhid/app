import { Component, Input, forwardRef, OnInit } from "@angular/core";
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from "@angular/forms";
import { ApiService } from "src/services";
import { BaseControlComponent } from "../baseControl";

@Component({
  selector: "[zhd=select]",
  templateUrl: "./select.component.html",
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => SelectComponent), multi: true }],
})
export class SelectComponent extends BaseControlComponent implements ControlValueAccessor, OnInit {
  @Input() field!: string; // the field of the model bound to this control
  @Input() label!: string; // label for the field
  @Input() forTable: boolean = false; // render control to be displayed inside table
  @Input() listUrl!: string;
  selected?: string;
  options: string[] = [];

  constructor(private apiService: ApiService) {
    super();
  }

  ngOnInit(): void {
    this.apiService.get(this.listUrl, "", true).then(res => {
      this.options = res.map((n: any) => n.text ?? n);
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
