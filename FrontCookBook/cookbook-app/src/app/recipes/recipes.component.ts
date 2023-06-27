import { Component, OnInit } from '@angular/core';
import { RecipeService } from '../recipe.service';
import { Recipe } from '../recipe.model';
import { Category } from '../category.model';

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.css']
})
export class RecipesComponent implements OnInit {
  recipes: Recipe[] = [];
  averageRatings: Map<number, number> = new Map<number, number>();
  categories: Category[] = [];
  selectedCategoryId: number = 0;
  searchQuery: string = '';

  constructor(private recipeService: RecipeService) { }

  ngOnInit() {
    this.getRecipes();
    this.getCategories();
  }

  getRecipes() {
    this.recipeService.getRecipes().subscribe(recipes => {
      this.recipes = recipes;
      this.calculateAverageRatings();
    });
  }

  calculateAverageRatings() {
    const filteredRecipes = this.filteredRecipes;
    filteredRecipes.forEach(recipe => {
      this.recipeService.getRatingByRecipeId(recipe.id).subscribe(ratings => {
        if (ratings.length > 0) {
          const totalRating = ratings.reduce((sum, rating) => sum + rating.rating, 0);
          const averageRating = totalRating / ratings.length;
          this.averageRatings.set(recipe.id, averageRating);
        }
      });
    });
  }

  getCategories() {
    this.recipeService.getCategories().subscribe(categories => {
      this.categories = categories;
    });
  }

  onCategoryChange(categoryId: number) {
    this.selectedCategoryId = categoryId;
    if (categoryId == 0) {
      this.getRecipes();
    } else {
      this.recipeService.getRecipesByCategory(categoryId).subscribe(recipes => {
        this.recipes = recipes;
        this.calculateAverageRatings();
      });
    }
  }

  get filteredRecipes(): Recipe[] {
    return this.recipes.filter(recipe => recipe.title.toLowerCase().includes(this.searchQuery.toLowerCase()));
  }
}

// import { Component, OnInit } from '@angular/core';
// import { RecipeService } from '../recipe.service';
// import { Recipe } from '../recipe.model';
// import { Category } from '../category.model';

// @Component({
//   selector: 'app-recipes',
//   templateUrl: './recipes.component.html',
//   styleUrls: ['./recipes.component.css']
// })
// export class RecipesComponent implements OnInit {
//   recipes: Recipe[] = [];
//   averageRatings: Map<number, number> = new Map<number, number>();
//   categories: Category[] = [];
//   selectedCategoryId: number = 0;
//   searchQuery: string = '';

//   constructor(private recipeService: RecipeService) { }

//   ngOnInit() {
//     this.getRecipes();
//     this.getCategories();
//   }

//   getRecipes() {
//     this.recipeService.getRecipes().subscribe(recipes => {
//       this.recipes = recipes;
//       this.calculateAverageRatings();
//     });
//   }

//   calculateAverageRatings() {
//     const filteredRecipes = this.filteredRecipes;
//     filteredRecipes.forEach(recipe => {
//       this.recipeService.getRatingByRecipeId(recipe.id).subscribe(ratings => {
//         if (ratings.length > 0) {
//           const totalRating = ratings.reduce((sum, rating) => sum + rating.rating, 0);
//           const averageRating = totalRating / ratings.length;
//           this.averageRatings.set(recipe.id, averageRating);
//         }
//       });
//     });
//   }

//   getCategories() {
//     this.recipeService.getCategories().subscribe(categories => {
//       this.categories = categories;
//     });
//   }

//   onCategoryChange(categoryId: number) {
//     this.selectedCategoryId = categoryId;
//     if (categoryId == 0) {
//       this.getRecipes();
//     } else {
//       this.recipeService.getRecipesByCategory(categoryId).subscribe(recipes => {
//         this.recipes = recipes;
//         this.calculateAverageRatings();
//       });
//     }
//   }

//   get filteredRecipes(): Recipe[] {
//     return this.recipes.filter(recipe => recipe.title.toLowerCase().includes(this.searchQuery.toLowerCase()));
//   }
// }

