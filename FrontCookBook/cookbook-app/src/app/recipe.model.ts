export interface Recipe {
    id: number;
    title: string;
    ingredients: string
    instructions: string;
    userId: number; // Добавлено свойство userId
    categoryId: number; // Добавлено свойство categoryId
  }