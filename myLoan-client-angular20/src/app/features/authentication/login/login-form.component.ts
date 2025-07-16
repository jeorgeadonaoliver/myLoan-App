import { Component, output } from "@angular/core";
import { LoginFormFields } from "./login-form-fields";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";

@Component({
    selector: 'app-login-form-component',
    imports:[ReactiveFormsModule],
    template: `
    <div>
      <p class="text-4xl font-bold leading-tight tracking-tight text-gray-800 mb-8">
        Welcome back!
      </p>

      <form class="grid grid-cols-2 md:grid-cols-1 gap-6" [formGroup]="loginForm" (ngSubmit)="submit()">
        <div class="md:col-span-2">
          <label class="block text-sm font-medium text-gray-800" for="email">Email</label>
          <input placeholder="admin@example.com" formControlName="email"
            class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
            id="email" type="email">
        </div>

        <div class="md:col-span-2">
          <label class="block mb-2 text-sm font-medium text-gray-800" for="password">Password</label>
          <input placeholder="Enter strong password" formControlName="password"
            class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
            id="password" type="password">
        </div>

        <div class="md:col-span-2">
          <button type="submit"
            class="w-full bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center text-white">
            Login
          </button>
        </div>
      </form>
  </div>
    `
})

export class LoginFormComponent{
    formSubmitted = output<LoginFormFields>();

    loginForm = new FormGroup({
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required, Validators.minLength(8)])
    });

    submit(){
        if(this.loginForm.valid){
            this.formSubmitted.emit(this.loginForm.value as LoginFormFields);
        }else{
            this.loginForm.markAllAsTouched();
            console.error("Form is invalid");
        }
    }
};