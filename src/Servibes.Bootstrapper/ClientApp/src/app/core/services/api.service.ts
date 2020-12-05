import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService<T> {

  constructor(private http: HttpClient) {

  }

  get<T>(path: string, params?): Observable<T> {
    return this.http.get(`${environment.backendEndpoint}${path}`, { params }) as Observable<T>;
  }

  post<T>(path: string, body = {}, params?): Observable<T> {
    return this.http.post(`${environment.backendEndpoint}${path}`, body, { params }) as Observable<T>;
  }

  put<T>(path: string, body = {}, params?): Observable<T> {
    return this.http.put(`${environment.backendEndpoint}${path}`, body, { params }) as Observable<T>;
  }

  patch<T>(path: string, body = {}, params?): Observable<T> {
    return this.http.patch(`${environment.backendEndpoint}${path}`, body) as Observable<T>;
  }

  delete<T>(path: string): Observable<T> {
    return this.http.delete(`${environment.backendEndpoint}${path}`) as Observable<T>;
  }
}
