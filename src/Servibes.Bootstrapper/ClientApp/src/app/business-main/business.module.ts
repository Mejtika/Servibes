import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { BusinessMainComponent } from './business-main.component';
import { BusinessRegisterComponent } from './business-register/business-register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TimeArray } from '../shared/others/time-array';
import { BusinessRegisterGuard } from './business-register/business-register.guard';

const routes: Routes = [
    { 
        path: "", pathMatch: "full", redirectTo: "register" 
    },
    {
        path: "",
        component: BusinessMainComponent,
        children: [
            {
                path: "register", 
                component: BusinessRegisterComponent,
                canActivate: [BusinessRegisterGuard]
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
    ],
    imports: [
        CommonModule,
        SharedModule,
        RouterModule.forChild(routes),
        ReactiveFormsModule
    ],
    providers: [
        TimeArray,
        BusinessRegisterGuard
    ]
})
export class BusinessModule {

}