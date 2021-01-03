import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ApiService } from 'src/app/core/services/api.service';
import { IHoursRange, IProfile } from '../models';

@Injectable()
export class ProfileService extends ApiService<IProfile> {
    constructor(http: HttpClient) {
        super(http)
    }

    public getProfile() : Observable<IProfile> {
        return this.get(`companies/owner`);
    }

    public updateProfile(updatedProfile: IProfile) : Observable<IProfile> {
        return this.put(`companies/${updatedProfile.companyId}`, updatedProfile);
    }

    public getOpeningHours(companyId: string) : Observable<IHoursRange[]> {
        return this.get(`companies/${companyId}/openingHours`);
    }

    public changeOpeningHours(companyId: string, body: object) {
        console.log('Body', body);

        return this.post(`companies/${companyId}/openingHours`, body);
    }
}
