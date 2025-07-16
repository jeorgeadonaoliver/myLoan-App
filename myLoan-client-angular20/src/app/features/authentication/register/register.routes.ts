import { Routes } from '@angular/router';
import { RegisterPage } from './register-page.component';

export const routes: Routes = [
        {
            path:'register',
            component: RegisterPage,
            pathMatch:'prefix'
        }
];