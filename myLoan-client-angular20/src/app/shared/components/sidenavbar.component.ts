import { Component, effect, OnInit, signal } from "@angular/core";
import { ActivatedRoute, RouterModule } from "@angular/router";
import { NgIcon } from "@ng-icons/core";
import { UserStateService } from "../../core/state/user-state-service";

@Component({
    selector: 'app-sidenavbar',
    imports: [NgIcon, RouterModule],
    template: `

<aside class=" bg-gray-800 text-white shadow-lg z-50 w-full py-2 flex flex-row justify-around items-center sm:w-full sm:h-auto 
    md:w-16 md:h-screen md:flex-col md:justify-between md:py-4 md:fixed md:top-0 md:left-0 md:overflow-y-auto">
    <nav class="w-full">
        <ul class="flex flex-row justify-around md:flex-col md:justify-start list-none md:space-y-4">
            <li class="hover:bg-gray-700 md:my-4 md:px-2 md:block hidden my-2 px-2 w-full justify-center items-center">
                <div class="block py-4 text-center text-xl" title="logo">
                    <ng-icon name="matFlutterDash" routerLinkActive="router-link-active" size="40" color="white"></ng-icon>
                </div>
                <div class="border-t-2 border-gray-500 w-13 md:w-12"></div>
            </li>
            <li class="hover:bg-gray-700 transition-colors duration-200">
                <a [routerLink]="['/users/dashboard', email()]" class="block py-4 text-center text-xl" title="home">
                    <ng-icon name="matHome" routerLinkActive="router-link-active" size="26" color="white"></ng-icon>
                    <span class=" text-white text-xs font-medium leading-none opacity-0 md:opacity-100 max-h-0 md:max-h-none overflow-hidden transition-all duration-200 ease-in-out  w-full  md:block hidden sm:block ">Home</span>
                </a>
            </li>
            <li class="hover:bg-gray-700 transition-colors duration-200">
                <a href="#" class="block py-4 text-center text-xl" title="Profile">
                    <ng-icon name="matFolder" size="26" color="white"></ng-icon>
                    <span class=" text-white text-xs font-medium leading-none opacity-0 md:opacity-100 max-h-0 md:max-h-none overflow-hidden transition-all duration-200 ease-in-out  w-full  md:block hidden sm:block ">Docs</span>
                </a>
            </li>
            <li class="hover:bg-gray-700 transition-colors duration-200">
                <a href="#" class="block py-4 text-center text-xl" title="Settings">
                    <ng-icon name="matRealEstateAgent" size="26" color="white"></ng-icon>
                    <span class=" text-white text-xs font-medium leading-none opacity-0 md:opacity-100 max-h-0 md:max-h-none overflow-hidden transition-all duration-200 ease-in-out  w-full  md:block hidden sm:block ">Loans</span>
                </a>
            </li>
        </ul>
    </nav>
    <nav class="w-full">
        <ul class="flex flex-row justify-around md:flex-col md:justify-start list-none md:space-y-4">
            <li class="hover:bg-gray-700 transition-colors duration-200">
                <a [routerLink]="['/users/profile']" routerLinkActive="router-link-active" class="block py-4 text-center text-xl" title="Profile">
                    <ng-icon name="matManageAccounts" size="26" color="white"></ng-icon>
                    <span class=" text-white text-xs font-medium leading-none opacity-0 md:opacity-100 max-h-0 md:max-h-none overflow-hidden transition-all duration-200 ease-in-out  w-full  md:block hidden sm:block ">Profile</span>
                </a>
            </li>
            <li class="hover:bg-gray-700 transition-colors duration-200">
                <a href="#" class="block py-4 text-center text-xl" title="Settings">
                    <ng-icon name="matPowerSettingsNew" size="26" color="white"></ng-icon>
                    <span class=" text-white text-xs font-medium leading-none opacity-0 md:opacity-100 max-h-0 md:max-h-none overflow-hidden transition-all duration-200 ease-in-out  w-full  md:block hidden sm:block ">Logout</span>
                </a>
            </li>
        </ul>
    </nav>
</aside>
    `
})

export class SideNavbar {
    readonly email = signal('');

    constructor(private userinfo : UserStateService, private route: ActivatedRoute){
        
        effect(() =>{
            const _userInfo = this.userinfo.user$;
            this.email.set(_userInfo()?.email ?? '');
        });
    }
}