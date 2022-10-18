import { Component, Input, Output, EventEmitter, ViewChildren, QueryList } from "@angular/core";
// import { InputComponent } from "../input/input.component";

@Component({
  selector: "[zhd=card]",
  templateUrl: "./card.component.html",
  styleUrls: ["./card.component.scss"],
})
export class CardComponent {
  @Input() header = "";
  @Input() reset = false;
  @Input() help = false;
  @Input() history = false;
  @Input() submit!: string;
  @Output() resetClick = new EventEmitter();
  @Output() helpClick = new EventEmitter();
  @Output() historyClick = new EventEmitter();
  @Output() submitClick = new EventEmitter();
  // @ViewChildren(InputComponent) inputComponents: QueryList<InputComponent>;
  cardId = () => "card-" + this.header.replace(" ", "");
}
