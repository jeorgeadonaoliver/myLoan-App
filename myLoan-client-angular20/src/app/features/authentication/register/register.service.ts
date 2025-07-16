import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { RegisterFormFields } from "./register-form-fields";
import { Observable, throwError } from "rxjs";
import { environment } from "../../../environment/environment";
import { apiResponse } from "../../../shared/domain/apiResponse.type";
import { catchError } from 'rxjs/operators';

@Injectable({ providedIn:'root' })
export class RegisterService {
    constructor(private httpClient: HttpClient) {}
    private readonly apiUrl = `${environment.apiUrl}/auth/register`;

    send(data: RegisterFormFields): Observable<apiResponse<string>> {
        return this.httpClient.post<apiResponse<string>>(this.apiUrl, data).pipe(
            catchError((error: HttpErrorResponse) => {
                console.error('Api Error:', error.message);
                return throwError(() => error);
            })
        )
    }
}