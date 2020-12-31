/* tslint:disable: ordered-imports*/
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

/* Modules */
import { AppCommonModule } from '../app-common/app-common.module';
import { NavigationModule } from '../navigation/navigation.module';

/* Components */
//import * as reviewsComponents from './components';

/* Containers */
import * as reviewsContainers from './containers';

/* Guards */
import * as reviewsGuards from './guards';

/* Services */
import * as reviewsServices from './services';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        ReactiveFormsModule,
        FormsModule,
        AppCommonModule,
        NavigationModule,
    ],
    providers: [...reviewsServices.services, ...reviewsGuards.guards],
    declarations: [...reviewsContainers.containers, /*...appointmentsComponents.components*/],
    exports: [...reviewsContainers.containers, /*...appointmentsComponents.components*/],
})
export class ReviewsModule {}
