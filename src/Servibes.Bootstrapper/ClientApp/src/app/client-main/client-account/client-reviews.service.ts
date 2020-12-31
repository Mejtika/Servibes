import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, combineLatest } from "rxjs";
import { environment } from "src/environments/environment";
import { CompanyDataService } from "../../data-service/company-data.service";
import { map, switchMap, tap } from "rxjs/operators";
import { ReviewDetails } from "src/app/shared/interfaces/company";

export interface LeaveReviewRequest {
  reviewId: string;
  description: string;
  starsCount: number;
}

export interface CompanyReviewDto {
  description: string;
  starsCount: number | null;
  name: string;
}

export interface ReviewSummaryDto {
  rating: number;
  count: number;
  percentOfTotal: number;
}

export interface ReviewsSummaryDto {
  reviews: ReviewSummaryDto[];
  average: number;
  count: number;
}

@Injectable()
export class ClientReviewsService {
  private newReviewAddedSubject = new BehaviorSubject<boolean>(false);
  newReviewAddedAction$ = this.newReviewAddedSubject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private companyService: CompanyDataService
  ) {}

  private reviews$ = this.httpClient.get<ReviewDetails[]>(
    `${environment.backendEndpoint}account/reviews`
  );

  reviewsWithCompany$ = combineLatest([
    this.reviews$,
    this.companyService.companies$,
  ]).pipe(
    map(([reviews, companies]) => {
      return reviews.map(
        (review) =>
          ({
            ...review,
            company: companies.find((c) => review.companyId === c.companyId),
          } as ReviewDetails)
      );
    })
  );

  getCompanyReviews(companyId: string){
    return this.httpClient.get<ReviewDetails[]>(`${environment.backendEndpoint}companies/${companyId}/reviews`)
  }

  getReviewsSummary(companyId: string){
    return this.httpClient
    .get<ReviewSummaryDto>(`${environment.backendEndpoint}companies/${companyId}/reviews/summary`);
  }

  leaveReview(request: LeaveReviewRequest) {
    return this.httpClient
      .post(`${environment.backendEndpoint}account/reviews`, request);
  }
}
