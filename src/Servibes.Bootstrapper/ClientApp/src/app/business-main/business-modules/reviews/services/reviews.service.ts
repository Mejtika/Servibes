import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ApiService } from 'src/app/core/services/api.service';
import { IReview } from '../models/review.model';

@Injectable()
export class ReviewsService extends ApiService<IReview> {
    constructor(http: HttpClient) {
        super(http)
    }

    getReviews(companyId: string): Observable<IReview[]> {
        return this.get(`companies/${companyId}/reviews`);
    }
}
