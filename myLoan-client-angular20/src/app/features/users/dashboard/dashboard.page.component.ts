import { Component, OnInit, runInInjectionContext, Signal, inject, EnvironmentInjector, signal, computed, effect } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { DashboardStore } from "./dashboard.store";
import { DashboardComponent } from "./dashboard.component";
import { UserStateService } from "../../../core/state/user-state-service";


@Component({
    selector: 'app-dashboard',
    imports: [DashboardComponent],
    providers:[],
    template: `
    <app-dashboard-component [fullname]="fullname()" />
    `
})

export class DashboardPage {
    public fullname = computed(() =>{
        return `${this._userinfo.lastname$()}, ${this._userinfo.firstname$()}`
    });
    constructor(private _userinfo: UserStateService, private store: DashboardStore, private route: ActivatedRoute){
        const email = this.route.snapshot.paramMap.get('email') ?? '';
        console.log(email);

        effect(() =>{
            this.store.loadUser(email).subscribe();
        });
    }
}
