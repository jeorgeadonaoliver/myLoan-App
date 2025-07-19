import { Routes } from "@angular/router";
import { DashboardPage } from "./dashboard.page.component";

export const route : Routes = [
    {
        path: ':email',
        component: DashboardPage,
        pathMatch: 'prefix'
    }
]