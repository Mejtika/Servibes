import { NgModule } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { MockDataService } from '../data-service/mock-data.service';

import { ModalComponent } from '../shared/components/modal/modal.component';
import { ModalTriggerDirective } from '../shared/directives/modalTriggerr.directive';
import { JQUERY_TOKEN } from '../shared/services/jQuery.service';
import { ClientFooterComponent } from './client-footer/client-footer.component';
import { ClientMainComponent } from './client-main.component';
import { ClientNavbarComponent } from './client-navbar/client-navbar.component';
import { CompanyPageComponent } from './company-page/company-page.component';
import { SharedModule } from '../shared/shared.module';
import { CommonModule } from '@angular/common';

let jQuery = window['$'];

const routes: Routes = [
    { 
        path: "", pathMatch: "full", redirectTo: "companies" 
    },
    {
        path: "",
        component: ClientMainComponent,
        children: [
            {
                path: "companies",
                data: { preload: true },
                loadChildren: () =>
                    import("./companies-list/companies-list.module").then(module => module.CompaniesListModule)
            },
            {
                path: "companies/:id",
                component: CompanyPageComponent
            }
        ]
    }
]

@NgModule({
    declarations: [
        ClientMainComponent,
        ClientNavbarComponent,
        ClientFooterComponent,
        ModalComponent,
        ModalTriggerDirective,

        CompanyPageComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        RouterModule.forChild(routes),

        FormsModule,
        ReactiveFormsModule
    ],
    providers: [ 
        MockDataService, 
        { provide: JQUERY_TOKEN, useValue: jQuery } 
      ]
})
export class ClientModule {

}