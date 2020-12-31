import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'appointments',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './appointments.component.html',
    styleUrls: ['appointments.component.scss'],
})
export class AppointmentsComponent implements OnInit {
    constructor() {}
    ngOnInit() {}
}
