import { Injectable } from "@angular/core";
import { ProfileFormFields } from "./profile.form-fields";
import { environment } from "../../../environment/environment";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { catchError, Observable, tap, throwError } from "rxjs";
import { ApiResponse } from "../../../shared/domain/apiResponse.type";

@Injectable({providedIn:'root'})
export class ProfileService{
    private userapiUrl = `${environment.usersApiUrl}/Users/users`;
    constructor(private http: HttpClient){}

    sendRequest(data: ProfileFormFields): Observable<ApiResponse<string>>{
        return this.http.put<ApiResponse<string>>(this.userapiUrl, data).pipe(
            catchError((err:HttpErrorResponse) => {
                console.error('error on profile update: ', err.message);
                return throwError(() => err);
            })
        );
    }
}