import { Component } from '@angular/core';

import { filter } from "rxjs/operators";
import { forkJoin } from "rxjs";

import { Category, ICompany } from '../../shared/interfaces/company';
import { MockDataService } from '../../data-service/mock-data.service';
import { ActivatedRoute } from '@angular/router';
import { CompanyDataService } from 'src/app/data-service/company-data.service';
import { ServicesDataService } from 'src/app/data-service/services-data.service';

@Component({
    selector: 'companies-list',
    templateUrl: './companies-list.component.html',
    styles: ['./companies-list.component.css']
})
export class CompaniesListComponent {
    companies: ICompany[];
    _category: Category;

    constructor(private dataService: MockDataService,
      private companyDataService: CompanyDataService,
      private servicesDataService: ServicesDataService,
      private route: ActivatedRoute) {
    }

    set category(category: Category) {
        this._category = category;
        this.getCompanies(this._category);
    }

    get category() {
        return this._category;
    }

    ngOnInit() {
        this.route.queryParams
            .pipe(filter((params) => params.category))
            .subscribe((params) => {
                this.category = params.category;
            });

        this.getCompanies(this.category);
    }

    getCompanies(category: Category) {
        this.companyDataService.getAllCompanies(this.category).subscribe(result => {
          this.companies = result;

          //TODO: Fix this to some kind of rxjs operation (to do two requests at once and combine them)

          this.companies.forEach(c => {
            this.servicesDataService.getAllCompanyServices(c.companyId).subscribe(services => {
              c.services = services;
            });
          });
        });
    }
}
