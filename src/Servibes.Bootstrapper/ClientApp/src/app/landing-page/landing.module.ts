import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { LandingFooterComponent } from './landing-footer/landing-footer.component';
import { LandingNavbarComponent } from './landing-navbar/landing-navbar.component';
import { LandingPageComponent } from './landing-page.component';

import { LoginMenuComponent } from './../../api-authorization/login-menu/login-menu.component';
import { ApiAuthorizationModule } from '../../api-authorization/api-authorization.module';

const routes: Routes = [
    {
        path: "",
        component: LandingPageComponent
    }
];

@NgModule({
    declarations: [
        LandingPageComponent,
        LandingFooterComponent,
        LandingNavbarComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
        RouterModule.forChild(routes),

        FormsModule,
        ReactiveFormsModule,
        ApiAuthorizationModule
    ],
    providers: [ 
      ]
})
export class LandingModule {

}
