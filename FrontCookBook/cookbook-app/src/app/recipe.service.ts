import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Recipe } from './recipe.model';
import { Category } from './category.model';
import { Comment } from './comment.model';
import { Rating } from './rating.model';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  private apiUrl = 'http://localhost:5148/api/recipes'; // Замените на ваш URL API

  constructor(private http: HttpClient) { }

  getRecipes(): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(this.apiUrl);
  }

  getRecipe(id: number): Observable<Recipe> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Recipe>(url);
  }

  getRecipesByCategory(categoryId: number): Observable<Recipe[]> {
      return this.http.get<Recipe[]>(`${this.apiUrl}/${categoryId}/category`);
  }

  addRecipe(recipe: Recipe): Observable<Recipe> {
    return this.http.post<Recipe>(this.apiUrl, recipe);
  }

  updateRecipe(recipe: Recipe): Observable<Recipe> {
    const url = `${this.apiUrl}/${recipe.id}`;
    return this.http.put<Recipe>(url, recipe);
  }//не надо

  deleteRecipe(id: number): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url);
  }

  getCategories(): Observable<Category[]>{
    return this.http.get<Category[]>('http://localhost:5148/api/categories');
  }

  getCategoryById(id: number): Observable<Category>{
    return this.http.get<Category>(`http://localhost:5148/api/categories/${id}`);
  }

  getCommentsByRecipeId(recipeId: number): Observable<Comment[]> {
    return this.http.get<Comment[]>(`http://localhost:5148/api/comments/${recipeId}`);
  }

  addComment(comment: Comment): Observable<Comment> {
    const userId = +sessionStorage.getItem('userId')!;
    comment.userId = userId;
    return this.http.post<Comment>(`http://localhost:5148/api/comments`, comment);
  }

  deleteComment(userId: number, recipeId: number): Observable<void> {
    return this.http.delete<void>(`http://localhost:5148/api/comments/${userId}/${recipeId}`);
  }

  getRatingByRecipeId(ratingId: number): Observable<Rating[]>{
    return this.http.get<Rating[]>(`http://localhost:5148/api/ratings/${ratingId}`)
  }

  addRating(rating: Rating): Observable<Rating> {
    const userId = +sessionStorage.getItem('userId')!;
    rating.userId = userId;
    return this.http.post<Rating>(`http://localhost:5148/api/ratings`, rating);
  }

  deleteRating(userId: number, recipeId: number): Observable<void> {
    return this.http.delete<void>(`http://localhost:5148/api/ratings/${userId}/${recipeId}`);
  }
}

