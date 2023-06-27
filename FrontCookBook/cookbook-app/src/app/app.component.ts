import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private router: Router) {}
  goToAddRecipe() {
    this.router.navigate(['/recipeform']);
  }

  isLoginPage(): boolean {
    return this.router.url.includes('login');
  }
  
  isRegistrationPage(): boolean {
    return this.router.url.includes('registration');
  }
}
