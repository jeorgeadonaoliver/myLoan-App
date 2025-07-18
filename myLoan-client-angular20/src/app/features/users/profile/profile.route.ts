import { Routes } from "@angular/router";
import { ProfilePage } from "./profile-page.component";

export const route : Routes = [
    {
        path: '',
        component: ProfilePage,
        pathMatch: 'prefix'

    }
]