import {Component, OnInit} from '@angular/core';
import {DatePipe, NgForOf} from "@angular/common";
import {Project, ProjectService} from "../project.service";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-project-table',
  standalone: true,
  imports: [
    DatePipe,
    NgForOf,
    FormsModule
  ],
  templateUrl: './project-table.component.html',
  styleUrl: './project-table.component.scss'
})
export class ProjectTableComponent implements OnInit {
  projects: Project[] = [];
  currentPage: number = 1;
  totalPages: number = 1;
  pageSize: number = 10;
  sortBy: string = 'id';
  sortDirection: string = 'asc';

  constructor(private projectService: ProjectService ) {
  }

  ngOnInit(): void {
    this.loadProjects();
  }

  loadProjects(): void {
    this.projectService.getProjects(this.currentPage, this.pageSize, this.sortBy, this.sortDirection).subscribe({
      next: (response) => {
        this.projects = response.items;
        this.currentPage = response.currentPage;
        this.totalPages = response.totalPages;
        this.pageSize = response.pageSize;
        console.log('Projects loaded:', this.projects);
      },
      error: (error) => {
        console.error('Error loading projects:', error);
      }
    });
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadProjects();
  }

  onSortChange(sortBy: string, sortDirection: string): void {
    this.sortBy = sortBy;
    this.sortDirection = sortDirection;
    this.loadProjects();
  }

}

