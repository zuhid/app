import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { ControlsModule } from "src/controls/controls.module";
import { Page1Component } from "./page1/page1.component";
import { Page2Component } from "./page2/page2.component";

const routes: Routes = [
  { path: "page1", component: Page1Component },
  { path: "page2", component: Page2Component },
  { path: "", redirectTo: "page1", pathMatch: "prefix" },
];

@NgModule({
  declarations: [Page1Component, Page2Component],
  imports: [CommonModule, FormsModule, ControlsModule, RouterModule.forChild(routes)],
})
export class PageModule {}
