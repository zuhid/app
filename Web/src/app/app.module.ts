import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { PreloadAllModules, RouterModule, Routes } from "@angular/router";

import { AppComponent } from "./app.component";
import { AuthGuard } from "src/guards";
import { ControlsModule } from "src/controls/controls.module";
import { IndexComponent } from "./index/index.component";
import { LoginComponent } from "./login/login.component";

const routes: Routes = [
  { path: "login", component: LoginComponent },
  {
    path: "",
    component: IndexComponent,
    canActivate: [AuthGuard],
    children: [
      { path: "chart", loadChildren: () => import("./chart/chart.module").then(m => m.ChartModule) },
      { path: "dashboard", loadChildren: () => import("./dashboard/dashboard.module").then(m => m.DashboardModule) },
      { path: "identity", loadChildren: () => import("./identity/identity.module").then(m => m.IdentityModule) },
      { path: "page", loadChildren: () => import("./page/page.module").then(m => m.PageModule) },
      { path: "report", loadChildren: () => import("./report/report.module").then(m => m.ReportModule) },
      { path: "", redirectTo: "dashboard", pathMatch: "full" },
    ],
  },
  { path: "**", redirectTo: "" },
];

@NgModule({
  declarations: [AppComponent, IndexComponent, LoginComponent],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    ControlsModule,
    RouterModule.forRoot(routes, {
      preloadingStrategy: PreloadAllModules,
      // enableTracing: true,
    }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
