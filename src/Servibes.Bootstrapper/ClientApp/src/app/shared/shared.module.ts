import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MockDataService } from '../data-service/mock-data.service';

import { SpinnerComponent } from './components/spinner/spinner.component';
import { MaterialModule } from './material.module';
import { ApiAuthorizationModule } from "../../api-authorization/api-authorization.module";
import { LoginMenuComponent } from "../../api-authorization/login-menu/login-menu.component";

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    ApiAuthorizationModule
  ],
  declarations: [
    SpinnerComponent
  ],
  exports: [
    SpinnerComponent,
    MaterialModule,
    LoginMenuComponent
  ],
  providers: [MockDataService]
})
export class SharedModule {

}
