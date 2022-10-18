import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";

import { IndexComponent } from "./index/index.component";

const routes: Routes = [{ path: "", component: IndexComponent }];

@NgModule({
  declarations: [IndexComponent],
  imports: [FormsModule, CommonModule, RouterModule.forChild(routes)],
})
export class DashboardModule {}
