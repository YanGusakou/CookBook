import { Injectable } from '@angular/core';
import { UserService } from './user.service';
import { Observable } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';
import { tap } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = false;

  constructor(
    private userService: UserService,
    /*private sessionStorage: Storage*/)
    { }
    // login(username: string, password: string): Observable<boolean> {
    //   return this.userService.getUsers().pipe(
    //     tap(users => console.log('Users:', users)), // Отладочный вывод
    //     map(users => {
    //       const foundUser = users.find(user => user.name === username);
    //       if (foundUser && foundUser.password === password) {
    //         const userId = foundUser.id;
    //         console.log('Found User:', foundUser); // Отладочный вывод
    //         console.log('User ID:', userId); // Отладочный вывод
    //         sessionStorage.setItem('userId', userId.toString());
    //         this.loggedIn = true;
    //         return true;
    //       }
    //       return false;
    //     })
    //   );
    // }
    login(username: string, password: string): Observable<boolean> {
      return this.userService.getUserByName(username).pipe(
        switchMap(user => {
          if (user && user.password === password) {
            const userId = user.id;
            console.log(userId);
            sessionStorage.setItem('userId', userId.toString());
            this.loggedIn = true;
            return of(true);
          } else {
            return of(false);
          }
        })
      );
    }
    // login(username: string, password: string): Observable<boolean> {
    //   return this.userService.getUsers().pipe(
    //     map(users => {
    //       const foundUser = users.find(user => user.name === username);
    //       if (foundUser && foundUser.password === password) {
    //         const userId = foundUser.id;
    //         console.log(foundUser);
    //         console.log(userId);
    //         sessionStorage.setItem('userId', userId.toString());

    //         this.loggedIn = true;
    //         return true;
    //       }
    //       return false;
    //     })
    //   );
    // }
    // login(username: string, password: string): Observable<boolean> {
    //   return this.userService.getUsers().pipe(
    //     map(users => {
    //       const foundUser = users.find(user => user.name === username);
    //       if (foundUser && foundUser.password === password) {
    //         const userId = foundUser.id;
    //         console.log(foundUser);
    //         console.log(userId);
    //         //console.log(sessionStorage.getItem('userId')); // Вывод значения в консоль
    //         sessionStorage.setItem('userId', userId.toString());
    //         //console.log(sessionStorage.getItem('userId')); // Вывод значения в консоль

    //         this.loggedIn = true;
    //         return true;
    //       }
    //       return false;
    //     })
    //   );
    // }
    // login(username: string, password: string): Observable<boolean> {
    //   const users = this.userService.getUsers();
    //   const user = users.find(user => user.name === username);
    // }
    // login(username: string, password: string): boolean {
    //   this.userService.getUsers().subscribe(users => {
    //     const foundUser = users.find(user => user.name === username);
    //     if (foundUser && foundUser.password === password) {
    //       const userId = foundUser.id;
    //       sessionStorage.setItem('userId', userId.toString());
    //       this.loggedIn = true;
    //       return true;
    //     }
    //     return false;
    //   });
    // }   

  logout(): void {
    // Здесь вы можете выполнить дополнительные действия при выходе пользователя, например, очистить данные аутентификации.
    this.loggedIn = false;
  }

  isLoggedIn(): boolean {
    return this.loggedIn;
  }
}
//const userId = +this.sessionStorage.getItem('userId');
