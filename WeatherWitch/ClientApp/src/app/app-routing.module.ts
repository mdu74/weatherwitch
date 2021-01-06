import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { WeatherComponent } from "./weather/weather.component";
import { AuthGuard } from "./Auth/auth.guard";

const routes: Routes = [
  {
    path: "login",
    component: LoginComponent
  },
  {
    path: "weather",
    component: WeatherComponent,
    canActivate: [AuthGuard]
  },
  { path: "", redirectTo: "/weather", pathMatch: "full" }  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }