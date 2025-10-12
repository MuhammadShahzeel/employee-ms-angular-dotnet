import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApplyLeave, ILeave } from '../types/ILeave';
import { environment } from '../../environments/environment';
import { IPagedData } from '../types/IPagedData';

@Injectable({
  providedIn: 'root'
})
export class Leave {
  http = inject(HttpClient);
  applyLeave(leave: ApplyLeave) {
    return this.http.post(`${environment.apiUrl}/api/Leave/apply`, leave);
  }

  getLeaves(filter: any) {
  let params = new HttpParams({ fromObject: filter });
  return this.http.get<IPagedData<ILeave>>(`${environment.apiUrl}/api/Leave`, { params });
}

}
