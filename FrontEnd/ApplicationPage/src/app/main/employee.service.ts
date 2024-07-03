import { Injectable } from '@angular/core';
import {LocalStorageService} from "../local-storage.service";
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {ApprovalRequestResponse} from "./approve-request.service";
import {Employee} from "./leave-request.service";

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private api = 'http://localhost:5151/list/employees';

  constructor(
    private storageService: LocalStorageService,
    private http: HttpClient
  ) { }

  public getEmployees(page: number, pageSize: number, sortBy: string, sortDirection: string): Observable<EmployeeResponse>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('sortBy', sortBy)
      .set('sortDirection', sortDirection);
    return this.http.get<EmployeeResponse>(this.api, {headers, params}) ;
  }

}

export interface EmployeeResponse {
  currentPage: number;
  totalPages: number;
  pageSize: number;
  totalCount: number;
  items: Employee[];
}
