import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'employees',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './employees.component.html',
    styleUrls: ['./employees.component.scss'],
})
export class EmployeesComponent implements OnInit {
    constructor() {}
    ngOnInit() {}
}
