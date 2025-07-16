import { BehaviorSubject, catchError, of, switchMap, tap, throwError } from "rxjs";
import { LoginFormFields } from "./login-form-fields";
import { LoginService } from "./login.service";
import { computed, Injectable, signal } from "@angular/core";
import { sign } from "crypto";
import { toSignal } from "@angular/core/rxjs-interop";

@Injectable({providedIn:'root'})
export class LoginStore{
    constructor(private loginService: LoginService){}
    private formSubject = new BehaviorSubject<LoginFormFields | null>(null);

    private loading = signal(true);
    private success = signal(true);
    private error$ = signal('');

    readonly isLoading = computed(() => this.loading);
    readonly isSuccess = computed(() => this.success);
    readonly isError = computed(() => this.error$);
    
    readonly response = toSignal(
        this.formSubject.asObservable().pipe(
            switchMap(form => {
                if(!form) return of({success: false, data: null, message: ''});
                this.loading.set(true);
                this.error$.set('');
                this.success.set(true);

                return this.loginService.send(form).pipe(
                    tap((res) => {
                        this.loading.set(false);
                        this.success.set(true);
                        console.log('this is response', res);
                    }),
                    catchError((error) => {
                        this.error$.set(error.message || 'error on login!');
                        return throwError(() => error.message);
                    })
                )
            })
        ),
        {initialValue : {success: false, data: null, message: 'error on login'}}
    );

    sendRequest(form: LoginFormFields){
        this.formSubject.next(form);
    }



}