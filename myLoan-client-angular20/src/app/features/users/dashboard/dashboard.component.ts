import { Component, input } from "@angular/core";

@Component({
    selector: 'app-dashboard-component',
    imports: [],
    template: `
    <div>
        <header class="bg-white p-6 mb-6 rounded-lg shadow-md">
            <h1 class="text-3xl font-bold text-gray-800 mb-2">Welcome {{fullname()}} to your Dashboard</h1>
            <p class="text-gray-600">This is the summary of your task.</p>
        </header>
    </div>

    `
})
export class DashboardComponent{
    fullname = input<string>('');
}