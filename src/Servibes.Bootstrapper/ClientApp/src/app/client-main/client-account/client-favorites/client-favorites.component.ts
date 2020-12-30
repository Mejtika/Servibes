import { Component } from "@angular/core";
import { CompanyDataService } from "src/app/data-service/company-data.service";
import { CompanyDetails } from "src/app/shared/interfaces/company";
import { ClientFavoritesService } from "../client-favorites.service";

@Component({
  selector: "client-favorites",
  templateUrl: "./client-favorites.component.html",
  styleUrls: ["./client-favorites.component.css"],
})
export class ClientFavoritesComponent {
  page: number = 1;
  pageSize: number = 10;
  collectionSize: number;
  companies: CompanyDetails[];
  pagedCompanies: CompanyDetails[];

  constructor(private favoritesService: ClientFavoritesService) {}

  ngOnInit(): void {
    this.favoritesService.favoritesCompanies$.subscribe((result) => {
        this.companies = result;
        this.collectionSize = result.length;
        this.refreshCompanies();
    });
  }

  refreshCompanies() {
    this.pagedCompanies = this.companies
      .slice(
        (this.page - 1) * this.pageSize,
        (this.page - 1) * this.pageSize + this.pageSize
      );
  }
}
