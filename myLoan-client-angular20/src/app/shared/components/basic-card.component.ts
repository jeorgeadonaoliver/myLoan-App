import { Component, input } from "@angular/core";

@Component({
    selector: 'app-basic-card',
    imports: [],
    template:`
        <section class="bg-white mb-6 rounded-3xl shadow-[0_10px_25px_rgba(0,0,0,0.3)] p-10">
            <h2 class="text-2xl font-semibold text-gray-700 mb-4">{{Title()}}</h2>
            <ng-content></ng-content>
        </section>
    `
})
export class CardComponent{
    Title = input()
}