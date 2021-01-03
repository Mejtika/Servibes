import { Component } from "@angular/core";
import {
  Category,
  SearchedCompanyDto,
  PagedResult,
} from "../../shared/interfaces/company";
import { ActivatedRoute } from "@angular/router";
import { CompanyDataService } from "src/app/data-service/company-data.service";

@Component({
  selector: "companies-list",
  templateUrl: "./companies-list.component.html",
  styleUrls: ["./companies-list.component.css"],
})
export class CompaniesListComponent {
  page: number = 1;
  pageSize: number = 10;
  collectionSize: number;
  companies: SearchedCompanyDto[];
  _category: Category;

  set category(category: Category) {
    this._category = category;
    this.getCompanies();
  }

  get category() {
    return this._category;
  }

  constructor(
    private companyDataService: CompanyDataService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.category = params.category;
    });
  }

  getCompanies() {
    this.companyDataService
      .getAllCompanies(this.page, this.pageSize, this.category)
      .subscribe((result: PagedResult<SearchedCompanyDto[]>) => {
        console.log(result.results, result.totalRecords);
        this.companies = result.results;
        this.collectionSize = result.totalRecords;
      });
  }
}
