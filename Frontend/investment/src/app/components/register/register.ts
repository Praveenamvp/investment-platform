import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  Validators,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  form!: ReturnType<FormBuilder['group']>;

  loading = false;
  error = '';
  showPassword = false;
  showConfirm = false;
  passwordStrength = '';

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
  ) {
    this.form = this.fb.group(
      {
        fullName: ['', [Validators.required, Validators.minLength(2)]],
        email: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d).+$/),
          ],
        ],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: this.passwordMatchValidator },
    );

    this.form.get('password')!.valueChanges.subscribe((val: string) => {
      this.passwordStrength = this.calcStrength(val ?? '');
    });
  }

  get fullName() {
    return this.form.get('fullName')!;
  }
  get email() {
    return this.form.get('email')!;
  }
  get password() {
    return this.form.get('password')!;
  }
  get confirmPassword() {
    return this.form.get('confirmPassword')!;
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.error = '';

    this.auth
      .login({
        email: this.email.value!,
        password: this.password.value!,
      })
      .subscribe({
        next: () => this.router.navigate(['/dashboard']),
        error: (err: any) => {
          this.loading = false;
          this.error = err?.error?.message || 'Registration failed. Please try again.';
        },
      });
  }

  private passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const pw = control.get('password');
    const cpw = control.get('confirmPassword');
    if (pw && cpw && pw.value !== cpw.value) {
      return { passwordMismatch: true };
    }
    return null;
  }

  private calcStrength(pw: string): string {
    if (!pw) return '';
    const hasLetter = /[A-Za-z]/.test(pw);
    const hasNumber = /\d/.test(pw);
    const hasSpecial = /[^A-Za-z0-9]/.test(pw);
    const long = pw.length >= 10;

    if (hasLetter && hasNumber && hasSpecial && long) return 'strong';
    if (hasLetter && hasNumber && pw.length >= 6) return 'medium';
    return 'weak';
  }
}
