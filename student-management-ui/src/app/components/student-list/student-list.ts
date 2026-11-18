
import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Student } from '../../models/student';

@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './student-list.html',
  styleUrl: './student-list.css'
})
export class StudentListComponent implements OnInit {

  // Store all student records
  students = signal<Student[]>([]);

  // Error message
  errorMessage = '';

  // Store selected student ID
  selectedStudentId: number | null = null;

  // Store student details for editing
  editStudentData: Student = {
    name: '',
    email: '',
    department: '',
    age: 18
  };

  // Backend API URL
  private apiUrl = 'https://localhost:7248/api/Students';

  constructor(private http: HttpClient) { }

  // Load students when component starts
  ngOnInit(): void {
    this.loadStudents();
  }

  // GET - View all students
  loadStudents(): void {
    this.http.get<Student[]>(this.apiUrl).subscribe({
      next: (data) => {
        this.students.set(data);
        console.log('Students array:', data);
      },
      error: (error) => {
        console.error('API Error:', error);
        this.errorMessage = 'Unable to load students.';
      }
    });
  }

  // EDIT - Load selected student details
  editStudent(student: Student): void {
    this.selectedStudentId = student.studentId!;

    this.editStudentData = {
      name: student.name,
      email: student.email,
      department: student.department,
      age: student.age
    };
  }

  // UPDATE - Save modified student details
  updateStudent(): void {

    if (this.selectedStudentId === null) {
      return;
    }

    this.http.put(
      `${this.apiUrl}/${this.selectedStudentId}`,
      this.editStudentData
    ).subscribe({

      next: () => {
        this.students.update(students =>
          students.map(student =>
            student.studentId === this.selectedStudentId
              ? { ...student, ...this.editStudentData }
              : student
          )
        );

        this.selectedStudentId = null;

        alert('Student updated successfully!');
      },

      error: (error) => {
        console.error('Update Error:', error);
        this.errorMessage = 'Unable to update student.';
      }

    });
  }

  // CANCEL - Stop editing
  cancelEdit(): void {
    this.selectedStudentId = null;

    this.editStudentData = {
      name: '',
      email: '',
      department: '',
      age: 18
    };
  }

  // DELETE - Remove student
  deleteStudent(id: number): void {

    if (confirm('Are you sure you want to delete this student?')) {

      this.http.delete(`${this.apiUrl}/${id}`).subscribe({

        next: () => {
          this.students.update(students =>
            students.filter(student => student.studentId !== id)
          );

          alert('Student deleted successfully!');
        },

        error: (error) => {
          console.error('Delete Error:', error);
          this.errorMessage = 'Unable to delete student.';
        }

      });

    }
  }
}
