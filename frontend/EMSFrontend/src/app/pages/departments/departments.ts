import { Component, inject } from '@angular/core';
import { LucideAngularModule, Pencil, Trash2, LoaderCircle, X } from 'lucide-angular'; // Loader icon
import { FormsModule } from '@angular/forms';
import { HttpService } from '../../services/http';
import { IDepartment } from '../../types/IDepartment';

@Component({
  selector: 'app-departments',
  imports: [LucideAngularModule,FormsModule],
  templateUrl: './departments.html',
  styleUrl: './departments.css'
})
export class Departments {

  readonly Pencil = Pencil;
  readonly Trash2 = Trash2;
  readonly LoaderCircle = LoaderCircle;

  httpService = inject(HttpService);

  departments: IDepartment[] = [];
  loading:boolean = true; // track loading
    // modal state
  isModalOpen: boolean = false;
  editId: number = 0;
  DepartmentName: string = '';
  readonly X = X;

    openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
    this.DepartmentName = '';
    this.editId = 0;
  }


getDepartments() {
    this.httpService.getDepartments().subscribe({
      next: (result) => {
        this.departments = result;
        this.loading = false; // stop loader when data arrives
      },
      error: () => {
        this.loading = false; // stop loader on error too
      }
    });

}



  addDepartment() {
    console.log('New Department:', this.DepartmentName);
    this.httpService.addDepartment(this.DepartmentName).subscribe({
      next: () => {
        this.getDepartments(); // Refresh the list after adding
        alert('Department added successfully');
      },
      error: () => {
        alert('Failed to add department');
      }
    });

    this.closeModal();
  }

  editDepartment(department: IDepartment) {
    this.DepartmentName = department.name;
    this.editId = department.id;
    
    this.openModal();
    
    

  }
  updateDepartment(){
    this.httpService.updateDepartment(this.editId,this.DepartmentName).subscribe({
      next:()=>{
        this.getDepartments();
        alert('Department updated successfully');
        this.closeModal();
        this.editId = 0;
      },
      error:()=>{
        alert('Failed to update department');
      }
    });

  }

  deleteDepartment(id: number) {
    this.httpService.deleteDepartment(id).subscribe({
      next: () => {
        this.getDepartments();
        alert('Department deleted successfully');
      },
      error: () => {
        alert('Failed to delete department');
      }
    });
  }

  ngOnInit() {
    this.getDepartments();
  
  }
}
