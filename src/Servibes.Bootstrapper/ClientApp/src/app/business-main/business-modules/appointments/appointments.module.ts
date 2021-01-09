/* tslint:disable: ordered-imports*/
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
/* Modules */
import { AppCommonModule } from "./../app-common/app-common.module";
import { NavigationModule } from "./../navigation/navigation.module";

/* Components */
//import * as appointmentsComponents from './components';

/* Containers */
import * as appointmentsContainers from "./containers";

/* Guards */
import * as appointmentsGuards from "./guards";

/* Services */
import * as appointmentsServices from "./services";
import { ModalModule } from "ngx-bootstrap/modal";

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    AppCommonModule,
    NavigationModule,
    ModalModule.forRoot(),
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
  ],
  providers: [...appointmentsServices.services, ...appointmentsGuards.guards],
  declarations: [
    ...appointmentsContainers.containers /*...appointmentsComponents.components*/,
  ],
  exports: [
    ...appointmentsContainers.containers /*...appointmentsComponents.components*/,
  ],
})
export class AppointmentsModule {}
