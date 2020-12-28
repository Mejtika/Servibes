import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';

import { Client } from '../shared/interfaces/company';

@Injectable()
export class ClientDataService extends ApiService<Client> {

  constructor(http: HttpClient) {
    super(http)
  }

  public getClientData(): Observable<Client> {
    return this.get(`identity/me`);
  }
}
