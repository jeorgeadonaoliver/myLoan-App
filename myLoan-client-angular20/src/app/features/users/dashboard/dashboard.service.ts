import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
import { ApiResponse } from "../../../shared/domain/apiResponse.type";
import { DashboardResponseDto } from "./dashboard-field.model";
import { environment } from "../../../environment/environment";

@Injectable({providedIn:'root'})
export class DashboardService{
    constructor(private httpClient: HttpClient){}

    send(email: string) : Observable<ApiResponse<DashboardResponseDto>>{
        const encodedEmail = encodeURIComponent(email);
        const apiUrl = `${environment.usersApiUrl}/Users/usersbyemail?email=${encodedEmail}`

        return this.httpClient.get<ApiResponse<DashboardResponseDto>>(apiUrl).pipe(
            tap((res)=> {
                console.log('service', res);
            }),
            catchError((err: HttpErrorResponse) => {
                console.error('Error on Dashboardservice: ', err.message);
                return throwError(() => err)
            })
        );
    }
}