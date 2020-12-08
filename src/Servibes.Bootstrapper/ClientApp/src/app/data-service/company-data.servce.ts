import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';

import { ICompany, Category } from '../shared/interfaces/company';

@Injectable()
export class CompanyDataService extends ApiService<ICompany> {

  constructor(http: HttpClient) {
    super(http)
  }

  public getAllCompanies(category: Category = Category.All) : Observable<ICompany[]> {
    if(category == Category.All)
      return this.get(`companies/`);

    return this.get(`companies?category=${category}`);
  }

  public getCompanyById(companyId: string) : Observable<ICompany> {
    return this.get(`companies/${companyId}`);
  }

  public getCategories(): string[] {
    return Object.keys(Category).map((key) => Category[key]);
  }
}