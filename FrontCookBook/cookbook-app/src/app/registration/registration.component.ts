/*import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
//import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { User } from '../user.model';
import { UserService } from '../user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  registrationForm!: FormGroup;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    //private authService: AuthService,
    private router: Router
  ) {
    this.registrationForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
    });
  }

  register(): void {
    this.submitted = true;

    if (this.registrationForm.valid) {
      const user: User = {
        id: 0,
        name: this.registrationForm.get('name')?.value,
        email: this.registrationForm.get('email')?.value,
        password: this.registrationForm.get('password')?.value,
        dateOfBirth: this.registrationForm.get('dateOfBirth')?.value,
      };

      this.userService.createUser(user).subscribe(
        () => {
          this.router.navigate(['/login']);
        },
        (error) => {
          console.error(error);
          // Выведите сообщение об ошибке пользователю
        }
      );
    }
  }
}*/
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
//import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { User } from '../user.model';
import { UserService } from '../user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  registrationForm!: FormGroup;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    //private authService: AuthService,
    private userService: UserService,
    private router: Router
  ) {
    this.registrationForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
    });
  }

  register(): void {
    this.submitted = true;

    if (this.registrationForm.valid) {
      const user: User = {
        id: 0,
        name: this.registrationForm.get('name')?.value,
        email: this.registrationForm.get('email')?.value,
        password: this.registrationForm.get('password')?.value,
        dateOfBirth: this.registrationForm.get('dateOfBirth')?.value,
      };

      this.userService.createUser(user).subscribe({
        next: () => {
          this.router.navigate(['/login']);
        },
        error: (error) => {
          console.error(error);
          // Выведите сообщение об ошибке пользователю
        }
      });
    }
  }
}