/* tslint:disable: ordered-imports*/
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

/* Modules */
import { AppCommonModule } from './../app-common/app-common.module';
import { NavigationModule } from './../navigation/navigation.module';

/* Components */
//import * as appointmentsComponents from './components';

/* Containers */
import * as appointmentsContainers from './containers';

/* Guards */
import * as appointmentsGuards from './guards';

/* Services */
import * as appointmentsServices from './services';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        ReactiveFormsModule,
        FormsModule,
        AppCommonModule,
        NavigationModule,
    ],
    providers: [...appointmentsServices.services, ...appointmentsGuards.guards],
    declarations: [...appointmentsContainers.containers, /*...appointmentsComponents.components*/],
    exports: [...appointmentsContainers.containers, /*...appointmentsComponents.components*/],
})
export class AppointmentsModule {}
