/* tslint:disable: ordered-imports*/
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

/* Modules */
import { AppCommonModule } from '../app-common/app-common.module';
import { NavigationModule } from '../navigation/navigation.module';

/* Components */
//import * as clientBaseComponents from './components';

/* Containers */
import * as clientBaseContainers from './containers';

/* Guards */
import * as clientBaseGuards from './guards';

/* Services */
import * as clientBaseServices from './services';
import { BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        ReactiveFormsModule,
        FormsModule,
        AppCommonModule,
        NavigationModule,
        BsDatepickerModule.forRoot(),
        ModalModule.forRoot()
    ],
    providers: [...clientBaseServices.services, ...clientBaseGuards.guards],
    declarations: [...clientBaseContainers.containers, /*...appointmentsComponents.components*/],
    exports: [...clientBaseContainers.containers, /*...appointmentsComponents.components*/],
})
export class ClientBaseModule {}
