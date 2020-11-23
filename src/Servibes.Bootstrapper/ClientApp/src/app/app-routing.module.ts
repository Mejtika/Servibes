import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizeInterceptor } from '../api-authorization/authorize.interceptor';
import { ApiAuthorizationModule } from '../api-authorization/api-authorization.module';

const routes: Routes = [
    {
        path: "authentication", loadChildren: () =>
          import("./../api-authorization/api-authorization.module")
    },
    {
        path: "", pathMatch: "full", loadChildren: () => 
           import("./landing-page/landing.module").then(module => module.LandingModule)
    },
        
    //{ path: "client", redirectTo: "/client/companies" },
    { 
        path: "client", loadChildren: () => 
            import("./client-main/client.module").then(module => module.ClientModule)
    },
    {
        path: "business", loadChildren: () =>
            import("./business-main/business.module").then(module => module.BusinessModule)
    },
    {
        path: "**", pathMatch: "full", redirectTo: "" 
    }
    
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forRoot(routes)
    ],
    exports: [
      RouterModule,
      ApiAuthorizationModule
    ],
  providers: [
        { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
    ]
})
export class AppRoutingModule {
    
}