import { Injectable, signal } from '@angular/core';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private loggedIn = signal(false);
  private userName = signal('');

  isLoggedIn(): boolean {
    return this.loggedIn();
  }

  getUserName(): string {
    return this.userName();
  }

  login(credentials: { email: string; password: string }): Observable<any> {
    return of({ token: 'mock-token', name: 'Investor' }).pipe(
      tap((res) => {
        this.loggedIn.set(true);
        this.userName.set(res.name);
      }),
    );
  }

  logout(): void {
    this.loggedIn.set(false);
    this.userName.set('');
  }
}
