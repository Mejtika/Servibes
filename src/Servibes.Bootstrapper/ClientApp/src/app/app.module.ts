import { NgModule } from '@angular/core';

import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';

import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    ApiAuthorizationModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { 

}
