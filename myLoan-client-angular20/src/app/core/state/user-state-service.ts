import { computed, Injectable, signal } from "@angular/core";

export interface UserInfo{
    email: string;
    lastName: string;
    firstName: string;
    userId: string;
    createdAt: string;
    updatedAt: string;
    status: string;
}

@Injectable({providedIn: 'root'})
export class UserStateService{
    private _user = signal<UserInfo| null>(null);

    readonly email$ = computed(() => this._user()?.email);
    readonly lastname$ = computed(() => this._user()?.lastName);
    readonly firstname$ = computed(() => this._user()?.firstName);
    readonly userid$ = computed(() => this._user()?.userId);
    readonly createdAt$ = computed(() => this._user()?.createdAt);
    readonly updateAt$ = computed(() => this._user()?.updatedAt);
    readonly status$ = computed(() => this._user()?.status);
    readonly user$ = this._user.asReadonly();

    setUserInfo(user: UserInfo){
        this._user.set(user);
    }

    clearUserInfo(){
        this._user.set(null);
    }
}