import { Injectable } from '@angular/core';
import {LocalStorageService} from "../local-storage.service";
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

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

  updateEmployee(updateData: UpdateEmployee) : Observable<Employee> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    return this.http.put<Employee>(`${this.api}/${updateData.id}`, updateData,{headers: headers}  );
  }

  deleteEmployee(id: number) : Observable<void> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    return this.http.delete<void>(`${this.api}/${id}`, {headers: headers})
  }

  createEmployee(employee: RegisterEmployee): Observable<void> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    return this.http.post<void>(`${this.api}/`, employee, {headers: headers}  );
  }
}

export interface RegisterEmployee {
  fullName: string;
  login: string;
  outOfOfficeBalance: number;
  subdivisionId: number;
  positionId: number;
  statusId: number;
  peoplePartnerId?: number;
  rolesId: number[];
  password: string;
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
  isEditing?: boolean;
  updateData: UpdateEmployee;
}

export interface UpdateEmployee{
  id: number;
  fullName: string;
  outOfOfficeBalance: number;
  partnerId: number | null;
  positionId: number |null;
  rolesId: number[] | null;
  statusId: number | null;
  subdivisionId: number | null;
}

export interface EmployeeResponse {
  currentPage: number;
  totalPages: number;
  pageSize: number;
  totalCount: number;
  items: Employee[];
}
