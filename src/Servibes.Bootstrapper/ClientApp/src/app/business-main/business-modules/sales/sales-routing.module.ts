/* tslint:disable: ordered-imports*/
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SBRouteData } from '../navigation/models';

/* Module */
import { SalesModule } from './sales.module';

/* Containers */
import * as salesContainers from './containers';

/* Guards */
import * as salesGuards from './guards';

/* Routes */
export const ROUTES: Routes = [
    {
        path: '',
        canActivate: [],
        component: salesContainers.SalesComponent,
        data: {
            title: 'Sales',
            breadcrumbs: [
                {
                    text: 'Sales',
                    active: true,
                },
            ],
        } as SBRouteData,
    },
    {
        path: 'history',
        canActivate: [],
        component: salesContainers.HistoryComponent,
        data: {
            title: 'History',
            breadcrumbs: [
                {
                    text: 'Sales',
                    link: '/business/sales',
                },
                {
                    text: 'History',
                    active: true,
                },
            ],
        } as SBRouteData,
    },
    
];

@NgModule({
    imports: [SalesModule, RouterModule.forChild(ROUTES)],
    exports: [RouterModule],
})
export class SalesRoutingModule {}
