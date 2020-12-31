import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable()
export class SalesService {
    constructor() {}

    getAppoinemtns$(): Observable<{}> {
        return of({});
    }
}
