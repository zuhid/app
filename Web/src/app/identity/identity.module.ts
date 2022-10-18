import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { ControlsModule } from "src/controls/controls.module";
import { UserComponent } from "./user/user.component";

const routes: Routes = [
  { path: "user", component: UserComponent },
  { path: "", redirectTo: "user", pathMatch: "prefix" },
];

@NgModule({
  imports: [CommonModule, FormsModule, ControlsModule, ControlsModule, RouterModule.forChild(routes)],
  declarations: [UserComponent],
})
export class IdentityModule {}
