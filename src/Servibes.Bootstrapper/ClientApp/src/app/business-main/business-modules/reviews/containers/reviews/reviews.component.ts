import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { IProfile } from '../../../profile/models';
import { ProfileService } from '../../../profile/services/profile.service';
import { IReview } from '../../models/review.model';
import { ReviewsService } from '../../services';

@Component({
    selector: 'reviews',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './reviews.component.html',
    styleUrls: ['./reviews.component.scss'],
})
export class ReviewsComponent implements OnInit {
    public reviews: IReview[];
    profile: IProfile;

    constructor(private reviewsService: ReviewsService,
        private profileService: ProfileService,
        private cd: ChangeDetectorRef) {}

    ngOnInit() {
        this.profileService.getProfile().subscribe(profile => {
            this.profile = profile;

            this.reviewsService.getReviews(this.profile.companyId).subscribe(reviews => {
                this.reviews = reviews;

                /*this.reviews = [
                    {
                        description: "test review text description",
                        starsCount: 3,
                        name: "Client Name"
                    },
                    {
                        description: "test review text description",
                        starsCount: 2,
                        name: "Client Name"
                    },
                    {
                        description: "test review text description",
                        starsCount: 5,
                        name: "Client Name"
                    },
                    {
                        description: "test review text description",
                        starsCount: 2,
                        name: "Client Name"
                    },
                    {
                        description: "test review text description",
                        starsCount: 2,
                        name: "Client Name"
                    },
                    {
                        description: "test review text description",
                        starsCount: 2,
                        name: "Client Name"
                    },
                    {
                        description: "test review text description",
                        starsCount: 2,
                        name: "Client Name"
                    },
                    {
                        description: "test review text description",
                        starsCount: 2,
                        name: "Client Name"
                    },
                    {
                        description: "test review text description",
                        starsCount: 2,
                        name: "Client Name"
                    },

                ]*/

                this.cd.markForCheck();
            });
        });
    }
}
