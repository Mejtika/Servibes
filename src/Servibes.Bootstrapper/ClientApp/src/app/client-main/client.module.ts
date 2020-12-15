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
import { ClientReservationComponent } from './company-page/client-reservation/client-reservation.component';
import { CompanyPageComponent } from './company-page/company-page.component';
import { SharedModule } from '../shared/shared.module';
import { CommonModule } from '@angular/common';
import { LoginMenuComponent } from "../../api-authorization/login-menu/login-menu.component";
import { CompanyDataService } from '../data-service/company-data.servce';
import { ServicesDataService } from '../data-service/services-data.service';
import { EmployeeDataService } from '../data-service/employee-data.service';
import { AppointmentDataService } from '../data-service/appointment-data.service';

import { ModalModule } from 'ngx-bootstrap/modal';
import { ClientDataService } from '../data-service/client-data.service';

let jQuery = window['$'];

const routes: Routes = [
  {
    path: "",
    pathMatch: "full",
    redirectTo: "companies"
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
];

@NgModule({
    declarations: [
        ClientMainComponent,
        ClientNavbarComponent,
        ClientFooterComponent,
        ModalComponent,
        ModalTriggerDirective,
        CompanyPageComponent,
        ClientReservationComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        RouterModule.forChild(routes),

        FormsModule,
        ReactiveFormsModule,

        ModalModule.forRoot()
    ],
    providers: [
        MockDataService,
        CompanyDataService,
        ServicesDataService,
        EmployeeDataService,
        AppointmentDataService,
        ClientDataService,
        { provide: JQUERY_TOKEN, useValue: jQuery }
      ]
})
export class ClientModule {

}
