import { Component, signal, computed, effect } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { DashboardStore } from "./dashboard.store";
import { DashboardComponent } from "./dashboard.component";
import { UserStateService } from "../../../core/state/user-state-service";

@Component({
    selector: 'app-dashboard',
    imports: [DashboardComponent],
    providers:[],
    template: `
        <div>
            <app-dashboard-component [fullname]="fullname()" />
        </div>
    `
})

export class DashboardPage {
    public fullname = computed(() =>{
        console.log(`${this._userinfo.lastname$()}, ${this._userinfo.firstname$()}`);
        return `${this._userinfo.lastname$()}, ${this._userinfo.firstname$()}`
    });

    readonly email$ = signal('');

    constructor(private _userinfo: UserStateService, private store: DashboardStore, private route: ActivatedRoute){
        const email = this.route.snapshot.paramMap.get('email') ?? '';
        this.email$.set(email);

        effect(() =>{
            const _userinfo$ = this._userinfo.user$();
            const _email = this.email$();

            if(!this._userinfo.user$() && this._userinfo.user$()?.email !== _email)
                this.store.loadUser(this.email$());
        });
    }
}
