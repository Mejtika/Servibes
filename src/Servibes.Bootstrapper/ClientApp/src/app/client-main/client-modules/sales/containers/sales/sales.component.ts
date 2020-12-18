import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'sales',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './sales.component.html',
    styleUrls: ['./sales.component.scss'],
})
export class SalesComponent implements OnInit {
    constructor() {}
    ngOnInit() {}
}
