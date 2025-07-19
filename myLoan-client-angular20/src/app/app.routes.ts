import { Routes } from '@angular/router';
import { AuthLayout } from './layout/auth-layout/auth-layout';
import { MainLayout } from './layout/main-layout/main-layout';

export const routes: Routes = [
    {
        path: 'auth',
        component: AuthLayout,
        children: [
            { path: '', loadChildren: () => import('./features/authentication/register/register.routes').then(m => m.routes)},
            { path: '', loadChildren: () => import('./features/authentication/login/login.route').then(m=> m.route)}
        ]    
    },
    {
        path: 'users',
        component: MainLayout,
        children: [
            { path: 'dashboard', loadChildren: () => import('./features/users/dashboard/dashboard.route').then(m => m.route)},
            { path: 'profile', loadChildren: () => import('./features/users/profile/profile.route').then(m => m.route)}
        ]   
    }    
];
