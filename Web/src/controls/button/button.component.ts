import { Component, Input, Output, EventEmitter, ViewChildren, QueryList } from "@angular/core";

@Component({
  selector: "zhd-button",
  templateUrl: "./button.component.html",
})
export class ButtonComponent {
  @Input() id!: string; // the id of the button
  @Input() text!: string; // the text of teh button
  @Output() onclick = new EventEmitter();
}
