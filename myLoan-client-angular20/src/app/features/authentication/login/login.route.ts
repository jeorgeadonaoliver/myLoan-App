import { Routes } from "@angular/router";
import { LoginPage } from "./login-page.component";

export const route : Routes = [{
    path:'login',
    component: LoginPage,
    pathMatch: 'prefix'
}];