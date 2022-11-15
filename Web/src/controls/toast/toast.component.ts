import { Component, OnInit } from "@angular/core";
import { ToastService } from "src/services";

@Component({
  selector: "zhd-toast",
  templateUrl: "./toast.component.html",
  styleUrls: ["./toast.component.scss"],
})
export class ToastComponent implements OnInit {
  toastList: string[] = [];

  constructor(private toastService: ToastService) {}

  ngOnInit(): void {
    this.toastService.toastEvent.subscribe(toast => this.toastList.push(toast.message));
  }
}
