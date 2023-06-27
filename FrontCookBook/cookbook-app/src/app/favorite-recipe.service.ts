import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FavoriteRecipe } from './favorite-recipe.model';

@Injectable({
  providedIn: 'root'
})
export class FavoriteRecipeService {
  private apiUrl = 'http://localhost:5148/api/favorite';

  constructor(private http: HttpClient) {}

  getFavoriteRecipesByUserId(userId: number): Observable<FavoriteRecipe[]> {
    const url = `${this.apiUrl}/${userId}`;
    return this.http.get<FavoriteRecipe[]>(url);
  }

  addFavoriteRecipe(userId: number, recipeId: number): Observable<FavoriteRecipe> {
    const url = `${this.apiUrl}`;
    const favoriteRecipe: FavoriteRecipe = { id: 0, userId, recipeId };
    return this.http.post<FavoriteRecipe>(url, favoriteRecipe);
  }

  removeFavoriteRecipe(userId: number, recipeId: number): Observable<void> {
    const url = `${this.apiUrl}/${userId}/${recipeId}`;
    return this.http.delete<void>(url);
  }
  

//   removeFavoriteRecipe(favoriteRecipeId: number): Observable<void> {
//     const url = `${this.apiUrl}/favorites/${favoriteRecipeId}`;
//     return this.http.delete<void>(url);
//   }
}
