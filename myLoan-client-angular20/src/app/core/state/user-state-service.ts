import { computed, Injectable, signal } from "@angular/core";

export interface UserInfo{

    userId: number
    firstName: string;
    lastName: string;
    email: string;  
    phone: string;
    dateOfBirth: string;
    addressLine1: string;
    addressLine2: string;
    city: string;
    stateProvince: string;
    postalCode: string;
    country: string;
    createdAt: string;
    updatedAt: string;
    status: number;
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
    readonly phone = computed(() => this._user()?.phone);
    readonly dateOfBirth = computed(() => this._user()?.dateOfBirth);
    readonly addressLine1 = computed(() => this._user()?.addressLine1);
    readonly addressLine2 = computed(() => this._user()?.addressLine2);
    readonly city = computed(() => this._user()?.city);
    readonly stateProvince = computed(() => this._user()?.stateProvince);
    readonly postalCode = computed(() => this._user()?.postalCode);
    readonly country = computed(() => this._user()?.country);

    readonly user$ = this._user.asReadonly();

    setUserInfo(user: UserInfo){
        this._user.set(user);
    }

    clearUserInfo(){
        this._user.set(null);
    }
}