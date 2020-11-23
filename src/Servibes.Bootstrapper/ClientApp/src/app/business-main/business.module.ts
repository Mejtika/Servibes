import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';

import { RouterModule, Routes } from '@angular/router';
import { CalendarComponent } from './calendar/calendar.component';
import { SharedModule } from '../shared/shared.module';

import { DxSchedulerModule, DxTemplateModule } from 'devextreme-angular';
import { MockDataService } from '../data-service/mock-data.service';
import { BusinessFooterComponent } from './business-footer/business-footer.component';
import { BusinessNavbarComponent } from './business-navbar/business-navbar.component';
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
                path: "appointments",
                component: CalendarComponent
            },
            {
                path: "register", 
                component: BusinessRegisterComponent
            }
        ]
    }
]

@NgModule({
    declarations: [
        BusinessMainComponent,
        CalendarComponent,
        BusinessNavbarComponent,
        BusinessFooterComponent,
        BusinessRegisterComponent
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