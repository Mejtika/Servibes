import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'profile',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
    constructor() {}
    ngOnInit() {}
}
