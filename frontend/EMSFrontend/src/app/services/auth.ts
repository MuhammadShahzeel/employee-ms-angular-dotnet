import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { IAuthCredentials } from '../types/IAuthCredentials';
import { IAuthToken } from '../types/IAuthToken';


@Injectable({
  providedIn: 'root'
})
export class Auth {
    http = inject(HttpClient);
    Login(credentials: IAuthCredentials) {
      return this.http.post<IAuthToken>(`${environment.apiUrl}/api/Auth/login`, credentials);
    }
  
}
  