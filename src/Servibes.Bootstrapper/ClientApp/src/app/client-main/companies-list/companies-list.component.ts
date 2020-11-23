import { Component } from '@angular/core';

import { filter } from "rxjs/operators";

import { Category, ICompany } from '../../shared/interfaces/company';
import { MockDataService } from '../../data-service/mock-data.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'companies-list',
    templateUrl: './companies-list.component.html',
    styles: ['./companies-list.component.css']
})
export class CompaniesListComponent {
    companies: ICompany[];
    _category: Category;

    constructor(private dataService: MockDataService,
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


        this.dataService.getCompanies(this.category).subscribe(result => {
            this.companies = result;
        });
    }

    getCompanies(category: Category) {
        this.dataService.getCompanies(this.category).subscribe(result => {
            this.companies = result;
        });
    }
}