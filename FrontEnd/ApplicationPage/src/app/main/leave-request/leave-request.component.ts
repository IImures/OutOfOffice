import {Component, OnInit} from '@angular/core';
import {DatePipe, NgForOf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {LeaveItem, LeaveRequestService, LeaveResponse} from "../leave-request.service";
import {ProjectService} from "../project.service";

@Component({
  selector: 'app-leave-request',
  standalone: true,
    imports: [
        DatePipe,
        FormsModule,
        NgForOf
    ],
  templateUrl: './leave-request.component.html',
  styleUrl: './leave-request.component.scss'
})
export class LeaveRequestComponent implements OnInit {
  leaveRequests: LeaveItem[] = [];
  currentPage: number = 1;
  totalPages: number = 1;
  pageSize: number = 10;
  sortBy: string = 'id';
  sortDirection: string = 'asc';

  constructor(
    private leaveService: LeaveRequestService,
    ) {}

  ngOnInit(): void {
    this.loadRequests();
  }

  loadRequests(): void {
    this.leaveService.getLeaveRequests(this.currentPage, this.pageSize, this.sortBy, this.sortDirection).subscribe({
      next: (response) => {
        this.leaveRequests = response.items;
        this.currentPage = response.currentPage;
        this.totalPages = response.totalPages;
        this.pageSize = response.pageSize;
      },
      error: (error) => {
        console.error('Error loading projects:', error);
      }
    });
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadRequests();
  }

  onSortChange(sortBy: string, sortDirection: string): void {
    this.sortBy = sortBy;
    this.sortDirection = sortDirection;
    this.loadRequests();
  }
}
