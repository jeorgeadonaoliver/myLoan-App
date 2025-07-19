import { Component, OnInit, signal } from "@angular/core";
import {  UserStateService } from "../../../core/state/user-state-service";
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";


@Component({
    selector:'app-profile-component',
    imports: [ReactiveFormsModule],
    template: `
        <form class="grid grid-cols-1 md:grid-cols-3 gap-6" [formGroup]="updateUserFormGroup" (submit)="onSubmit()">
            <div>
              <label class="block mb-2 text-sm font-medium text-gray-800" for="userId">User Id</label>
              <input placeholder="Enter your userId" formControlName="userId" value=""
                class=" placeholder-gray-400 bg-gray-200 border border-gray-300 text-gray-500 text-sm rounded-lg block w-full p-2.5 italic"
                id="userId" type="text">
            </div>
            <div>
              <label class="block mb-2 text-sm font-medium text-gray-800" for="firstName">First Name</label>
              <input placeholder="Enter your first name" formControlName="firstName" value=""
                class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
                id="firstName" type="text">
            </div>

            <div>
              <label class="block mb-2 text-sm font-medium text-gray-800" for="lastName">Last Name</label>
              <input placeholder="Enter your last name" formControlName="lastName"
                class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
                id="lastName" type="text">
            </div>

            <div class="md:col-span-2">
              <label class="block mb-2 text-sm font-medium text-gray-800" for="email">Email</label>
              <input placeholder="admin@example.com" formControlName="email"
                class=" placeholder-gray-400 bg-gray-200 border border-gray-300 text-gray-500 text-sm rounded-lg block w-full p-2.5 italic"
                id="email" type="email">
            </div>
            <div>
              <label class="block mb-2 text-sm font-medium text-gray-800" for="dateOfBirth">Date of Birth</label>
              <input placeholder="YYYY-MM-DD" formControlName="dateOfBirth"
                class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
                id="dateOfBirth" type="text">
            </div>
            <div class="md:col-span-3">
              <label class="block mb-2 text-sm font-medium text-gray-800" for="addressLine1">Address 1</label>
              <input placeholder="Enter you address" formControlName="addressLine1"
                class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
                id="addressLine1" type="text">
            </div>
            <div class="md:col-span-2">
              <label class="block mb-2 text-sm font-medium text-gray-800" for="addressLine2">Address 2</label>
              <input placeholder="Enter you address" formControlName="addressLine2"
                class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
                id="addressLine2" type="text">
            </div>
            <div>
              <label class="block mb-2 text-sm font-medium text-gray-800" for="city">City</label>
              <input placeholder="Enter your city" formControlName="city"
                class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
                id="city" type="text">
            </div>
            
            <div>
              <label class="block mb-2 text-sm font-medium text-gray-800" for="stateProvince">State Province</label>
              <input placeholder="Enter state province" formControlName="stateProvince"
                class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
                id="stateProvince" type="text">
            </div>
            <div>
              <label class="block mb-2 text-sm font-medium text-gray-800" for="postalCode">Postal Code</label>
              <input placeholder="Enter postal code" formControlName="postalCode"
                class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
                id="postalCode" type="text">
            </div>
            <div>
              <label class="block mb-2 text-sm font-medium text-gray-800" for="stateProvince">Country</label>
              <input placeholder="Enter country" formControlName="country"
                class="placeholder:italic placeholder-gray-400 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
                id="country" type="text">
            </div>

            <div>
              <label class="block mb-2 text-sm font-medium text-gray-800" for="createdAt">Created Date</label>
              <input placeholder="Enter create date" formControlName="createdAt" value=""
                class=" placeholder-gray-400 bg-gray-200 border border-gray-300 text-gray-500 text-sm rounded-lg block w-full p-2.5 italic"
                id="createdAt" type="text">
            </div>
            <div>
              <label class="block mb-2 text-sm font-medium text-gray-800" for="updatedAt">Modified Date</label>
              <input placeholder="Enter modified date" formControlName="updatedAt" value=""
                class=" placeholder-gray-400 bg-gray-200 border border-gray-300 text-gray-500 text-sm rounded-lg block w-full p-2.5 italic"
                id="updatedAt" type="text">
            </div>

            <div>
              <label class="block mb-2 text-sm font-medium text-gray-800" for="lastName">Status</label>
              <input placeholder="Enter status" formControlName="status" [value]="statusLabel()"
                class=" placeholder-gray-400 bg-gray-200 border border-gray-300 text-gray-500 text-sm rounded-lg block w-full p-2.5 italic"
                id="status" type="text">
            </div>

            <div class="md:col-span-3">
              <button type="submit"
                class="w-full bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center text-white">
                Create an account
              </button>
            </div>
          </form>
    `
})
export class ProfileComponent implements OnInit{
    updateUserFormGroup! : FormGroup;
    readonly statusLabel = signal('');

    constructor(private userinfo: UserStateService){}
    
    ngOnInit(): void {

        const _userinfo = this.userinfo.user$();
        console.log(_userinfo?.status);

        if(_userinfo?.status == '1'){
            this.statusLabel.set('Active');
        }else{
            this.statusLabel.set('Unknown');
        }


        this.updateUserFormGroup = new FormGroup({
        userId: new FormControl({value: _userinfo?.userId, disabled: true}, [Validators.required]),
        firstName : new FormControl(_userinfo?.firstName, [Validators.required]),
        lastName: new FormControl(_userinfo?.lastName,[Validators.required]),
        email: new FormControl({value: _userinfo?.email, disabled: true}, [Validators.required]),
        dateOfBirth: new FormControl('', [Validators.required]),
        addressLine1: new FormControl('', [Validators.required]),
        addressLine2: new FormControl('', [Validators.required]),
        city: new FormControl('', [Validators.required]),
        stateProvince: new FormControl('', [Validators.required]),
        postalCode: new FormControl('', [Validators.required]),
        country: new FormControl('', [Validators.required]),
        createdAt: new FormControl({value:_userinfo?.createdAt , disabled: true},[Validators.required]),
        updatedAt: new FormControl({value: _userinfo?.updatedAt, disabled: true}, [Validators.required]),
        status: new FormControl({value: this.statusLabel(), disabled: true},[Validators.required]) 
        });    
    };

    onSubmit(){
        console.log(this.updateUserFormGroup.getRawValue());
    }
}