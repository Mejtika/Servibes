/* tslint:disable: ordered-imports*/
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SBRouteData } from '../navigation/models';

/* Module */
import { ReviewsModule } from './reviews.module';

/* Containers */
import * as reviewsContainers from './containers';

/* Guards */
import * as reviewsGuards from './guards';

/* Routes */
export const ROUTES: Routes = [
    {
        path: '',
        canActivate: [],
        component: reviewsContainers.ReviewsComponent,
        data: {
            title: 'Reviews',
            breadcrumbs: [
                {
                    text: 'Reviews',
                    link: '/business/reviews',
                },
                {
                    text: 'Reviews',
                    active: true,
                },
            ],
        } as SBRouteData,
    },
];

@NgModule({
    imports: [ReviewsModule, RouterModule.forChild(ROUTES)],
    exports: [RouterModule],
})
export class ReviewsRoutingModule {}
