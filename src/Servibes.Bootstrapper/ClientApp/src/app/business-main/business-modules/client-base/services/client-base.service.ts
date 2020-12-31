import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ApiService } from 'src/app/core/services/api.service';
import { IClient } from '../models/client.model';

@Injectable()
export class ClientBaseService extends ApiService<IClient> {
    constructor(http: HttpClient) {
        super(http);
    }

    getClients(companyId: string) : Observable<IClient[]> {
        return this.get(`companies/${companyId}/clients`);
    }
}
