import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable()
export class ProfileService {
    constructor() {}

    getAppoinemtns$(): Observable<{}> {
        return of({});
    }
}
