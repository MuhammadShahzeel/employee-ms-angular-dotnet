
import { Component, inject, OnInit } from '@angular/core';
import { LucideAngularModule, LoaderCircle, X } from 'lucide-angular';

import { ILeave, LeaveStatus, LeaveType } from '../../types/ILeave';

import { Leave } from '../../services/leave';
import { Auth } from '../../services/auth';


@Component({
  selector: 'app-leaves',
  imports: [LucideAngularModule],
  templateUrl: './leaves.html',
  styleUrl: './leaves.css'
})
export class Leaves implements OnInit {

  readonly LoaderCircle = LoaderCircle;
  readonly X = X;
  Math = Math;
  readonly LeaveStatus = LeaveStatus;

  leaveService = inject(Leave);
  authService = inject(Auth);
  

  // state
  leaves: ILeave[] = [];
  loading: boolean = true;

  // pagination
  filter: any = {};
  pageIndex: number = 0;
  pageSize: number = 5;
  totalCount: number = 0;

  ngOnInit() {
    this.getLeaves();
  }

  getLeaves() {
    this.filter.pageIndex = this.pageIndex;
    this.filter.pageSize = this.pageSize;
    this.leaveService.getLeaves(this.filter).subscribe({
      next: (result) => {
        this.leaves = result.data;
        this.totalCount = result.totalCount;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        alert('Failed to fetch leaves');
      }
    });
  }

  onPageChange(newPage: number) {
    this.pageIndex = newPage;
    this.getLeaves();
  }

// leave type and status mapping using enums
  getLeaveType(type: LeaveType): string {
    return LeaveType[type] || 'Unknown';
  }

  getStatusText(status: LeaveStatus): string {
    return LeaveStatus[status] || 'Unknown';
  }
}
