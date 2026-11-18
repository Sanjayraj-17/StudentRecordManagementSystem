
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

export interface AuthResponse {
  token: string;
  userId: string;
  name: string;
  email: string;
  role: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'http://localhost:5219/api/auth';

  constructor(private http: HttpClient) { }

  // Login API
  login(email: string, password: string): Observable<AuthResponse> {

    return this.http.post<AuthResponse>(
      `${this.apiUrl}/login`,
      { email, password }
    ).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('user', JSON.stringify(response));
      })
    );
  }

  // Get stored JWT token
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Check whether token exists
  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  // Get logged-in user details
  getUser(): AuthResponse | null {
    const user = localStorage.getItem('user');

    return user ? JSON.parse(user) as AuthResponse : null;
  }

  // Logout
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }
}
