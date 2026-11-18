
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from '../models/student';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private apiUrl = 'https://localhost:7248/api/Students';

  constructor(private http: HttpClient) { }

  addStudent(student: Student): Observable<any> {
    return this.http.post(this.apiUrl, student);
  }
}
