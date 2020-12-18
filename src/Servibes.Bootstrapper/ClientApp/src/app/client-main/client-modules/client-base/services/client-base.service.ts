import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable()
export class ClientBaseService {
    constructor() {}

    getAppoinemtns$(): Observable<{}> {
        return of({});
    }
}
