import { Component, Input, OnInit } from '@angular/core';
import { NgbRatingConfig } from '@ng-bootstrap/ng-bootstrap';
import { ReviewsSummaryDto, CompanyReviewDto } from '../../client-account/client-reviews.service';
import { ReviewDetails } from '../../client-account/client-reviews/client-reviews.component';

@Component({
  selector: 'company-ratings',
  templateUrl: './company-ratings.component.html',
  styleUrls: ['./company-ratings.component.css']
})
export class CompanyRatingsComponent {
  page: number = 1;
  pageSize: number = 3;
  collectionSize: number;
  pagedReviews: CompanyReviewDto[];

  @Input() reviewsSummary: ReviewsSummaryDto;
  @Input() reviews: CompanyReviewDto[];
  
  constructor(
    private rateConfig: NgbRatingConfig
  ) { 
    this.rateConfig.max = 5;
  }

  ngOnInit(): void {
    this.collectionSize = this.reviews.length;
    this.refreshReviews();
  }

  refreshReviews() {
    this.pagedReviews = this.reviews
      .slice(
        (this.page - 1) * this.pageSize,
        (this.page - 1) * this.pageSize + this.pageSize
      );
  }
}
