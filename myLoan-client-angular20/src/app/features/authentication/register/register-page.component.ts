import { Component } from "@angular/core";
import { RegisterFormComponent } from "./register-form.component";
import { CommonModule } from "@angular/common";
import { RegisterStore } from "./register.store";
import { RegisterFormFields } from "./register-form-fields";
import { FormComponent } from "../../../shared/components/form.component";

@Component({
  selector: 'app-register-page',
    imports: [CommonModule, RegisterFormComponent, FormComponent],
    template:`
      <div class="min-h-screen min-w-screen flex items-center justify-center"> 
        <app-form-component>
          <app-register-form-component (formSubmitted)="onSubmit($event)" />

          <div *ngIf="store.isLoading()" class="text-sm text-blue-400 text-center animate-pulse">
            Loading...
          </div>
          <div *ngIf="store.errorMessage()" class="text-sm text-red-500 text-center">
            {{ store.errorMessage() }}
          </div>
          <div *ngIf="store.isSuccess()" class="text-sm text-green-400 text-center">
            Registration Successful!
          </div>
      </app-form-component>
      </div>
    `
})

export class RegisterPage {
    constructor(public store: RegisterStore) {}

    onSubmit(formData: RegisterFormFields) {
        console.log('formData:', formData);
        this.store.sendRequest(formData);
    }
};