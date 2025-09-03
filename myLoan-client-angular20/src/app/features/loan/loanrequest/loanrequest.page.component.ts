import { Component } from "@angular/core";
import { LoanRequestComponent } from "./loanrequest.component";

@Component({
    selector: 'app-loanrequest',
    imports: [LoanRequestComponent],
    providers:[],
    template: `
    <div>
        <app-loanrequest-component></app-loanrequest-component>
    </div>
    `
})

export class LoanRequestPage {
    constructor(){}
}