import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {JwtParserService} from "../jwt-parser.service";
import {LocalStorageService} from "../local-storage.service";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class LeaveRequestService {
  private api = 'http://localhost:5151/list/leave-requests';

  constructor(
    private http: HttpClient,
    private jwrParser: JwtParserService,
    private storage: LocalStorageService,
  ) { }

  public getLeaveRequests(page: number, pageSize: number, sortBy: string, sortDirection: string): Observable<LeaveResponse>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storage.getItem('token')}`);
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('sortBy', sortBy)
      .set('sortDirection', sortDirection)
    if(this.jwrParser.hasRole('EMP')){
      return this.http.get<LeaveResponse>(this.api)
    }else {
      return this.http.get<LeaveResponse>(this.api, {headers: headers, params});
    }
  }

}

export interface LeaveResponse {
  currentPage: number;
  totalPages: number;
  pageSize: number;
  totalCount: number;
  items: LeaveItem[];
}

export interface LeaveItem {
  id: number;
  startDate: string;
  endDate: string;
  comment: string | null;
  status: string;
  absenceReason: string;
  employee: Employee;
}

export interface Employee {
  id: number;
  fullName: string;
  outOfOfficeBalance: number;
  subdivision: string;
  position: string;
  status: string;
  roles: string[];
  partnerId: number | null;
}

