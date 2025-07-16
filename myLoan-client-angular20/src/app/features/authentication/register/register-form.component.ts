import { CommonModule } from '@angular/common';
import { Component, output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegisterFormFields } from './register-form-fields';

@Component({
  selector: 'app-register-form-component',
  imports: [CommonModule, ReactiveFormsModule],
  template:`

  <div>
      <p class="text-4xl font-bold leading-tight tracking-tight text-gray-800 mb-8">
        Create an account
      </p>

      <form class="grid grid-cols-1 md:grid-cols-2 gap-6" [formGroup]="registerForm" (ngSubmit)="submit()">
        <div>
          <label class="block mb-2 text-sm font-medium text-gray-800" for="firstName">First Name</label>
          <input placeholder="Enter your first name" formControlName="firstName"
            class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
            id="firstName" type="text">
        </div>

        <div>
          <label class="block mb-2 text-sm font-medium text-gray-800" for="lastName">Last Name</label>
          <input placeholder="Enter your last name" formControlName="lastName"
            class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
            id="lastName" type="text">
        </div>

        <div>
          <label class="block mb-2 text-sm font-medium text-gray-800" for="dateOfBirth">Date of Birth</label>
          <input placeholder="YYYY-MM-DD" formControlName="dateOfBirth"
            class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
            id="dateOfBirth" type="text">
        </div>

        <div>
          <label class="block mb-2 text-sm font-medium text-gray-800" for="email">Email</label>
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
            Create an account
          </button>
        </div>
      </form>
  </div>

  `
})
export class RegisterFormComponent {
    formSubmitted = output<RegisterFormFields>();

    registerForm = new FormGroup({
        firstName: new FormControl('', [Validators.required]),
        lastName: new FormControl('', [Validators.required]),
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required, Validators.minLength(6)]),
        dateOfBirth: new FormControl('', [Validators.required])
    });

    submit(){

        if (this.registerForm.valid) {
            const formValue = this.registerForm.value;

            const transformed: RegisterFormFields = {
                ...formValue, 
                dateOfBirth: new Date(formValue.dateOfBirth??'1900/01/01').toISOString()
            } as RegisterFormFields;

            this.formSubmitted.emit(transformed);
        } else {

            console.log(this.registerForm.value);
            this.registerForm.markAllAsTouched();
            console.error('Form is invalid');
        }
    }
}
