import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-auth-layout',
  imports: [RouterOutlet],
  template: `
    <div class="h-min-screen w-min-screen bg-gray-300">
      <router-outlet />
    </div>
  `
})
export class AuthLayout {

}
