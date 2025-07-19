import { Component } from "@angular/core";
import { FormComponent } from "../../../shared/components/form.component";
import { CardComponent } from "../../../shared/components/basic-card.component";
import { ProfileComponent } from "./profile.component";

@Component({
    selector: 'app-profile-page',
    imports: [CardComponent, ProfileComponent],
    template: `
        <app-basic-card>
        <div>
            <p class="text-4xl font-bold leading-tight tracking-tight text-gray-800 mb-8">
                Update your profile
            </p>
            <p class="text-gray-700 leading-relaxed text-sm italic">
              - <strong>User ID</strong> and <strong>Email</strong> is disabled on purpose. please contact HR for the information update.
            </p>
            <br/>
             <app-profile-component></app-profile-component>
        </div>
        </app-basic-card>

    `,
})
export class ProfilePage{

}