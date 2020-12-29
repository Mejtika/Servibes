import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';

import { Company, Category, OpeningHours } from '../shared/interfaces/company';

@Injectable()
export class CompanyDataService extends ApiService<Company> {

  constructor(http: HttpClient) {
    super(http)
  }

  companies$ =  this.get<Company[]>(`companies`);

  public getAllCompanies(category: Category = Category.All) : Observable<Company[]> {
    if(category == Category.All)
      return this.get(`companies/`);

    return this.get(`companies?category=${category}`);
  }

  public getCompanyById(companyId: string) : Observable<Company> {
    return this.get(`companies/${companyId}`);
  }

  public getCompanyOpeningHours(companyId: string): Observable<OpeningHours[]> {
    return this.get(`companies/${companyId}/openingHours`);
  }

  public getCategories(): string[] {
    return Object.keys(Category).map((key) => Category[key]);
  }
}
