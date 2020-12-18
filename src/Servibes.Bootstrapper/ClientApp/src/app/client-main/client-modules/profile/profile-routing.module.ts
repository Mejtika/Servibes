/* tslint:disable: ordered-imports*/
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SBRouteData } from '../navigation/models';

/* Module */
import { ProfileModule } from './profile.module';

/* Containers */
import * as profileContainers from './containers';

/* Guards */
import * as profileGuards from './guards';

/* Routes */
export const ROUTES: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'profile'   
    },
    {
        path: 'profile',
        canActivate: [],
        component: profileContainers.ProfileComponent,
        data: {
            title: 'Profile',
            breadcrumbs: [
                {
                    text: 'Profile',
                    active: true,
                },
            ],
        } as SBRouteData,
    },
    {
        path: 'employees',
        canActivate: [],
        component: profileContainers.EmployeesComponent,
        data: {
            title: 'Employees',
            breadcrumbs: [
                {
                    text: 'Profile',
                    link: '/business/profile'
                },
                {
                    text: 'Employees',
                    active: true
                },
            ],
        } as SBRouteData
    },
    {
        path: 'services',
        canActivate: [],
        component: profileContainers.ServicesComponent,
        data: {
            title: 'Services',
            breadcrumbs: [
                {
                    text: 'Profile',
                    link: '/business/profile'
                },
                {
                    text: 'Services',
                    active: true
                },
            ],
        } as SBRouteData
    }
];

@NgModule({
    imports: [ProfileModule, RouterModule.forChild(ROUTES)],
    exports: [RouterModule],
})
export class ProfileRoutingModule {}
