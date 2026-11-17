
import { Component } from '@angular/core';

import { AddStudentComponent } from './components/add-student/add-student';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ AddStudentComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
}
