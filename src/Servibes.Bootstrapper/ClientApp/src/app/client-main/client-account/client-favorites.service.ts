import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { CompanyDataService } from "src/app/data-service/company-data.service";
import { environment } from "src/environments/environment";
import { concatMap, map, tap, mergeMap } from "rxjs/operators";
import { forkJoin } from "rxjs";

export interface FavoritesCompaniesDto {
  companyId: string;
}

export interface AddToFavoritesRequest {
  companyId: string;
}

@Injectable()
export class ClientFavoritesService {
  constructor(
    private httpClient: HttpClient,
    private companyService: CompanyDataService
  ) {}

  public favoritesCompanies$ = this.httpClient
    .get<FavoritesCompaniesDto[]>(
      `${environment.backendEndpoint}account/favorites`
    )
    .pipe(
      mergeMap((ids) => {
        const obs = ids.map((dto) =>
          this.companyService.getCompanyById(dto.companyId)
        );
        return forkJoin(obs);
      })
    );

  public addToFavorites(companyId: string) {
    const requestBody: AddToFavoritesRequest = {
      companyId,
    };
    return this.httpClient.post(
      `${environment.backendEndpoint}account/favorites`,
      requestBody
    );
  }

  public deleteFromFavorites(companyId: string){
    return this.httpClient.delete(
      `${environment.backendEndpoint}account/favorites/${companyId}`,
    );
  }
}
