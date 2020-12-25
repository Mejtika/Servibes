import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, combineLatest } from "rxjs";
import { environment } from "src/environments/environment";
import { ReviewDetails } from "./client-reviews/client-reviews.component";
import { CompanyDataService } from "../../data-service/company-data.service";
import { map, switchMap, tap } from "rxjs/operators";

export interface LeaveReviewRequest {
  reviewId: string;
  description: string;
  starsCount: number;
}

@Injectable()
export class ClientReviewsService {
  private newReviewAddedSubject = new BehaviorSubject<boolean>(false);
  newReviewAddedAction$ = this.newReviewAddedSubject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private companyService: CompanyDataService
  ) {}

  reviews$ = this.httpClient.get<ReviewDetails[]>(
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

  leaveReview(request: LeaveReviewRequest) {
    return this.httpClient
      .post(`${environment.backendEndpoint}account/reviews`, request);
  }
}
