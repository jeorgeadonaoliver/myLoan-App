import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { environment } from "../../../environment/environment";
import { LoginFormFields } from "./login-form-fields";
import { catchError, Observable, throwError } from "rxjs";
import { ApiResponse } from "../../../shared/domain/apiResponse.type";
import { Injectable } from "@angular/core";

@Injectable({providedIn:'root'})
export class LoginService{
    private readonly apiUrl = `${environment.authenticationApiUrl}/auth/login`;
    constructor(private httpClient: HttpClient){}

    send(data: LoginFormFields): Observable<ApiResponse<string>>{
        return this.httpClient.post<ApiResponse<string>>(this.apiUrl, data).pipe(
            catchError((error:HttpErrorResponse)=> {
                console.error('api error', error.message);
                return throwError(() => error.message);
            })
        );
    }
}