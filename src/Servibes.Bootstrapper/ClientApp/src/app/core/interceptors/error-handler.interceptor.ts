import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerInterceptor implements HttpInterceptor {

  constructor(private toastr: ToastrService) {

  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((errorResponse: HttpErrorResponse) => {
        console.dir(errorResponse);
        if (errorResponse.status === 400) {
          const commandValidationMessage = `${errorResponse.error.detail}: ${errorResponse.error.errors}`;
          this.toastr.error(commandValidationMessage);

        } else {
          const businessRuleMessage = `${errorResponse.error.title}: ${errorResponse.error.detail}`;
          this.toastr.error(businessRuleMessage);
        }
        return throwError(errorResponse);
      })
    )
  }

}
