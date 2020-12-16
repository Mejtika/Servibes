import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';

import { RouterModule, Routes } from '@angular/router';
import { CalendarComponent } from './appointments/appointments.component';
import { SharedModule } from '../shared/shared.module';

import { DxSchedulerModule, DxTemplateModule } from 'devextreme-angular';
import { MockDataService } from '../data-service/mock-data.service';
import { BusinessFooterComponent } from './business-footer/business-footer.component';
import { BusinessNavbarComponent } from './business-navbar/business-navbar.component';
import { BusinessSidenavComponent } from './business-sidenav/business-sidenav.component';
import { BusinessMainComponent } from './business-main.component';
import { BusinessRegisterComponent } from './business-register/business-register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TimeArray } from '../shared/others/time-array';
import { SalesComponent } from './sales/sales.component';
import { ClientBaseComponent } from './client-base/client-base.component';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { ReviewsComponent } from './reviews/reviews.component';
import { BusinessProfileComponent } from './business-profile/business-profile.component';

const routes: Routes = [
    { 
        path: "", pathMatch: "full", redirectTo: "appointments" 
    },
    {
        path: "",
        component: BusinessMainComponent,
        children: [
            {
                path: "register", 
                component: BusinessRegisterComponent
            },
            {
                path: "appointments",
                component: CalendarComponent
            },
            {
                path: "sales",
                component: SalesComponent
            },
            {
                path: "clientbase",
                component: ClientBaseComponent
            },
            {
                path: "portfolio",
                component: PortfolioComponent
            },
            {
                path: "reviews",
                component: ReviewsComponent
            },
            {
                path: "businessprofile",
                component: BusinessProfileComponent
            }
        ]
    }
]

@NgModule({
    declarations: [
        BusinessMainComponent,
        BusinessNavbarComponent,
        BusinessFooterComponent,
        BusinessSidenavComponent,
        BusinessRegisterComponent,
        CalendarComponent,
        SalesComponent,
        ClientBaseComponent,
        PortfolioComponent,
        ReviewsComponent,
        BusinessProfileComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        RouterModule.forChild(routes),
        DxSchedulerModule,
        DxTemplateModule,
        ReactiveFormsModule
    ],
    providers: [
        MockDataService,
        TimeArray
    ]
})
export class BusinessModule {

}