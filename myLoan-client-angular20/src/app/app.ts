import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ReactiveFormsModule],
  template: `
  <div class="flex flex-col min-h-screen h-screen">
      <router-outlet></router-outlet>
</div>
`
})
export class App {
  protected title = 'myLoan-client-angular20';
}
