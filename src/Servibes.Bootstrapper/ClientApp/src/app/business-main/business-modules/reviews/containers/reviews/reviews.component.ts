import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'reviews',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './reviews.component.html',
    styleUrls: ['./reviews.component.scss'],
})
export class ReviewsComponent implements OnInit {
    constructor() {}
    ngOnInit() {}
}
