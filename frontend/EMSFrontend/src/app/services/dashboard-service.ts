import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { IDashboard } from '../types/IDashboard';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  http = inject(HttpClient);

   getDashboardData(){
      return this.http.get<IDashboard>(`${environment.apiUrl}/api/Dashboard`);
    }
  
}
