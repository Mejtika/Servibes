import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanLoad, Route, UrlSegment } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorizeService } from './authorize.service';
import { tap, map, mergeMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PermissionGuard implements CanLoad {
  constructor(private authorize: AuthorizeService, private router: Router) {
  }

  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
    console.log(segments);
    return this.authorize.havePermissions(route.path)
      .pipe(tap(isAuthenticated => this.handleAuthorization(isAuthenticated, route)));
  }

  private handleAuthorization(hasRequiredPermissions: boolean, route: Route) {
    if (!hasRequiredPermissions) {
      this.router.navigateByUrl("/");
    }
  }
}
