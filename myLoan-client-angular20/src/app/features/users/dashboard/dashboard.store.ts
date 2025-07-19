import { computed, Injectable, Signal, signal } from "@angular/core";
import { DashboardService } from "./dashboard.service";
import { toSignal } from "@angular/core/rxjs-interop";
import { BehaviorSubject, catchError, defer, filter, map, Observable, of, switchMap, tap, throwError } from "rxjs";
import { ApiResponse } from "../../../shared/domain/apiResponse.type";
import { DashboardResponseDto } from "./dashboard-field.model";
import { UserInfo, UserStateService } from "../../../core/state/user-state-service";

@Injectable({providedIn: 'root'})
export class DashboardStore{
    constructor(private httpService: DashboardService, private userstateservice : UserStateService){}
    private loading = signal(false);
    private userinfo$ = signal<UserInfo | null>(null);
    
    loadUser(email: string){
         this.httpService.send(email).pipe(
            map((response: ApiResponse<DashboardResponseDto>) => {

                if(!response.data){
                    throw new Error('API Response data is null!');
                }

                const data = response.data;
                console.log('data store:', data);

                if(Array.isArray(data) && data.length > 0){
                    const _userinfo: UserInfo = {
                        lastName: data[0].lastName,
                        firstName: data[0].firstName, 
                        email: data[0].email,
                        userId:data[0].userId,
                        createdAt: data[0].createdAt,
                        updatedAt: data[0].updatedAt,
                        status: data[0].status
                    };
                    console.log('map: ',_userinfo);
                    this.userinfo$.set(_userinfo);
                    return _userinfo;
                }
                throw new Error('API returned an empty array');
            }),
            tap((_userInfo : UserInfo) => {
                console.log('tap: ',_userInfo)
                this.userstateservice.setUserInfo(_userInfo)
            }),
            catchError((err)=> {
                console.error('Error on mappong userinfo:', err);
                return throwError(err.message);
            })
        ).subscribe();
    }
}