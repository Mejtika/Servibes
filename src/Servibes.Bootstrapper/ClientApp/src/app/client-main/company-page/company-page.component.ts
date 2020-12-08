import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CompanyDataService } from 'src/app/data-service/company-data.servce';
import { EmployeeDataService } from 'src/app/data-service/employee-data.service';
import { MockDataService } from 'src/app/data-service/mock-data.service';
import { ServicesDataService } from 'src/app/data-service/services-data.service';

import { ICompany } from 'src/app/shared/interfaces/company';

@Component({
    selector: 'company-page',
    templateUrl: 'company-page.component.html',
    styleUrls: ['./company-page.component.css']
})
export class CompanyPageComponent {
    company: ICompany;

    constructor(
        private companyDataService: CompanyDataService,
        private servicesDataService: ServicesDataService,
        private employeeDataService: EmployeeDataService,
        private activatedRoute: ActivatedRoute) {

    }

    ngOnInit() {
        const companyId: string = this.activatedRoute.snapshot.paramMap.get('id');

        /*this.dataService.getCompany(id).subscribe(result => {
            this.company = result;
        });*/

        //TODO: Fix 3 api calls into 1 using rxjs

        this.companyDataService.getCompanyById(companyId).subscribe(result => {
          this.company = result;
          console.log('company', result);

          this.servicesDataService.getAllCompanyServices(companyId).subscribe(result => {
            this.company.services = result;
            console.log('services', result);
          });

          this.employeeDataService.getAllCompanyEmployees(companyId).subscribe(result => {
            this.company.employees = result;
            console.log('employees', result);
          });
        });


    }
}
