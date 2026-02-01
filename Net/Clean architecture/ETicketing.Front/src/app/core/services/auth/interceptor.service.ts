import { HttpInterceptorFn, HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ApiService } from '../api.service';

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn) => {

  const auth = inject(AuthService);
  const router = inject(Router);
  const api = inject(ApiService);

  if (!req.url.startsWith(api.baseUrl)) { return next(req); }

  const token = auth.getAccessToken();

  let cloned = req;
  if (token) {
    cloned = req.clone({
      setHeaders: {
        Authorization: 'Bearer ' + token
      }
    });
  }

  return new Observable(observer => {

    next(cloned).subscribe({
      next: event => observer.next(event),

      error: err => {
        if (err.status === 401 && auth.getRefreshToken()) {

          auth.refreshToken().subscribe({
            next: refreshRes => {
              auth.saveTokens(refreshRes.data.accessToken, refreshRes.data.refreshToken, refreshRes.data.accessTokenExpires);

              const retryReq = req.clone({
                setHeaders: {
                  Authorization: 'Bearer ' + refreshRes.data.accessToken
                }
              });

              next(retryReq).subscribe({
                next: e => observer.next(e),
                error: e => observer.error(e),
                complete: () => observer.complete()
              });
            },

            error: _ => {
              auth.logout();
              observer.error(err);
            }
          });

        } else {
          observer.error(err);
        }
      },

      complete: () => observer.complete()
    });
  });
};
