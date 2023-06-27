import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { RecipesComponent } from './recipes/recipes.component';
import { RecipeDetailsComponent } from './recipe-details/recipe-details.component';
import { RecipeFormComponent } from './recipe-form/recipe-form.component';
import { UserAccountComponent } from './user-account/user-account.component';
import { LoginComponent } from './login/login.component';//для логинга
//import { ReactiveFormsModule } from '@angular/forms';
import { RegistrationComponent } from './registration/registration.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'registration', component: RegistrationComponent },
  //{ path: '', redirectTo: '/recipes', pathMatch: 'full' },
  { path: 'recipes', component: RecipesComponent },
  { path: 'recipes/:id', component: RecipeDetailsComponent },
  { path: 'recipeform', component: RecipeFormComponent },
  //{ path: 'edit-recipe/:id', component: RecipeFormComponent },//не надо
  { path: 'accounts/:id', component: UserAccountComponent },//надо добавить маршрут для логинга
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
