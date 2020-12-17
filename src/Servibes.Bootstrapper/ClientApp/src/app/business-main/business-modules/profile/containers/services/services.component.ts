import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'services',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './services.component.html',
    styleUrls: ['./services.component.scss'],
})
export class ServicesComponent implements OnInit {
    constructor() {}
    ngOnInit() {}
}
