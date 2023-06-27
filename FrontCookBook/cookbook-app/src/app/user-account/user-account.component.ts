import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../user.model';
import { FavoriteRecipeService } from '../favorite-recipe.service';
import { FavoriteRecipe } from '../favorite-recipe.model';
import { RecipeService } from '../recipe.service';
import { forkJoin } from 'rxjs';
import { Recipe } from '../recipe.model';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {
  user: User | undefined;
  deletingAccount = false;
  favoriteRecipes: Recipe[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private favoriteRecipeService: FavoriteRecipeService,
    private recipeService: RecipeService
  ) {}

  ngOnInit(): void {
    const id = +window.sessionStorage.getItem('userId')!;
    this.userService.getUserById(id).subscribe(user => {
      this.user = user;
    });
    this.getFavoriteRecipes(id);
  }

  viewRecipe(recipeId: number): void {
    this.router.navigate(['/recipes', recipeId]);
  }

  getFavoriteRecipes(userId: number): void {
    this.favoriteRecipeService.getFavoriteRecipesByUserId(userId).subscribe(favoriteRecipes => {
      const recipeIds = favoriteRecipes.map(favoriteRecipe => favoriteRecipe.recipeId);
      const recipeObservables = recipeIds.map(recipeId => this.recipeService.getRecipe(recipeId));
      forkJoin(recipeObservables).subscribe(recipes => {
        this.favoriteRecipes = recipes;
      });
    });
  }

  // getFavoriteRecipes(userId: number): void {
  //   this.favoriteRecipeService.getFavoriteRecipesByUserId(userId).subscribe(favoriteRecipes => {
  //     const recipeIds = favoriteRecipes.map(favoriteRecipe => favoriteRecipe.recipeId);
  //     this.recipeService.getRecipesByIds(recipeIds).subscribe(recipes => {
  //       this.favoriteRecipes = recipes;
  //     });
  //   });
  // }

  // getFavoriteRecipes(userId: number): void {
  //   this.favoriteRecipeService.getFavoriteRecipesByUserId(userId).subscribe(recipes => {
  //     this.favoriteRecipes = recipes;
  //   });
  // }

  logout(): void {
    this.router.navigate(['/login']);
  }

  deleteAccount(): void {
    this.deletingAccount = true;
  }

  confirmDeleteAccount(): void {
    if (confirm('Вы уверены, что хотите удалить аккаунт?')) {
      const id = +window.sessionStorage.getItem('userId')!;
      this.userService.deleteUser(id).subscribe(() => {
        this.router.navigate(['/login']);
      });
    }
  }
}