import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable()
export class AppointmentsService {
    constructor() {}

    getAppoinemtns$(): Observable<{}> {
        return of({});
    }
}
