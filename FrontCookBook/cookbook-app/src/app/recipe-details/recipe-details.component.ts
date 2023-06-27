import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Recipe } from '../recipe.model';
import { RecipeService } from '../recipe.service';
import { FavoriteRecipeService } from '../favorite-recipe.service';
import { Comment } from '../comment.model';
import { CommentWithUserName } from '../CommentWithUserName.model';
import { UserService } from '../user.service';
import { Rating } from '../rating.model';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.css']
})
export class RecipeDetailsComponent implements OnInit {
  recipe: Recipe | undefined;
  isFavorite: boolean = false;
  comments: CommentWithUserName[] = [];
  commentContent: string = '';
  recipeId = +this.route.snapshot.paramMap.get('id')!;
  userId = +sessionStorage.getItem('userId')!;
  ratings: Rating[] = [];
  averageRating: number = 0;
  userRating: number | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private recipeService: RecipeService,
    private favoriteRecipeService: FavoriteRecipeService,
    private userService: UserService 
  ) {}

  ngOnInit(): void {
    this.getRecipe();
    this.getUserId();
    this.checkFavoriteStatus();
    this.getComments();
    this.getRatings().then(() => {
      this.getUserRating();
    });
  }

  getUserRating(): void {
    const userId = +sessionStorage.getItem('userId')!;
    const userRating = this.ratings.find(rating => rating.userId === userId);
    this.userRating = userRating ? userRating.rating : null;
  }

  rateRecipe(rating: number): void {
    const userId = +sessionStorage.getItem('userId')!;
    const recipeId = this.recipeId;
    const newRating: Rating = { id: 0, userId, recipeId, rating };
  
    this.recipeService.addRating(newRating).subscribe(() => {
      this.getRatings().then(() => {
        this.getUserRating();
      });
    });
  }
  
  deleteRating(): void {
    const userId = +sessionStorage.getItem('userId')!;
    const recipeId = this.recipeId;
  
    this.recipeService.deleteRating(userId, recipeId).subscribe(() => {
      this.getRatings().then(() => {
        this.getUserRating();
      });
    });
  }

  getRatings(): Promise<void> {
    return new Promise((resolve) => {
      this.recipeService.getRatingByRecipeId(this.recipeId).subscribe(ratings => {
        this.ratings = ratings;
        this.calculateAverageRating();
        resolve(); // Разрешить промис после получения оценок
      });
    });
  }

  calculateAverageRating(): void {
    if (this.ratings.length > 0) {
      const sum = this.ratings.reduce((total, rating) => total + rating.rating, 0);
      this.averageRating = sum / this.ratings.length;
    } else {
      this.averageRating = 0;
    }
  }

  addComment(): void {
    if (!this.commentContent) {
      return;
    }

    const comment: Comment = {
      id: 0, // ID будет присвоен на сервере
      userId: this.userId,
      recipeId: this.recipeId,
      content: this.commentContent
    };

    this.recipeService.addComment(comment).subscribe(() => {
      this.commentContent = ''; // Сбросить поле ввода комментария после добавления
      this.getComments(); // Обновить список комментариев
    });
  }

  deleteComment(commentId: number): void {
    const userId = +sessionStorage.getItem('userId')!;
    const recipeId = +this.route.snapshot.paramMap.get('id')!;

    this.recipeService.deleteComment(userId, recipeId).subscribe(() => {
      this.getComments(); // Обновить список комментариев
    });
  }

  isCommentOwner(comment: CommentWithUserName): boolean {
    const userId = +sessionStorage.getItem('userId')!;
    return comment.userId === userId;
  }

  getComments(): void {
    this.recipeService.getCommentsByRecipeId(this.recipeId).subscribe(comments => {
      const userIds = comments.map(comment => comment.userId);
      const uniqueUserIds = [...new Set(userIds)]; // Убираем дублирующиеся ID пользователей
  
      const getUserNamesPromises = uniqueUserIds.map(userId => {
        return this.userService.getUserById(userId).toPromise();
      });
  
      Promise.all(getUserNamesPromises).then(users => {
        const userMap = new Map<number, string>();
        users.forEach(user => {
          if (user) {
            userMap.set(user.id, user.name);
          }
        });
  
        const updatedComments: CommentWithUserName[] = comments.map(comment => {
          const userName = userMap.get(comment.userId) || '';
          return { ...comment, userName };
        });
  
        this.comments = updatedComments;
      });
    });
  }

  getRecipe(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.recipeService.getRecipe(id).subscribe(recipe => {
      this.recipe = recipe;
    });
  }

  getUserId(): void {
    this.userId = +sessionStorage.getItem('userId')!;
  }

  checkFavoriteStatus(): void {
    const recipeId = +this.route.snapshot.paramMap.get('id')!;
  
    if (this.userId !== undefined) {
      this.favoriteRecipeService.getFavoriteRecipesByUserId(this.userId).subscribe(recipes => {
        const favoriteRecipe = recipes.find(recipe => recipe.recipeId === recipeId);
        this.isFavorite = !!favoriteRecipe;
      });
    }
  }

  toggleFavorite(): void {
    const recipeId = +this.route.snapshot.paramMap.get('id')!;
  
    if (this.userId !== undefined) {
      if (this.isFavorite) {
        this.favoriteRecipeService.removeFavoriteRecipe(this.userId, recipeId).subscribe(() => {
          this.isFavorite = false;
        });
      } else {
        this.favoriteRecipeService.addFavoriteRecipe(this.userId, recipeId).subscribe(() => {
          this.isFavorite = true;
        });
      }
    }
  }

  deleteRecipe(): void {
    if (!this.recipe) return;

    this.recipeService.deleteRecipe(this.recipe.id)
      .subscribe(() => {
        // Рецепт успешно удален, выполнить необходимые действия, например, навигация на другую страницу
        this.router.navigate(['/recipes']);
      });
  }

  isOwner(): boolean {
    return this.recipe !== undefined && this.userId !== undefined && this.userId === this.recipe.userId;
  }
}

// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { Recipe } from '../recipe.model';
// import { RecipeService } from '../recipe.service';
// import { FavoriteRecipeService } from '../favorite-recipe.service';
// import { Comment } from '../comment.model';
// import { CommentWithUserName } from '../CommentWithUserName.model';
// import { UserService } from '../user.service';
// import { Rating } from '../rating.model';

// @Component({
//   selector: 'app-recipe-details',
//   templateUrl: './recipe-details.component.html',
//   styleUrls: ['./recipe-details.component.css']
// })
// export class RecipeDetailsComponent implements OnInit {
//   recipe: Recipe | undefined;
//   isFavorite: boolean = false;
//   comments: CommentWithUserName[] = [];
//   commentContent: string = '';
//   recipeId = +this.route.snapshot.paramMap.get('id')!;
//   userId = +sessionStorage.getItem('userId')!;
//   ratings: Rating[] = [];
//   averageRating: number = 0;

//   constructor(
//     private route: ActivatedRoute,
//     private router: Router,
//     private recipeService: RecipeService,
//     private favoriteRecipeService: FavoriteRecipeService,
//     private userService: UserService 
//   ) {}

//   ngOnInit(): void {
//     this.getRecipe();
//     this.getUserId();
//     this.checkFavoriteStatus();
//     this.getComments();
//     this.getRatings();
//   }

//   getRatings(): void {
//     this.recipeService.getRatingByRecipeId(this.recipeId).subscribe(ratings => {
//       this.ratings = ratings;
//       this.calculateAverageRating();
//     });
//   }

//   calculateAverageRating(): void {
//     if (this.ratings.length > 0) {
//       const sum = this.ratings.reduce((total, rating) => total + rating.rating, 0);
//       this.averageRating = sum / this.ratings.length;
//     } else {
//       this.averageRating = 0;
//     }
//   }

//   addComment(): void {
//     if (!this.commentContent) {
//       return;
//     }

//     const comment: Comment = {
//       id: 0, // ID будет присвоен на сервере
//       userId: this.userId,
//       recipeId: this.recipeId,
//       content: this.commentContent
//     };

//     this.recipeService.addComment(comment).subscribe(() => {
//       this.commentContent = ''; // Сбросить поле ввода комментария после добавления
//       this.getComments(); // Обновить список комментариев
//     });
//   }

//   deleteComment(commentId: number): void {
//     const userId = +sessionStorage.getItem('userId')!;
//     const recipeId = +this.route.snapshot.paramMap.get('id')!;

//     this.recipeService.deleteComment(userId, recipeId).subscribe(() => {
//       this.getComments(); // Обновить список комментариев
//     });
//   }

//   isCommentOwner(comment: CommentWithUserName): boolean {
//     const userId = +sessionStorage.getItem('userId')!;
//     return comment.userId === userId;
//   }

//   getComments(): void {
//     this.recipeService.getCommentsByRecipeId(this.recipeId).subscribe(comments => {
//       const userIds = comments.map(comment => comment.userId);
//       const uniqueUserIds = [...new Set(userIds)]; // Убираем дублирующиеся ID пользователей
  
//       const getUserNamesPromises = uniqueUserIds.map(userId => {
//         return this.userService.getUserById(userId).toPromise();
//       });
  
//       Promise.all(getUserNamesPromises).then(users => {
//         const userMap = new Map<number, string>();
//         users.forEach(user => {
//           if (user) {
//             userMap.set(user.id, user.name);
//           }
//         });
  
//         const updatedComments: CommentWithUserName[] = comments.map(comment => {
//           const userName = userMap.get(comment.userId) || '';
//           return { ...comment, userName };
//         });
  
//         this.comments = updatedComments;
//       });
//     });
//   }

//   getRecipe(): void {
//     const id = +this.route.snapshot.paramMap.get('id')!;
//     this.recipeService.getRecipe(id).subscribe(recipe => {
//       this.recipe = recipe;
//     });
//   }

//   getUserId(): void {
//     this.userId = +sessionStorage.getItem('userId')!;
//   }

//   checkFavoriteStatus(): void {
//     const recipeId = +this.route.snapshot.paramMap.get('id')!;
  
//     if (this.userId !== undefined) {
//       this.favoriteRecipeService.getFavoriteRecipesByUserId(this.userId).subscribe(recipes => {
//         const favoriteRecipe = recipes.find(recipe => recipe.recipeId === recipeId);
//         this.isFavorite = !!favoriteRecipe;
//       });
//     }
//   }

//   toggleFavorite(): void {
//     const recipeId = +this.route.snapshot.paramMap.get('id')!;
  
//     if (this.userId !== undefined) {
//       if (this.isFavorite) {
//         this.favoriteRecipeService.removeFavoriteRecipe(this.userId, recipeId).subscribe(() => {
//           this.isFavorite = false;
//         });
//       } else {
//         this.favoriteRecipeService.addFavoriteRecipe(this.userId, recipeId).subscribe(() => {
//           this.isFavorite = true;
//         });
//       }
//     }
//   }

//   deleteRecipe(): void {
//     if (!this.recipe) return;

//     this.recipeService.deleteRecipe(this.recipe.id)
//       .subscribe(() => {
//         // Рецепт успешно удален, выполнить необходимые действия, например, навигация на другую страницу
//         this.router.navigate(['/recipes']);
//       });
//   }

//   isOwner(): boolean {
//     return this.recipe !== undefined && this.userId !== undefined && this.userId === this.recipe.userId;
//   }
// }

// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { Recipe } from '../recipe.model';
// import { RecipeService } from '../recipe.service';
// import { FavoriteRecipeService } from '../favorite-recipe.service';
// import { Comment } from '../comment.model';
// import { CommentWithUserName } from '../CommentWithUserName.model';
// import { UserService } from '../user.service';

// @Component({
//   selector: 'app-recipe-details',
//   templateUrl: './recipe-details.component.html',
//   styleUrls: ['./recipe-details.component.css']
// })
// export class RecipeDetailsComponent implements OnInit {
//   recipe: Recipe | undefined;
//   isFavorite: boolean = false;
//   comments: CommentWithUserName[] = [];
//   commentContent: string = '';
//   recipeId = +this.route.snapshot.paramMap.get('id')!;
//   userId = +sessionStorage.getItem('userId')!;

//   constructor(
//     private route: ActivatedRoute,
//     private router: Router,
//     private recipeService: RecipeService,
//     private favoriteRecipeService: FavoriteRecipeService,
//     private userService: UserService 
//   ) {}

//   ngOnInit(): void {
//     this.getRecipe();
//     this.getUserId();
//     this.checkFavoriteStatus();
//     this.getComments();
//   }

//   addComment(): void {
//     if (!this.commentContent) {
//       return;
//     }

//     const comment: Comment = {
//       id: 0, // ID будет присвоен на сервере
//       userId: this.userId,
//       recipeId: this.recipeId,
//       content: this.commentContent
//     };

//     this.recipeService.addComment(comment).subscribe(() => {
//       this.commentContent = ''; // Сбросить поле ввода комментария после добавления
//       this.getComments(); // Обновить список комментариев
//     });
//   }

//   deleteComment(commentId: number): void {
//     const userId = +sessionStorage.getItem('userId')!;
//     const recipeId = +this.route.snapshot.paramMap.get('id')!;

//     this.recipeService.deleteComment(userId, recipeId).subscribe(() => {
//       this.getComments(); // Обновить список комментариев
//     });
//   }

//   isCommentOwner(comment: CommentWithUserName): boolean {
//     const userId = +sessionStorage.getItem('userId')!;
//     return comment.userId === userId;
//   }

//   getComments(): void {
//     this.recipeService.getCommentsByRecipeId(this.recipeId).subscribe(comments => {
//       const userIds = comments.map(comment => comment.userId);
//       const uniqueUserIds = [...new Set(userIds)]; // Убираем дублирующиеся ID пользователей
  
//       const getUserNamesPromises = uniqueUserIds.map(userId => {
//         return this.userService.getUserById(userId).toPromise();
//       });
  
//       Promise.all(getUserNamesPromises).then(users => {
//         const userMap = new Map<number, string>();
//         users.forEach(user => {
//           if (user) {
//             userMap.set(user.id, user.name);
//           }
//         });
  
//         const updatedComments: CommentWithUserName[] = comments.map(comment => {
//           const userName = userMap.get(comment.userId) || '';
//           return { ...comment, userName };
//         });
  
//         this.comments = updatedComments;
//       });
//     });
//   }

//   getRecipe(): void {
//     const id = +this.route.snapshot.paramMap.get('id')!;
//     this.recipeService.getRecipe(id).subscribe(recipe => {
//       this.recipe = recipe;
//     });
//   }

//   getUserId(): void {
//     this.userId = +sessionStorage.getItem('userId')!;
//   }

//   checkFavoriteStatus(): void {
//     const recipeId = +this.route.snapshot.paramMap.get('id')!;
  
//     if (this.userId !== undefined) {
//       this.favoriteRecipeService.getFavoriteRecipesByUserId(this.userId).subscribe(recipes => {
//         const favoriteRecipe = recipes.find(recipe => recipe.recipeId === recipeId);
//         this.isFavorite = !!favoriteRecipe;
//       });
//     }
//   }

//   toggleFavorite(): void {
//     const recipeId = +this.route.snapshot.paramMap.get('id')!;
  
//     if (this.userId !== undefined) {
//       if (this.isFavorite) {
//         this.favoriteRecipeService.removeFavoriteRecipe(this.userId, recipeId).subscribe(() => {
//           this.isFavorite = false;
//         });
//       } else {
//         this.favoriteRecipeService.addFavoriteRecipe(this.userId, recipeId).subscribe(() => {
//           this.isFavorite = true;
//         });
//       }
//     }
//   }

//   deleteRecipe(): void {
//     if (!this.recipe) return;

//     this.recipeService.deleteRecipe(this.recipe.id)
//       .subscribe(() => {
//         // Рецепт успешно удален, выполнить необходимые действия, например, навигация на другую страницу
//         this.router.navigate(['/recipes']);
//       });
//   }

//   isOwner(): boolean {
//     return this.recipe !== undefined && this.userId !== undefined && this.userId === this.recipe.userId;
//   }
// }







