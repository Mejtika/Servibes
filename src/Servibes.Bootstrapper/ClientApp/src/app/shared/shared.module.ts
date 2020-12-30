import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
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
    CommonModule,
    SpinnerComponent,
    MaterialModule,
    LoginMenuComponent
  ],
  providers: []
})
export class SharedModule {

}
