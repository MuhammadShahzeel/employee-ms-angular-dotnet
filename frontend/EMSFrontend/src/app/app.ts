import { Component, inject, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { LucideAngularModule, TextAlignJustify,LayoutDashboard, Building2,Users,  LogInIcon, LogOutIcon, User, CalendarDays, CalendarDaysIcon, History } from 'lucide-angular';
import { Auth } from './services/auth';


@Component({
  selector: 'app-root',

  imports: [RouterOutlet, RouterLink, LucideAngularModule, RouterLinkActive],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('EMS');
    readonly TextAlignJustify = TextAlignJustify;
    readonly User = User;

    
    readonly LayoutDashboard = LayoutDashboard;
    readonly Building2 = Building2;
    readonly Users = Users;
    readonly LogIn = LogInIcon; 
    readonly LogOut = LogOutIcon;
    readonly Leaves = CalendarDays;
    readonly History = History;
    authService = inject(Auth);

     isSidebarOpen = true;

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }
  logout() {
    this.authService.logout();}

    
    
}
