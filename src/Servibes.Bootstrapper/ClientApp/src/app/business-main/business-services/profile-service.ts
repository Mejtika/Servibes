import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { BusinessProfile } from '../models/BusinessProfile';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  constructor(private apiService: ApiService<BusinessProfile>) {

  }

  getBusinessProfile(id: string): Observable<BusinessProfile> {
    return this.apiService.get(`api/companies/${id}`);
  }

  addBusinessProfile(businessProfile: BusinessProfile): Observable<BusinessProfile> {
    return this.apiService.post(`api/companies`, businessProfile);
  }

  updateBusinessProfile(businessProfile: BusinessProfile): Observable<BusinessProfile> {
    //TODO: Think about doing patch instead
    return this.apiService.post(`api/companies/${businessProfile.id}`, businessProfile);
  }
}
