import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RecipesComponent } from './recipes/recipes.component';
import { RecipeDetailsComponent } from './recipe-details/recipe-details.component';
import { RecipeFormComponent } from './recipe-form/recipe-form.component';
import { UserAccountComponent } from './user-account/user-account.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { RegistrationComponent } from './registration/registration.component';
//import { SessionStorage } from 'ngx-webstorage';

@NgModule({
  declarations: [
    AppComponent,
    RecipesComponent,
    RecipeDetailsComponent,
    RecipeFormComponent,
    UserAccountComponent,
    LoginComponent,
    RegistrationComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule, 
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    /*{ provide: Storage, useValue: sessionStorage }*/
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
// export class MyService {
//   constructor(@Inject('SESSION_STORAGE') private sessionStorage: Storage) {
//     // Ваш код инициализации сервиса
//   }для глобальной переменной