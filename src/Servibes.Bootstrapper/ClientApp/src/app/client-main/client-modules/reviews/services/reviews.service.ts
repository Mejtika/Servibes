import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable()
export class ReviewsService {
    constructor() {}

    getAppoinemtns$(): Observable<{}> {
        return of({});
    }
}
