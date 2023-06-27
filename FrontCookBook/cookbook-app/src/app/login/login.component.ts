import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm!: FormGroup;
  submitted = false;
  errorMessage: string | null = null;
  //username!: string;
  //password!: string;

  constructor(
  private formBuilder: FormBuilder,
  private authService: AuthService,
  private router: Router) 
  {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }
  login(): void {
    this.submitted = true;
  
    if (this.loginForm.valid) {
      const username = this.loginForm.get('username')?.value;
      const password = this.loginForm.get('password')?.value;
  
      this.authService.login(username, password).subscribe(
        loggedIn => {
          if (loggedIn) {
            // Перенаправление на страницу с рецептами после успешной авторизации
            this.router.navigate(['/recipes']);
          } else {
            // Аутентификация не удалась, выполните необходимые действия, например, отображение сообщения об ошибке.
            this.errorMessage = 'Неверное имя пользователя или пароль.';
          }
        },
        error => {
          // Обработка ошибки, если такая возникла при выполнении запроса
          console.error(error);
          // Дополнительные действия в случае ошибки
        }
      );
    }
  }

  goToRegistration(): void {
    this.router.navigate(['/registration']);
  }
}
// login(): void {

  //   this.submitted = true;

  //   if (this.loginForm.valid) {
  //     const username = this.loginForm.get('username')?.value;
  //     const password = this.loginForm.get('password')?.value;

  //     // Вызов метода сервиса для авторизации
  //     const loggedIn = this.authService.login(username, password);
  //     if (loggedIn) {
  //       // Перенаправление на страницу с рецептами после успешной авторизации

  //       console.log(sessionStorage.getItem('userId')); // Вывод значения в консоль

  //       this.router.navigate(['/recipes']);
  //     } else {
  //       this.errorMessage = 'Неверное имя пользователя или пароль.';
  //       // Аутентификация не удалась, выполните необходимые действия, например, отображение сообщения об ошибке.
  //     }
  //   }
  // }
