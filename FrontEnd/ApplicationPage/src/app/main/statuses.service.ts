import { Injectable } from '@angular/core';
import {LocalStorageService} from "../local-storage.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class StatusesService {

  private api = 'http://localhost:5151/list';


  constructor(
    private storageService: LocalStorageService,
    private http: HttpClient
  ) { }

  getEmployeeStatuses(): Observable<Status[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    return  this.http.get<Status[]>(`${this.api}/statuses/employees/`, {headers: headers})
  }

  getLeaveRequestsStatuses(): Observable<Status[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    return this.http.get<Status[]>(`${this.api}/statuses/leave-requests/`, {headers: headers})
  }

  getApprovalRequestsStatuses(): Observable<Status[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    return this.http.get<Status[]>(`${this.api}/statuses/approval-requests/`, {headers: headers})
  }

  getProjectStatuses(): Observable<Status[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    return this.http.get<Status[]>(`${this.api}/statuses/project-statuses/`, {headers: headers})
  }

  getProjectTypes(): Observable<Type[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    return this.http.get<Type[]>(`${this.api}/statuses/project-types/`, {headers: headers})
  }

  getRoles(): Observable<Role[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    return this.http.get<Role[]>(`${this.api}/roles`, {headers: headers})
  }

  getSubdivisions(): Observable<Subdivision[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    return this.http.get<Subdivision[]>(`${this.api}/subdivisions`, {headers: headers})
  }

  getPositions(): Observable<Position[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    return this.http.get<Position[]>(`${this.api}/positions`, {headers: headers})
  }
}


export interface Status{
  id: number;
  status: string;
}

export interface Type{
  id: number;
  type: string;
}

export interface Position{
  id: number;
  position: string;
}

export interface Subdivision{
  id: number;
  subdivision: string;
}

export interface Role{
  id:number;
  role:string;
}
