import { Injectable } from '@angular/core';
import {LocalStorageService} from "../local-storage.service";
import {JwtParserService} from "../jwt-parser.service";
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Employee} from "./leave-request.service";

@Injectable({
  providedIn: 'root'
})
export class ApproveRequestService {
  private api = 'http://localhost:5151/list/approval-requests';


  constructor(
    private storageService: LocalStorageService,
    private http: HttpClient) { }

  public getRequests(page: number, pageSize: number, sortBy: string, sortDirection: string): Observable<ApprovalRequestResponse> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('sortBy', sortBy)
      .set('sortDirection', sortDirection);
    return this.http.get<ApprovalRequestResponse>(this.api, {headers: headers, params});
  }
}

export interface ApprovalRequestItem {
  id: number;
  leaveRequestId: number;
  comment: string | null;
  status: string;
  employee: Employee;
}

export interface  ApprovalRequestResponse {
  currentPage: number;
  totalPages: number;
  pageSize: number;
  totalCount: number;
  items: ApprovalRequestItem[];
}
