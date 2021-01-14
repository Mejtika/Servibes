import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';

import { Company, Category, OpeningHours, SearchedCompanyDto, PagedResult, CompanyDetails } from '../shared/interfaces/company';
import { IImage } from "../business-main/business-modules/profile/models/profile.model";

@Injectable()
export class CompanyDataService extends ApiService<Company> {

  constructor(http: HttpClient) {
    super(http)
  }

  companies$ =  this.get<CompanyDetails[]>(`companies`);

  public getAllCompanies(
    page:number, 
    pageSize: number, 
    category: string = "") : Observable<PagedResult<SearchedCompanyDto[]>> {
      console.log(page, pageSize, category);
      if(category === "All" || category === ""){
        console.log("all")
        return this.get(`companies/search?page=${page}&pageSize=${pageSize}`);
      }      
      return this.get(`companies/search?page=${page}&pageSize=${pageSize}&category=${category}`);
  }

  public getCompanyById(companyId: string) : Observable<Company> {
    return this.get(`companies/${companyId}`);
  }

  public getCompanyOpeningHours(companyId: string): Observable<OpeningHours[]> {
    return this.get(`companies/${companyId}/openingHours`);
  }

  // public getCategories(): string[] {
  //   return Object.keys(Category).map((key) => Category[key]);
  // }

  public getImage(imageId: string) : Observable<IImage> {
      return this.get(`companies/images/${imageId}`);
  }
}
