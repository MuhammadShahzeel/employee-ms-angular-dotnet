import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CalendarCheck2, CheckCircle, CircleCheck, FilePlus2, LucideAngularModule, X } from 'lucide-angular';
import { Leave } from '../../services/leave';
import { ApplyLeave } from '../../types/ILeave';

@Component({
  selector: 'app-employee-dashboard',
  imports: [ReactiveFormsModule, LucideAngularModule],
  templateUrl: './employee-dashboard.html',
  styleUrl: './employee-dashboard.css'
})
export class EmployeeDashboard {

  readonly X = X;
  readonly FilePlus2 = FilePlus2;

  readonly CircleCheck = CircleCheck;

  fb = inject(FormBuilder);
  leaveService = inject(Leave);

  isModalOpen = false;
  leaveForm!: FormGroup;

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
    this.leaveForm.reset();
  }

  ngOnInit() {
    this.leaveForm = this.fb.group({
      type: [null, Validators.required],
      reason: ['', [Validators.required, Validators.minLength(5)]],
      leaveDate: ['', Validators.required],
    });
  }

  applyLeave() {
    if (this.leaveForm.invalid) return;

    const leaveData: ApplyLeave = this.leaveForm.value;

    this.leaveService.applyLeave(leaveData).subscribe({
      next: () => {
        alert('Leave applied successfully!');
        this.closeModal();
      },
      error: () => {
        alert('Failed to apply leave.');
      },
    });
  }

  markAttendance() {
    this.leaveService.markAttendance().subscribe({
      next: (res:any) => {
        alert(res.message);
      },
      error: (err) => {
       if(err.error && err.error.message){
        alert(err.error.message);
       } else{
        alert('Failed to mark attendance.');
       }
      },
    });
  }
}

   
