import { Routes } from "@angular/router";
import { LoginPage } from "./login-page.component";

export const route : Routes = [{
    path:'',
    component: LoginPage,
    pathMatch: 'prefix'
}];