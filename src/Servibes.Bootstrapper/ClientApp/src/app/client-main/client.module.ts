import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
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
import { CompanyDataService } from '../data-service/company-data.service';
import { ServicesDataService } from '../data-service/services-data.service';
import { EmployeeDataService } from '../data-service/employee-data.service';
import { AppointmentDataService } from '../data-service/appointment-data.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ClientDataService } from '../data-service/client-data.service';
import { ClientFavoritesService } from './client-account/client-favorites.service';
import { CompanyRatingsComponent } from './company-page/company-ratings/company-ratings.component';
import { ClientReviewsService } from './client-account/client-reviews.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { ClientNavbarSearchComponent } from './client-navbar/client-navbar-search/client-navbar-search.component';

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
      },
      {
        path: 'account',
        data: { preload: true },
        loadChildren: () => import('./client-account/client-account.module').then(m => m.ClientAccountModule)
      }
    ]
  }
];

@NgModule({
    declarations: [
        ClientMainComponent,
        ClientFooterComponent,
        ClientNavbarSearchComponent,
        ClientNavbarComponent,
        ModalComponent,
        ModalTriggerDirective,
        CompanyPageComponent,
        ClientReservationComponent,
        CompanyRatingsComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        RouterModule.forChild(routes),
        FormsModule,
        ReactiveFormsModule,
        NgSelectModule,
        NgbModule,
        ModalModule.forRoot()
    ],
    providers: [
        CompanyDataService,
        ServicesDataService,
        EmployeeDataService,
        AppointmentDataService,
        ClientDataService,
        ClientFavoritesService,
        ClientReviewsService,
        { provide: JQUERY_TOKEN, useValue: jQuery }
      ]
})
export class ClientModule {

}
