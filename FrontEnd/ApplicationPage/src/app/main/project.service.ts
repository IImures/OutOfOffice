import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {JwtParserService} from "../jwt-parser.service";
import {Observable} from "rxjs";
import {LocalStorageService} from "../local-storage.service";

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private api = 'http://localhost:5151/list/projects';

  constructor(
    private storageService: LocalStorageService,
    private jwtParser: JwtParserService,
    private http: HttpClient) { }

  public   getProjects(page: number, pageSize: number, sortBy: string, sortDirection: string): Observable<ProjectResponse> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.storageService.getItem('token')}`);
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('sortBy', sortBy)
      .set('sortDirection', sortDirection);
    if(this.jwtParser.hasRole("EMP")){
      return this.http.get<ProjectResponse>(this.api + '/employees', {headers: headers, params});
    }else{
      return this.http.get<ProjectResponse>(this.api, {headers: headers, params});
    }
  }
}

interface ProjectResponse {
  currentPage: number;
  totalPages: number;
  pageSize: number;
  totalCount: number;
  items: Project[];
}

export interface Project {
  id: number;
  projectStatus: string;
  projectType: string;
  startDate: Date;
  endDate?: Date | null;
  projectManager: string;
  comment?: string | null;
  employees: string[];
}
