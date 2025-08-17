import { Component } from "@angular/core";
import { FormComponent } from "../../../shared/components/form.component";
import { CardComponent } from "../../../shared/components/basic-card.component";
import { ProfileComponent } from "./profile.component";
import { ProfileStore } from "./profile.store";
import { ProfileFormFields } from "./profile.form-fields";

@Component({
    selector: 'app-profile-page',
    imports: [CardComponent, ProfileComponent],
    template: `
    <div class="w-1/2">
        <app-basic-card>
        <div>
            <p class="text-4xl font-bold leading-tight tracking-tight text-gray-800 mb-8">
                Update your profile
            </p>
            <p class="text-gray-700 leading-relaxed text-sm italic">
              - <strong>User ID</strong> and <strong>Email</strong> is disabled on purpose. please contact HR for the information update.
            </p>
            <br/>
             <app-profile-component (formSubmitted)="onSubmit($event)"></app-profile-component>
        </div>
        </app-basic-card>
    </div>
    `,
})
export class ProfilePage{
    constructor(private store: ProfileStore){}

    onSubmit(formData: ProfileFormFields){
        this.store.sendRequest(formData);
    }
}