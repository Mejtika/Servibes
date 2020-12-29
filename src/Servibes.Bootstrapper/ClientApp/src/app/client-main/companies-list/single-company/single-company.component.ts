import { Component, Input } from '@angular/core';
import { Company } from 'src/app/shared/interfaces/company';

@Component({
    selector: 'single-company',
    templateUrl: './single-company.component.html',
    styleUrls: ['./single-company-component.css']
})
export class SingleCompanyComponent {
    @Input() company: Company;

    constructor() {
        
    }
}