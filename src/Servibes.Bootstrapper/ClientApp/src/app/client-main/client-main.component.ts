import { Component } from '@angular/core';
import { MockDataService } from '../data-service/mock-data.service';
import { Category, Company } from '../shared/interfaces/company';

@Component({
    selector: 'client-main',
    templateUrl: './client-main.component.html',
    styles: ['./client-main.component.css']
})
export class ClientMainComponent {

    constructor() {
    }
}