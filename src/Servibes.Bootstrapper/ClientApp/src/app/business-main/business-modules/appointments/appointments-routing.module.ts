/* tslint:disable: ordered-imports*/
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SBRouteData } from './../navigation/models';

/* Module */
import { AppointmentsModule } from './appointments.module';

/* Containers */
import * as appointmentsContainers from './containers';

/* Guards */
import * as salesGuards from './guards';

/* Routes */
export const ROUTES: Routes = [
    {
        path: '',
        canActivate: [],
        component: appointmentsContainers.AppointmentCalendarComponent,
        data: {
            title: 'Appointments',
            breadcrumbs: [
                {
                    text: 'Appointments',
                    link: '/business/appointments',
                },
                {
                    text: 'Appointments',
                    active: true,
                },
            ],
        } as SBRouteData,
    },
];

@NgModule({
    imports: [AppointmentsModule, RouterModule.forChild(ROUTES)],
    exports: [RouterModule],
})
export class AppointmentsRoutingModule {}
