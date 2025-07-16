import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { environment } from "../../../environment/environment";
import { LoginFormFields } from "./login-form-fields";
import { catchError, Observable, throwError } from "rxjs";
import { apiResponse } from "../../../shared/domain/apiResponse.type";
import { Injectable } from "@angular/core";

@Injectable({providedIn:'root'})
export class LoginService{
    private readonly apiUrl = `${environment.apiUrl}/auth/login`;
    constructor(private httpClient: HttpClient){}

    send(data: LoginFormFields): Observable<apiResponse<string>>{
        return this.httpClient.post<apiResponse<string>>(this.apiUrl, data).pipe(
            catchError((error:HttpErrorResponse)=> {
                console.error('api error', error.message);
                return throwError(() => error);
            })
        );
    }
}