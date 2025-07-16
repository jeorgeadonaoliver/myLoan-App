import { Component } from "@angular/core";

@Component({
    selector: 'app-form-component',
    imports:[],
    template: `

    <div class="flex justify-center items-center min-h-screen">
        <div class="w-full max-w-7xl bg-white rounded-3xl shadow-[0_10px_25px_rgba(0,0,0,0.3)] overflow-hidden p-10">
             <ng-content></ng-content>    
        </div>
    </div>

    `
})
export class FormComponent{}