import { computed, Injectable, signal } from "@angular/core";
import { RegisterService } from "./register.service";
import { RegisterFormFields } from "./register-form-fields";
import { toSignal } from '@angular/core/rxjs-interop';
import { BehaviorSubject, catchError, of, switchMap, tap, throwError } from "rxjs";


@Injectable({providedIn: 'root'})
export class RegisterStore {
    private formSubject = new BehaviorSubject<RegisterFormFields | null>(null);

    private loading = signal(false);
    private error = signal<string | null>(null);
    private success = signal(false);

    readonly isLoading = computed(() => this.loading());
    readonly errorMessage = computed(() => this.error());
    readonly isSuccess = computed(() => this.success());

    readonly result = toSignal(
        this.formSubject.asObservable().pipe(
            switchMap(form => {
                if(!form) return of({message: '', data: '', success: false});

                this.loading.set(true);
                this.error.set(null);
                this.success.set(false);

                return this.registerService.send(form).pipe(
                    tap(() =>{
                        this.success.set(true);
                        this.loading.set(false);
                    }),
                    catchError(err => {
                        this.loading.set(false);
                        this.error.set(err.message || 'Registration Failed!');
                        return of({success: false, data:'', message:''});
                    })
                );
            })
        ), 
        {initialValue: { message: '', data: '', success: false}});


    constructor(private registerService: RegisterService) {}

    sendRequest(form:RegisterFormFields){
        this.formSubject.next(form);
    }
};