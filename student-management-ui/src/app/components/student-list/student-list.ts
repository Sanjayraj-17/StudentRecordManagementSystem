
import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Student } from '../../models/student';

@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './student-list.html',
  styleUrl: './student-list.css'
})
export class StudentListComponent implements OnInit {

  students = signal<Student[]>([]);

  errorMessage = '';

  private apiUrl = 'https://localhost:7248/api/Students';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.loadStudents();
  }

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
}
