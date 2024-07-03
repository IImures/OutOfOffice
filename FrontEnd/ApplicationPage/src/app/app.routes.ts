import { Routes } from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {MainComponent} from "./main/main.component";
import {RoleGuard} from "./role-guard.service";
import {ProjectTableComponent} from "./main/project-table/project-table.component";
import {LeaveRequestComponent} from "./main/leave-request/leave-request.component";
import {ApproveRequestComponent} from "./main/approve-request/approve-request.component";
import {EmployeeTableComponent} from "./main/employee-table/employee-table.component";

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/main',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'main',
    component: MainComponent,
    canActivate: [RoleGuard],
    data: {roles: ['PM', 'HR', 'EMP']},
    children: [
      {
        path: 'projects',
        component: ProjectTableComponent,
        canActivate: [RoleGuard],
        data: {roles: ['PM', 'HR', 'EMP']}
      },
      {
        path: 'leave-requests',
        component: LeaveRequestComponent,
        canActivate: [RoleGuard],
        data: {roles: ['PM', 'HR', 'EMP']}
      },
      {
        path: 'approval-requests',
        component: ApproveRequestComponent,
        canActivate: [RoleGuard],
        data: {roles: ['PM', 'HR']}
      },
      {
        path: 'employees',
        component: EmployeeTableComponent,
        canActivate: [RoleGuard],
        data: {roles: ['PM', 'HR']}
      }
    ]
  },

];
