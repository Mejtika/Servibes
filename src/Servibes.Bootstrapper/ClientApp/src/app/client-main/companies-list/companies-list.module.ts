import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppModule } from 'src/app/app.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CompaniesListComponent } from './companies-list.component';
import { SingleCompanyComponent } from './single-company/single-company.component';

const routes: Routes = [
    {
        path: "",
        component: CompaniesListComponent
    }
];

@NgModule({
    imports: [
        CommonModule,
        SharedModule, 
        RouterModule.forChild(routes)
    ],
    declarations: [
        CompaniesListComponent,
        SingleCompanyComponent,
    ],
})
export class CompaniesListModule {

}