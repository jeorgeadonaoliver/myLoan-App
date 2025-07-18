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
        
        <!-- <section class="bg-white p-6 mb-6 rounded-lg shadow-md">
            <h2 class="text-2xl font-semibold text-gray-700 mb-4">Section One</h2>
            <p class="text-gray-700 leading-relaxed">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            <p class="text-gray-700 leading-relaxed mt-4">Curabitur pretium tincidunt lacus. Nulla gravida orci a odio. Nullam varius, turpis et commodo pharetra, est eros bibendum elit, nec luctus magna felis sollicitudin mauris. Integer in mauris eu nibh egestas pharetra. Ut id mi vitae orci malesuada mollis. Sed non volutpat arcu, et sollicitudin lectus. Maecenas fringilla non dolor a mollis. Sed nec aliquet orci. Nam vel arcu at sapien lacinia posuere. Sed elementum, nulla eget bibendum consectetur, massa magna tincidunt sem, eu ultrices leo enim vel dui. Maecenas ac nibh risus. Nam et lectus eget elit facilisis tincidunt. Nam eget sem at urna tristique tempor. Sed vel libero quis purus feugiat gravida. Etiam vehicula justo ac enim volutpat, vitae imperdiet libero luctus.</p>
        </section>

        <section class="bg-white p-6 mb-6 rounded-lg shadow-md">
            <h2 class="text-2xl font-semibold text-gray-700 mb-4">Section Two</h2>
            <p class="text-gray-700 leading-relaxed">More content goes here. You can add various components like cards, tables, forms, etc.</p>
            <div class="h-96 bg-gray-50 p-4 text-center flex items-center justify-center border border-gray-200 rounded mt-4">
                <p class="text-gray-500">Scroll down to see the fixed sidebar in action on larger screens!</p>
            </div>
        </section> -->
    </main>
  </div>
  `
})
export class MainLayout {

}
