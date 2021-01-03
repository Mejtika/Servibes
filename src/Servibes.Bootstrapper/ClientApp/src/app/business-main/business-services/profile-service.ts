import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { IImage } from '../business-modules/profile/models/profile.model';
import { BusinessProfile } from '../models/BusinessProfile';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  constructor(private apiService: ApiService<BusinessProfile>) {

  }

  getBusinessProfile(id: string): Observable<BusinessProfile> {
    return this.apiService.get(`companies/${id}`);
  }

  addBusinessProfile(businessProfile: BusinessProfile): Observable<BusinessProfile> {
    return this.apiService.post(`companies`, businessProfile);
  }

  updateBusinessProfile(businessProfile: BusinessProfile): Observable<BusinessProfile> {
    //TODO: Think about doing patch instead
    return this.apiService.post(`companies/${businessProfile.id}`, businessProfile);
  }

  public hasProfile() : Observable<boolean> {
    return this.apiService.get(`companies/owner/exists`);
  }

  uploadImage(image: FormData) : Observable<IImage> {
    return this.apiService.post(`companies/images`, image);
  }

  getImage(imageId: string) : Observable<IImage> {
    return this.apiService.get(`companies/images/${imageId}`);
  }
}
