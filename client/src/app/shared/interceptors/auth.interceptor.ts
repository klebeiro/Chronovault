import { HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { inject } from "@angular/core";

import { AuthService } from "../../services";


export function authInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
  const authCredentials = inject(AuthService).getAuthCredentials();

  if (!authCredentials) {
    return next(req);
  }

  const newReq = req.clone({
    headers: req.headers.append('Authorization', `Bearer ${authCredentials.token}`),
  });

  return next(newReq);
}