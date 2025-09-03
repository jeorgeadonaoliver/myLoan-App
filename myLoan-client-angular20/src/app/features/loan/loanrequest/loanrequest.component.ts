import { Component } from "@angular/core";
import { CardComponent } from "../../../shared/components/basic-card.component";

@Component({
    selector: 'app-loanrequest-component',
    imports: [CardComponent],
    template: `
        <div>
            <app-basic-card>
                <div>
                    <p class="text-4xl font-bold leading-tight tracking-tight text-gray-800 mb-8">
                        Loan Request Portal
                    </p>
                     <ul class="list-disc list-inside text-gray-600 space-y-2">
                        <li>Use this portal to initiate and submit your loan request.</li>
                        <li>Ensure all required fields in the request form are accurately completed.</li>
                        <li>Review all entered information thoroughly prior to submission.</li>
                    </ul>
                    <br/>
                </div>
        </app-basic-card>
        </div>
    `,
})
export class LoanRequestComponent{

}

