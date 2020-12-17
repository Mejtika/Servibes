import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';

import { RouterModule, Routes } from '@angular/router';
import { CalendarComponent } from './appointments/appointments.component';
import { SharedModule } from '../shared/shared.module';

import { DxSchedulerModule, DxTemplateModule } from 'devextreme-angular';
import { MockDataService } from '../data-service/mock-data.service';
import { BusinessMainComponent } from './business-main.component';
import { BusinessRegisterComponent } from './business-register/business-register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TimeArray } from '../shared/others/time-array';

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
                loadChildren: () => import('./business-modules/appointments/appointments-routing.module').then(m => m.AppointmentsRoutingModule),
            },
            {
                path: "sales",
                loadChildren: () => import('./business-modules/sales/sales-routing.module').then(m => m.SalesRoutingModule),
            },
            {
                path: "clientbase",
                loadChildren: () => import('./business-modules/client-base/client-base-routing.module').then(m => m.ClientBaseRoutingModule),
            },
            {
                path: "reviews",
                loadChildren: () => import('./business-modules/reviews/reviews-routing.module').then(m => m.ReviewsRoutingModule),
            },
            {
                path: "profile",
                loadChildren: () => import('./business-modules/profile/profile-routing.module').then(m => m.ProfileRoutingModule),
            }
        ]
    }
]

@NgModule({
    declarations: [
        BusinessMainComponent,
        BusinessRegisterComponent,
        CalendarComponent
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