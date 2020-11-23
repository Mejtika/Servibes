import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MockDataService } from '../data-service/mock-data.service';

import { SpinnerComponent } from './components/spinner/spinner.component';
import { MaterialModule } from './material.module';

@NgModule({
    imports: [
        CommonModule,
        MaterialModule
    ],
    declarations: [
        SpinnerComponent
    ],
    exports: [
        SpinnerComponent,
        MaterialModule
    ],
    providers: [MockDataService]
})
export class SharedModule {

}