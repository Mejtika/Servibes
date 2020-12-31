/* tslint:disable: ordered-imports*/
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

/* Modules */
import { AppCommonModule } from '../app-common/app-common.module';
import { NavigationModule } from '../navigation/navigation.module';

/* Components */
//import * as salesComponents from './components';

/* Containers */
import * as salesContainers from './containers';

/* Guards */
import * as salesGuards from './guards';

/* Services */
import * as salesServices from './services';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        ReactiveFormsModule,
        FormsModule,
        AppCommonModule,
        NavigationModule,
    ],
    providers: [...salesServices.services, ...salesGuards.guards],
    declarations: [...salesContainers.containers, /*...appointmentsComponents.components*/],
    exports: [...salesContainers.containers, /*...appointmentsComponents.components*/],
})
export class SalesModule {}
