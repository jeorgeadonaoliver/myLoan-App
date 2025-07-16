import { Routes } from '@angular/router';
import { AuthLayout } from './layout/auth-layout/auth-layout';

export const routes: Routes = [
    {
        path: 'auth',
        component: AuthLayout,
        children: [
            { path: '', loadChildren: () => import('./features/authentication/register/register.routes').then(m => m.routes)},
            { path: '', loadChildren: () => import('./features/authentication/login/login.route').then(m=> m.route)}
        ]    
    }    
];
