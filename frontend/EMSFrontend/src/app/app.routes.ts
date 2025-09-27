import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Departments } from './pages/departments/departments';
import { Employee } from './pages/employee/employee';
import { Login } from './pages/login/login';
import { EmployeeDashboard } from './pages/employee-dashboard/employee-dashboard';

export const routes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { 
        path: 'dashboard', component: Home,
       },
       { 
        path: 'employee-dashboard', component: EmployeeDashboard,
       },

       {
           path: 'departments', component: Departments
       },
       {
           path: 'employee', component: Employee
       },
       { path: 'login', component: Login }
   ];
