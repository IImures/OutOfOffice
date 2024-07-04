import {Component, OnInit} from '@angular/core';
import {DatePipe, NgForOf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {ApprovalRequestItem, ApprovalRequestResponse, ApproveRequestService} from "../approve-request.service";
import {JwtParserService} from "../../jwt-parser.service";

@Component({
  selector: 'app-approve-request',
  standalone: true,
    imports: [
        DatePipe,
        FormsModule,
        NgForOf
    ],
  templateUrl: './approve-request.component.html',
  styleUrl: './approve-request.component.scss'
})
export class ApproveRequestComponent implements OnInit{
  approveRequests: ApprovalRequestItem[] = [];
  currentPage: number = 1;
  totalPages: number = 1;
  pageSize: number = 10;
  sortBy: string = 'id';
  sortDirection: string = 'asc';

  constructor(
    private approveService: ApproveRequestService,
    private jwtParser : JwtParserService
  ) {}

  ngOnInit(): void {
    this.loadApproveRequests();
  }

  loadApproveRequests(): void {
    this.approveService.getRequests(this.currentPage, this.pageSize, this.sortBy, this.sortDirection).subscribe({
      next: (response) => {
        this.approveRequests = response.items;
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
    this.loadApproveRequests();
  }

  onSortChange(sortBy: string, sortDirection: string): void {
    this.sortBy = sortBy;
    this.sortDirection = sortDirection;
    this.loadApproveRequests();
  }

  hasRole(roles: string[]): boolean {
    return <boolean>roles.some(role => this.jwtParser.hasRole(role));
  }
}
