import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { PreloadAllModules, RouterModule, Routes } from "@angular/router";

import { ControlsModule } from "src/controls/controls.module";
import { AuthenticationGuard, AuthorizationGuard } from "src/guards";
import { AppComponent } from "./app.component";
import { IndexComponent } from "./index/index.component";
import { LoginComponent } from "./login/login.component";

const routes: Routes = [
  { path: "login", component: LoginComponent },
  {
    path: "",
    component: IndexComponent,
    canActivate: [AuthenticationGuard],
    canActivateChild: [AuthorizationGuard],
    children: [
      { path: "dashboard", data: { role: "dashboard" }, loadChildren: () => import("./dashboard/dashboard.module").then((m) => m.DashboardModule) },
      { path: "identity", data: { role: "Administrator" }, loadChildren: () => import("./identity/identity.module").then((m) => m.IdentityModule) },
      { path: "page", data: { role: "page" }, loadChildren: () => import("./page/page.module").then((m) => m.PageModule) },
      { path: "chart", data: { role: "chart" }, loadChildren: () => import("./chart/chart.module").then((m) => m.ChartModule) },
      { path: "report", data: { role: "report" }, loadChildren: () => import("./report/report.module").then((m) => m.ReportModule) },
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
