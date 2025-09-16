import { Component, inject, OnInit } from '@angular/core';
import { HttpService } from '../../services/http';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { LucideAngularModule, Pencil, Trash2, LoaderCircle, X } from 'lucide-angular'; // Loader icon

import { IDepartment } from '../../types/IDepartment';
import { IEmployee } from '../../types/IEmployee';

@Component({
  selector: 'app-employee',
  imports: [LucideAngularModule,ReactiveFormsModule],
  templateUrl: './employee.html',
  styleUrl: './employee.css'
})
export class Employee implements OnInit {
readonly Pencil = Pencil;
  readonly Trash2 = Trash2;
  readonly LoaderCircle = LoaderCircle;

  httpService = inject(HttpService);
  

  departments: IDepartment[] = [];
  fb = inject(FormBuilder);
    employeeForm!: FormGroup;

  employees: IEmployee[] = [];
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
    this.employeeForm.reset({
      gender: null, // reset default
      departmentId: null, // reset default
      jobTitle: null // reset default
    });
    this.editId = 0;
  }


getEmployees() {
    this.httpService.getEmployees().subscribe({
      next: (result) => {
        this.employees = result;
        this.loading = false; // stop loader when data arrives
      },
      error: () => {
        this.loading = false; // stop loader on error too
      }
    });

}

  addEmployee() {
    console.log('New Employee:', this.employeeForm.value);
    this.httpService.addEmployee(this.employeeForm.value).subscribe({
      next: () => {
        this.getEmployees(); // Refresh the list after adding
        alert('Employee added successfully');
      },
      error: () => {
        alert('Failed to add employee');
      }
    });

    this.closeModal();
  }

  editEmployee(employee: IEmployee) {
    // this.DepartmentName = department.name;
    // this.editId = department.id;
    
    // this.openModal();
    
    

  }
  updateEmployee(){
    // this.httpService.updateDepartment(this.editId,this.DepartmentName).subscribe({
    //   next:()=>{
    //     this.getEmployees();
    //     alert('Department updated successfully');
    //     this.closeModal();
    //     this.editId = 0;
    //   },
    //   error:()=>{
    //     alert('Failed to update department');
    //   }
    // });

  }
  getDepartments() {
    this.httpService.getDepartments().subscribe({
      next: (result) => {
        this.departments = result;
     
      },
      error: () => {
        alert('Failed to fetch departments');
    
      }
    });

}

  deleteEmployee(id: number) {
    // this.httpService.deleteDepartment(id).subscribe({
    //   next: () => {
    //     this.getEmployees();
    //     alert('Department deleted successfully');
    //   },
    //   error: () => {
    //     alert('Failed to delete department');
    //   }
    // });
  }

  ngOnInit() {
    this.getEmployees();
    this.getDepartments();

  this.employeeForm = this.fb.group({
    id: [0],
      name: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^[0-9]{10,15}$/)]],
      gender: [null, Validators.required],
      jobTitle: [null, Validators.required],
      departmentId: [null, Validators.required],
      joiningDate: ['', Validators.required],
      lastWorkingDate: [''], // optional
      dateOfBirth: ['', Validators.required],
    });
  
  }

}
