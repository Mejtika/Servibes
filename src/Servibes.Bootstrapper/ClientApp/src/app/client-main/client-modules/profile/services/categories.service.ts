import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ApiService } from 'src/app/core/services/api.service';
import { ICategory } from '../models';

@Injectable()
export class CategoriesService extends ApiService<ICategory> {
    constructor(http: HttpClient) {
        super(http)
    }

    public getCategories() : Observable<ICategory[]> {
        return this.get(`companies/categories`);
    }
}
