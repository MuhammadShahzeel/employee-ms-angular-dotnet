import { Component, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { LucideAngularModule, TextAlignJustify,LayoutDashboard, Building2,Users } from 'lucide-angular';


@Component({
  selector: 'app-root',

  imports: [RouterOutlet, RouterLink, LucideAngularModule, RouterLinkActive],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('EMS');
    readonly TextAlignJustify = TextAlignJustify;
    readonly LayoutDashboard = LayoutDashboard;
    readonly Building2 = Building2;
    readonly Users = Users;

     isSidebarOpen = true;

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }
}
