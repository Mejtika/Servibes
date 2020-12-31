/* tslint:disable: ordered-imports*/
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

/* Modules */
import { AppCommonModule } from '../app-common/app-common.module';
import { NavigationModule } from '../navigation/navigation.module';

/* Components */
//import * as profileComponents from './components';

/* Containers */
import * as profileContainers from './containers';

/* Guards */
import * as profileGuards from './guards';

/* Services */
import * as profileServices from './services';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        ReactiveFormsModule,
        FormsModule,
        AppCommonModule,
        NavigationModule,
        SharedModule
    ],
    providers: [...profileServices.services, ...profileGuards.guards],
    declarations: [...profileContainers.containers, /*...appointmentsComponents.components*/],
    exports: [...profileContainers.containers, /*...appointmentsComponents.components*/],
})
export class ProfileModule {}
