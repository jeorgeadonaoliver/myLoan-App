import { BehaviorSubject, catchError, of, switchMap, throwError } from "rxjs";
import { ProfileService } from "./profile.service";
import { ProfileFormFields } from "./profile.form-fields";
import { toSignal } from "@angular/core/rxjs-interop";
import { Injectable } from "@angular/core";

@Injectable({providedIn:'root'})
export class ProfileStore {
    private formSubject = new BehaviorSubject<ProfileFormFields | null>(null);
    constructor(private profileService: ProfileService){}

    readonly result = toSignal(
            this.formSubject.asObservable().pipe(
            switchMap((form) => {
                if(!form) return of({message: '', data: null, success: false});

                return this.profileService.send(form).pipe(
                    catchError((err) => {
                        console.error('error on updating profile', err.message);
                        return of({message:err.message, data: null, success: false});
                    })
                );
            })
        ),{
            initialValue: {message: '', data: '', success: false}
        }
    );

   sendRequest(formData: ProfileFormFields){
        console.log('store layer',formData);
        this.formSubject.next(formData);
   }
}