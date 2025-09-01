import { Routes } from "@angular/router";
import { LoanRequestPage } from "./loanrequest.page.component";

export const route : Routes = [
    {  
        path: 'loanrequest',
        component: LoanRequestPage,
        pathMatch: 'prefix'
    }
]