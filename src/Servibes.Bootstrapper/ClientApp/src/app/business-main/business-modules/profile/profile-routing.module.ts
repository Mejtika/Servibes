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
        path: 'employees/:employeeId',
        canActivate: [],
        component: profileContainers.EmployeeFormComponent,
        data: {
            title: 'Employee form',
            breadcrumbs: [
                {
                    text: "Profile",
                    link: '/business/profile'
                },
                {
                    text: 'Employees',
                    link: '/business/profile/employees'
                },
                {
                    text: 'Manage',
                    active: true
                }
            ]
        } as SBRouteData
    },
    {
        path: 'employees/:employeeId/workinghours',
        canActivate: [],
        component: profileContainers.EmployeeWorkingHoursComponent,
        data: {
            title: 'Working hours',
            breadcrumbs: [
                {
                    text: "Profile",
                    link: '/business/profile'
                },
                {
                    text: 'Employees',
                    link: '/business/profile/employees'
                },
                {
                    text: 'Working hours',
                    active: true
                }
            ]
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
    },
    {
        path: 'services/:serviceId',
        canActivate: [],
        component: profileContainers.ServiceFormComponent,
        data: {
            title: 'Service form',
            breadcrumbs: [
                {
                    text: "Profile",
                    link: '/business/profile'
                },
                {
                    text: 'Services',
                    link: '/business/profile/services'
                },
                {
                    text: 'Manage',
                    active: true
                }
            ]
        } as SBRouteData
    },
    {
        path: 'openinghours',
        canActivate: [],
        component: profileContainers.OpeningHoursComponent,
        data: {
            title: 'Opening hours',
            breadcrumbs: [
                {
                    text: 'Profile',
                    link: '/business/profile'
                },
                {
                    text: 'Opening hours',
                    active: true
                }
            ]
        } as SBRouteData
    }
];

@NgModule({
    imports: [ProfileModule, RouterModule.forChild(ROUTES)],
    exports: [RouterModule],
})
export class ProfileRoutingModule {}
