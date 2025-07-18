import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { RegisterFormFields } from "./register-form-fields";
import { Observable, throwError } from "rxjs";
import { environment } from "../../../environment/environment";
import { ApiResponse } from "../../../shared/domain/apiResponse.type";
import { catchError } from 'rxjs/operators';

@Injectable({ providedIn:'root' })
export class RegisterService {
    constructor(private httpClient: HttpClient) {}
    private readonly apiUrl = `${environment.authenticationApiUrl}/auth/register`;

    send(data: RegisterFormFields): Observable<ApiResponse<string>> {
        
        return this.httpClient.post<ApiResponse<string>>(this.apiUrl, data).pipe(
            catchError((error: HttpErrorResponse) => {
                console.error('Api Error:', error.message);
                return throwError(() => error);
            })
        )
    }
}