import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { ControlsModule } from "src/controls/controls.module";
import { IndexComponent } from "./index/index.component";

const routes: Routes = [{ path: "", component: IndexComponent }];

@NgModule({
  declarations: [IndexComponent],
  imports: [CommonModule, ControlsModule, RouterModule.forChild(routes)],
})
export class ReportModule {}
