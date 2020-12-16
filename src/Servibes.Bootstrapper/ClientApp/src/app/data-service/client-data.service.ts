import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';

import { IClient } from '../shared/interfaces/company';

@Injectable()
export class ClientDataService extends ApiService<IClient> {

  constructor(http: HttpClient) {
    super(http)
  }

  public getClientData(): Observable<IClient> {
    return this.get(`identity/me`);
  }
}
