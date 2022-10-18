import { Component, ElementRef, Input, OnInit } from "@angular/core";
import { ApiService } from "src/services";

@Component({ selector: "zhd-svg", template: "" })
export class SvgComponent implements OnInit {
  @Input() url: string = "";

  constructor(private apiService: ApiService, private el: ElementRef) {}

  async ngOnInit() {
    this.el.nativeElement.innerHTML = await this.apiService.getAsset(this.url);
  }
}
