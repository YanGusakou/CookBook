import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RecipeService } from '../recipe.service';
import { Recipe } from '../recipe.model';
import { Category } from '../category.model';

@Component({
  selector: 'app-recipe-form',
  templateUrl: './recipe-form.component.html',
  styleUrls: ['./recipe-form.component.css']
})
export class RecipeFormComponent implements OnInit {
  recipeForm!: FormGroup;
  categories: Category[] = [];

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private recipeService: RecipeService
  ) {}

  ngOnInit(): void {
    this.recipeForm = this.formBuilder.group({
      title: ['', Validators.required],
      ingredients: ['', Validators.required],
      instructions: ['', Validators.required],
      category: ['', Validators.required]
    });

    this.getCategories();
  }

  getCategories(): void {
    this.recipeService.getCategories().subscribe(categories => {
      this.categories = categories;
    });
  }

  onSubmit(): void {
    if (this.recipeForm == null || this.recipeForm.invalid) {
      return;
    }

    const { title, ingredients, instructions, category } = this.recipeForm.value;
    const userId = +sessionStorage.getItem('userId')!;

    const recipe: Recipe = {
      id: 0, // ID будет присвоен на сервере
      title,
      ingredients,
      instructions,
      userId,
      categoryId: category.id
    };

    this.recipeService.addRecipe(recipe).subscribe(() => {
      // Рецепт успешно добавлен, выполнить необходимые действия, например, навигация на другую страницу
      this.router.navigate(['/recipes']);
    });
  }
}


// import { Component } from '@angular/core';
// import { Router } from '@angular/router';
// import { RecipeService } from '../recipe.service';
// import { Recipe } from '../recipe.model';

// @Component({
//   selector: 'app-recipe-form',
//   templateUrl: './recipe-form.component.html',
//   styleUrls: ['./recipe-form.component.css']
// })
// export class RecipeFormComponent {
//   recipe: Recipe = { id: 0, title: '',ingredients: '', instructions: '' };

//   constructor(
//     private router: Router,
//     private recipeService: RecipeService
//   ) { }

//   saveRecipe() {
//     this.recipeService.addRecipe(this.recipe).subscribe(() => {
//       this.router.navigate(['/recipes']);
//     });
//   }
// }