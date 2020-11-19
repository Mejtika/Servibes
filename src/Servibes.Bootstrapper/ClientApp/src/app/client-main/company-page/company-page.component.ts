import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MockDataService } from 'src/app/data-service/mock-data.service';

import { ICompany } from 'src/app/shared/interfaces/company';

@Component({
    selector: 'company-page',
    templateUrl: 'company-page.component.html',
    styleUrls: ['./company-page.component.css']
})
export class CompanyPageComponent {
    company: ICompany;

    constructor(
        private dataService: MockDataService,
        private activatedRoute: ActivatedRoute) {

    }

    ngOnInit() {
        const id: number = +this.activatedRoute.snapshot.paramMap.get('id');

        this.dataService.getCompany(id).subscribe(result => {
            this.company = result;
        });
    }
}