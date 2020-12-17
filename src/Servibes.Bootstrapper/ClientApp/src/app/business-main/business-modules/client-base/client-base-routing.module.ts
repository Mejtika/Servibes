/* tslint:disable: ordered-imports*/
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SBRouteData } from '../navigation/models';

/* Module */
import { ClientBaseModule } from './client-base.module';

/* Containers */
import * as clientBaseContainers from './containers';

/* Guards */
import * as clientBaseGuards from './guards';

/* Routes */
export const ROUTES: Routes = [
    {
        path: '',
        canActivate: [],
        component: clientBaseContainers.ClientBaseComponent,
        data: {
            title: 'Client base',
            breadcrumbs: [
                {
                    text: 'Client base',
                    link: '/business/clientbase',
                },
                {
                    text: 'Client base',
                    active: true,
                },
            ],
        } as SBRouteData,
    },
];

@NgModule({
    imports: [ClientBaseModule, RouterModule.forChild(ROUTES)],
    exports: [RouterModule],
})
export class ClientBaseRoutingModule {}
