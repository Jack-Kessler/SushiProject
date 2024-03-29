﻿using SushiProject.Models;

namespace SushiProject
{
    public interface IMenuItemRepository
    {
        public IEnumerable<MenuItem> GetAllMenuItemsSQL();
        public MenuItem GetMenuItemSQL(int menuItemID);
        public void UpdateMenuItemSQL(MenuItem menuItem);
        public void InsertMenuItemSQL(MenuItem menuItemToInsert);
        public MenuItem ValidateIngredientsSQL(MenuItem item);
        public IEnumerable<MenuItemCategory> GetMenuItemCategoriesSQL();
        public MenuItem AssignMenuItemCategorySQL();
        public IEnumerable<FoodBevIngredient> AssignMenuItemIngredientListSQL();
        public void DeleteMenuItemSQL(MenuItem menuItem);
        public bool ReorderMenuItemIDNumbersSQL();
        public MenuItem IngredientSetNullValues(MenuItem item);
    }
}
