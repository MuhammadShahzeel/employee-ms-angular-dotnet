import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { IAuthCredentials } from '../types/IAuthCredentials';
import { IAuthToken } from '../types/IAuthToken';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class Auth {
    http = inject(HttpClient);
    router = inject(Router);
    
    Login(credentials: IAuthCredentials) {
      return this.http.post<IAuthToken>(`${environment.apiUrl}/api/Auth/login`, credentials);
    
    
    }
    saveToken(authToken: IAuthToken) {
  localStorage.setItem("auth", JSON.stringify(authToken));
  localStorage.setItem("token", (authToken.token));
}
get isLoggedIn() {
  return localStorage.getItem('token') ? true : false;
}
get isEmployee() {
  return JSON.parse(localStorage.getItem('auth')!)?.role === 'Employee';
}


logout() {
  localStorage.removeItem('token');
  localStorage.removeItem('auth');
  this.router.navigate(['/login']);
}
}
  