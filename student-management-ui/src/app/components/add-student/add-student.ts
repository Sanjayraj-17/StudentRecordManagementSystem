
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Student } from '../../models/student';
import { StudentService } from '../../services/student';

@Component({
  selector: 'app-add-student',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-student.html',
  styleUrl: './add-student.css'
})
export class AddStudentComponent {

  student: Student = {
    name: '',
    email: '',
    department: '',
    age: 18
  };

  message = '';
  errorMessage = '';

  constructor(private studentService: StudentService) { }

  addStudent(): void {
    this.message = '';
    this.errorMessage = '';

    this.studentService.addStudent(this.student).subscribe({
      next: (response) => {
        this.message = response.message;
        this.student = {
          name: '',
          email: '',
          department: '',
          age: 18
        };
      },
      error: (error) => {
        console.error(error);
        this.errorMessage = 'Unable to add student. Please try again.';
      }
    });
  }
}
