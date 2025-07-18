import { computed, Injectable, signal } from "@angular/core";
import { userInfo } from "os";

export interface UserInfo{
    email: string;
    lastName: string;
    firstName: string;
    userId: string;
}

@Injectable({providedIn: 'root'})
export class UserStateService{
    private _user = signal<UserInfo| null>(null);

    readonly email$ = computed(() => this._user()?.email);
    readonly lastname$ = computed(() => this._user()?.lastName);
    readonly firstname$ = computed(() => this._user()?.firstName);
    readonly userid$ = computed(() => this._user()?.userId);

    setUserInfo(user: UserInfo){
        this._user.set(user);
    }

    clearUserInfo(){
        this._user.set(null);
    }
}