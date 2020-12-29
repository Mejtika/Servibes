import { Component } from "@angular/core";
import {
  ClientReviewsService,
  LeaveReviewRequest,
} from "../client-reviews.service";
import { CompanyDetails } from "../client-appointments/client-appointments.component";
import { NgbRatingConfig } from "@ng-bootstrap/ng-bootstrap";
import { FormControl, FormGroup, NgForm, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";

export enum ReviewStatus {
  New,
  Leaved,
}
export interface ReviewDetails {
  reviewId: string;
  clientId: string;
  companyId: string;
  company?: CompanyDetails;
  description?: string;
  starsCount?: number;
  status: ReviewStatus;
}

@Component({
  selector: "client-reviews",
  templateUrl: "./client-reviews.component.html",
  styleUrls: ["./client-reviews.component.css"],
})
export class ClientReviewsComponent {
  page: number = 1;
  pageSize: number = 2;
  reviews: ReviewDetails[];
  pagedReviews: ReviewDetails[];
  collectionSize: number;
  addReviewCollapse = true;
  newReviewForm = new FormGroup({
    rating: new FormControl(null, Validators.required),
    description: new FormControl("", Validators.required),
  });

  constructor(
    private reviewsService: ClientReviewsService,
    private rateConfig: NgbRatingConfig,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };
    this.rateConfig.max = 5;
  }

  ngOnInit(): void {
    this.reviewsService.reviewsWithCompany$.subscribe((result) => {
      this.reviews = result;
      this.collectionSize = result.length;
      this.refreshReviews();
    });
  }
  
  isNew(review: ReviewDetails): boolean {
    return review.status === ReviewStatus.New;
  }

  leaveReview(reviewId: string) {
    const request: LeaveReviewRequest = {
      reviewId: reviewId,
      description: this.newReviewForm.value.description,
      starsCount: this.newReviewForm.value.rating,
    };

    this.reviewsService.leaveReview(request)
    .subscribe(_ => {
      this.refreshPage();
      this.toastr.success("Review was added successfully!");
    });
  }

  refreshReviews() {
    this.pagedReviews = this.reviews
    .slice(
      (this.page - 1) * this.pageSize,
      (this.page - 1) * this.pageSize + this.pageSize
    );
  }

  refreshPage(){
    this.router.navigateByUrl('/client/account/reviews');
  }
}
