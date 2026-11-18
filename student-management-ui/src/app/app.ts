
import { Component } from '@angular/core';
import { AddStudentComponent } from './components/add-student/add-student';
import { StudentListComponent } from './components/student-list/student-list';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    AddStudentComponent,
    StudentListComponent
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App { }
