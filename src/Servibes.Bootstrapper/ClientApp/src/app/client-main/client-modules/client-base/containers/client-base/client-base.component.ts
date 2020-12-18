import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'client-base',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './client-base.component.html',
    styleUrls: ['./client-base.component.scss'],
})
export class ClientBaseComponent implements OnInit {
    constructor() {}
    ngOnInit() {}
}
