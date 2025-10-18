import { Component, inject, OnInit } from '@angular/core';
import { LucideAngularModule, Users, Building2, Wallet, CalendarDays } from 'lucide-angular';
import { DashboardService } from '../../services/dashboard-service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [LucideAngularModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {

  totalSalary!: number
employeeCount!: number
departmentCount!: number
  // icons
  readonly Users = Users;
  readonly Building2 = Building2;
 readonly  Wallet = Wallet;
 readonly CalendarDays  = CalendarDays;

 dashboardService = inject(DashboardService)

  ngOnInit(): void {
     this.getDashboardData(); 

  }



  getDashboardData() {
   

 
    this.dashboardService.getDashboardData().subscribe({
      next: (result) => {

        this.totalSalary = result.totalSalary
        this.employeeCount = result.employeeCount
        this.departmentCount = result.departmentCount
      },
      error: () => {
        
        alert('Failed to fetch dashboard data ');
      }
    });
  }

}
