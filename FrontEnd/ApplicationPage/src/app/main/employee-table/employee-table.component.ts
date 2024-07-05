import {Component, OnInit} from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {Employee, EmployeeService, RegisterEmployee, UpdateEmployee} from "../employee.service";
import {JwtParserService} from "../../jwt-parser.service";
import {Position, Role, Status, StatusesService, Subdivision} from "../statuses.service";

@Component({
  selector: 'app-employee-table',
  standalone: true,
  imports: [
    NgForOf,
    ReactiveFormsModule,
    FormsModule,
    NgIf
  ],
  templateUrl: './employee-table.component.html',
  styleUrl: './employee-table.component.scss'
})
export class EmployeeTableComponent implements OnInit{
  employees: Employee[] = [];
  originalEmployees: { [id: number]: Employee } = {};

  currentPage: number = 1;
  totalPages: number = 1;
  pageSize: number = 10;
  sortBy: string = 'id';
  sortDirection: string = 'asc';

  subdivisions: Subdivision[] = [];
  positions: Position[] = [];
  roles: Role[] = [];
  employeeStatuses: Status[] = [];

  newEmployee: RegisterEmployee = {
    fullName: '',
    login: '',
    outOfOfficeBalance: 0,
    subdivisionId: 0,
    positionId: 0,
    statusId: 0,
    rolesId: [],
    password: ''
  };

  constructor(
    private employeeService: EmployeeService,
    private statusesService: StatusesService,
    private jwtParser: JwtParserService
    ) {}

  ngOnInit(): void {
    this.loadEmployees();
    this.loadAdditionalData();
  }

  private loadAdditionalData() {
    this.statusesService.getEmployeeStatuses()
      .subscribe({
        next: (response:Status[]) => {
          this.employeeStatuses = response;
        }
      });

    this.statusesService.getRoles()
      .subscribe({
        next: (response:Role[]) => {
          this.roles = response;
        }
      });

    this.statusesService.getPositions()
      .subscribe({
        next: (response:Position[]) => {
          this.positions = response;
        }
      });

    this.statusesService.getSubdivisions()
      .subscribe(
        (response:Subdivision[]) => {
          this.subdivisions = response;
        }
      )
  }

  loadEmployees(): void {
    this.employeeService.getEmployees(this.currentPage, this.pageSize, this.sortBy, this.sortDirection).subscribe({
      next: (response) => {
        this.employees = response.items;
        this.currentPage = response.currentPage;
        this.totalPages = response.totalPages;
        this.pageSize = response.pageSize;

        this.employees.forEach(employee => {
          this.originalEmployees[employee.id] = {...employee};
        })
      },
      error: (error) => {
        console.error('Error loading projects:', error);
      }
    });
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadEmployees();
  }

  onSortChange(sortBy: string, sortDirection: string): void {
    this.sortBy = sortBy;
    this.sortDirection = sortDirection;
    this.loadEmployees();
  }

  hasRole(roles: string[]): boolean {
    return <boolean>roles.some(role => this.jwtParser.hasRole(role));
  }

  onCreate(){
    this.employeeService.createEmployee(this.newEmployee)
      .subscribe({
        next: () => {
          this.loadEmployees();
        }
      })
  }

  onUpdate(employee: Employee) {
    employee.isEditing = true;
    employee.updateData = {
      id: employee.id,
      fullName: employee.fullName,
      outOfOfficeBalance: employee.outOfOfficeBalance,
      partnerId: employee.partnerId,
      positionId: this.positions.find(p => p.position === employee.position)?.id ?? null, // Provide default value
      rolesId: employee.roles.map(role => this.roles.find(r => r.role === role)?.id).filter(id => id !== undefined) as number[],
      statusId: this.employeeStatuses.find(s => s.status === employee.status)?.id ?? null, // Provide default value
      subdivisionId: this.subdivisions.find(s => s.subdivision === employee.subdivision)?.id ?? null // Provide default value
    };
  }


  onDelete(employee: Employee) {
    this.employeeService.deleteEmployee(employee.id).subscribe({
      next: (response: void) => {
        this.employees = this.employees.filter(emp => emp.id !== employee.id);
        this.employees.forEach(employee => {
          this.originalEmployees[employee.id] = {...employee};
        });
      }
    })
  }

  onSubmit(employee: Employee){
    console.log(employee)
    this.employeeService.updateEmployee(<UpdateEmployee>employee.updateData).subscribe(
      {
        next: (response) => {
          employee.isEditing = false;
          employee.fullName = response.fullName;
          employee.outOfOfficeBalance = response.outOfOfficeBalance;
          employee.subdivision= response.subdivision;
          employee.partnerId = response.partnerId;
          employee.position=response.position;
          employee.roles=response.roles;
          this.originalEmployees[employee.id] = {...employee};
        },
        error: err => {
          this.onCancel(employee);
          console.log(err);
        }
      }
    );
  }

  onCancel(employee: Employee){
    const original = this.originalEmployees[employee.id];
    employee.fullName = original.fullName;
    employee.outOfOfficeBalance = original.outOfOfficeBalance;
    employee.subdivision = original.subdivision;
    employee.position = original.position;
    employee.status = original.status;
    employee.roles = original.roles;
    employee.isEditing = false;
  }


}
