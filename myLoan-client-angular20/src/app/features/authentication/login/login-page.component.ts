import { Component } from "@angular/core";
import { FormComponent } from "../../../shared/components/form-component";
import { LoginFormComponent } from "./login-form.component";
import { LoginFormFields } from "./login-form-fields";
import { LoginStore } from "./login.store";



@Component({
    selector: 'app-login-page',
    imports: [FormComponent, LoginFormComponent],
    template: `      
        <div class="min-h-screen min-w-screen flex items-center justify-center"> 
            <app-form-component>
                <app-login-form-component (formSubmitted)="onSubmit($event)"/>
            </app-form-component>
        </div>
    `
})

export class LoginPage{
    constructor(public store: LoginStore){}

    onSubmit(formData: LoginFormFields){
        console.log('formData:', formData);
        this.store.sendRequest(formData);
    }

};