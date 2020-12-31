import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { ProfileService } from '../business-services/profile-service';

@Injectable()
export class BusinessRegisterGuard implements CanActivate {
    constructor(private profileService: ProfileService,
        private router: Router) {

    }

    canActivate(
        _next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
            
        return this.profileService.hasProfile().pipe(
          tap(result => this.handleAuthorization(result, state)),
          map(result => !result),
          tap(result => console.log('result', result)));
    }

    private handleAuthorization(hasCompany: boolean, state: RouterStateSnapshot) {
      if (hasCompany) {
        console.log('hascompany', hasCompany);
        this.router.navigateByUrl('business/appointments');
      }
    }
}
