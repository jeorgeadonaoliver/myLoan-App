import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SideNavbar } from "../../shared/components/sidenavbar.component";

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, SideNavbar],
  template:`
  <div class="font-sans antialiased bg-gray-100 min-h-screen flex flex-col md:flex-row">
    <app-sidenavbar/>
    <main class="flex-grow p-6 ml-16 md:ml-16 sm:ml-0 mt-[theme('spacing.16')] sm:mt-0 flex flex-col">
        <router-outlet />
    </main>
  </div>
  `
})
export class MainLayout {

}
