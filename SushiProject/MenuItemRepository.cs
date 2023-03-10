using Dapper;
using SushiProject.Models;
using System.Data;

namespace SushiProject
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly IDbConnection _conn;

        public MenuItemRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<MenuItem> GetAllMenuItemsSQL()
        {
            return _conn.Query<MenuItem>("SELECT * FROM MENU_ITEMS;");
        }

        public MenuItem GetMenuItemSQL(int menuItemID)
        {
            return _conn.QuerySingle<MenuItem>("SELECT * FROM MENU_ITEMS WHERE MENUITEMID = @id;", new { id = menuItemID });
        }

        public void UpdateMenuItemSQL(MenuItem menuItemToUpdate)
        {
            _conn.Execute("UPDATE MENU_ITEMS " +
                "SET " +
                "MENUITEMNAME = @name, " +
                "MENUITEMPRICE = @price " +
                "MENUITEMCATEGORY = @cat, " +
                "MENUITEMINGREDIENTNAME1 = @I1, " +
                "MenuItemIngredientQuantity1 = @Q1," +
                "MENUITEMINGREDIENTNAME2 = @I2," +
                "MenuItemIngredientQuantity2 = @Q2," +
                "MENUITEMINGREDIENTNAME3 = @I3," +
                "MenuItemIngredientQuantity3 = @Q3," +
                "MENUITEMINGREDIENTNAME4 = @I4," +
                "MenuItemIngredientQuantity4 = @Q4," +
                "MENUITEMINGREDIENTNAME5 = @I5," +
                "MenuItemIngredientQuantity5 = @Q5," +
                "MENUITEMINGREDIENTNAME6 = @I6," +
                "MenuItemIngredientQuantity6 = @Q6," +
                "MENUITEMINGREDIENTNAME7 = @I7," +
                "MenuItemIngredientQuantity7 = @Q7," +
                "MENUITEMINGREDIENTNAME8 = @I8," +
                "MenuItemIngredientQuantity8 = @Q8," +
                "MENUITEMINGREDIENTNAME9 = @I9," +
                "MenuItemIngredientQuantity9 = @Q9," +
                "MENUITEMINGREDIENTNAME10 = @I10," +
                "MenuItemIngredientQuantity10 = @Q10" +
                "" +
                "WHERE MENUITEMID = @id;",
                 new
                 {
                     name = menuItemToUpdate.MenuItemName,
                     price = menuItemToUpdate.MenuItemPrice,
                     cat = menuItemToUpdate.MenuItemCategory,
                     i1 = menuItemToUpdate.MenuItemIngredientName1,
                     q1 = menuItemToUpdate.MenuItemIngredientQuantity1,
                     i2 = menuItemToUpdate.MenuItemIngredientName2,
                     q2 = menuItemToUpdate.MenuItemIngredientQuantity2,
                     i3 = menuItemToUpdate.MenuItemIngredientName3,
                     q3 = menuItemToUpdate.MenuItemIngredientQuantity3,
                     i4 = menuItemToUpdate.MenuItemIngredientName4,
                     q4 = menuItemToUpdate.MenuItemIngredientQuantity4,
                     i5 = menuItemToUpdate.MenuItemIngredientName5,
                     q5 = menuItemToUpdate.MenuItemIngredientQuantity5,
                     i6 = menuItemToUpdate.MenuItemIngredientName6,
                     q6 = menuItemToUpdate.MenuItemIngredientQuantity6,
                     i7 = menuItemToUpdate.MenuItemIngredientName7,
                     q7 = menuItemToUpdate.MenuItemIngredientQuantity7,
                     i8 = menuItemToUpdate.MenuItemIngredientName8,
                     q8 = menuItemToUpdate.MenuItemIngredientQuantity8,
                     i9 = menuItemToUpdate.MenuItemIngredientName9,
                     q9 = menuItemToUpdate.MenuItemIngredientQuantity9,
                     i10 = menuItemToUpdate.MenuItemIngredientName10,
                     q10 = menuItemToUpdate.MenuItemIngredientQuantity10,
                 });
        }

        public void InsertMenuItemSQL(MenuItem menuItemToInsert)
        {
            _conn.Execute("INSERT INTO MENU_ITEMS " +
                "(" +
                "MENUITEMNAME, " +
                "MENUITEMPRICE, " +
                "MENUITEMCATEGORY, " +
                "MENUITEMINGREDIENTNAME1, " +
                "MenuItemIngredientQuantity1," +
                "MENUITEMINGREDIENTNAME2," +
                "MenuItemIngredientQuantity2," +
                "MENUITEMINGREDIENTNAME3," +
                "MenuItemIngredientQuantity3," +
                "MENUITEMINGREDIENTNAME4," +
                "MenuItemIngredientQuantity4," +
                "MENUITEMINGREDIENTNAME5," +
                "MenuItemIngredientQuantity5," +
                "MENUITEMINGREDIENTNAME6," +
                "MenuItemIngredientQuantity6," +
                "MENUITEMINGREDIENTNAME7," +
                "MenuItemIngredientQuantity7," +
                "MENUITEMINGREDIENTNAME8," +
                "MenuItemIngredientQuantity8," +
                "MENUITEMINGREDIENTNAME9," +
                "MenuItemIngredientQuantity9," +
                "MENUITEMINGREDIENTNAME10," +
                "MenuItemIngredientQuantity10" +
                ") " +
                "VALUES (" +
                "@name, " +
                "@price, " +
                "@category, " +
                "@i1, " +
                "@q1, " +
                "@i2, " +
                "@q2, " +
                "@i3, " +
                "@q3, " +
                "@i4, " +
                "@q4, " +
                "@i5, " +
                "@q5, " +
                "@i6, " +
                "@q6, " +
                "@i7, " +
                "@q7, " +
                "@i8, " +
                "@q8, " +
                "@i9, " +
                "@q9, " +
                "@i10, " +
                "@q10 " +
                ");",
                new { 
                    name = menuItemToInsert.MenuItemName, 
                    price = menuItemToInsert.MenuItemPrice, 
                    category = menuItemToInsert.MenuItemCategory, 
                    i1 = menuItemToInsert.MenuItemIngredientName1, 
                    q1 = menuItemToInsert.MenuItemIngredientQuantity1,
                    i2 = menuItemToInsert.MenuItemIngredientName2,
                    q2 = menuItemToInsert.MenuItemIngredientQuantity2,
                    i3 = menuItemToInsert.MenuItemIngredientName3,
                    q3 = menuItemToInsert.MenuItemIngredientQuantity3,
                    i4 = menuItemToInsert.MenuItemIngredientName4,
                    q4 = menuItemToInsert.MenuItemIngredientQuantity4,
                    i5 = menuItemToInsert.MenuItemIngredientName5,
                    q5 = menuItemToInsert.MenuItemIngredientQuantity5,
                    i6 = menuItemToInsert.MenuItemIngredientName6,
                    q6 = menuItemToInsert.MenuItemIngredientQuantity6,
                    i7 = menuItemToInsert.MenuItemIngredientName7,
                    q7 = menuItemToInsert.MenuItemIngredientQuantity7,
                    i8 = menuItemToInsert.MenuItemIngredientName8,
                    q8 = menuItemToInsert.MenuItemIngredientQuantity8,
                    i9 = menuItemToInsert.MenuItemIngredientName9,
                    q9 = menuItemToInsert.MenuItemIngredientQuantity9,
                    i10 = menuItemToInsert.MenuItemIngredientName10,
                    q10 = menuItemToInsert.MenuItemIngredientQuantity10,
                    });
        }
        public IEnumerable<MenuItemCategory> GetMenuItemCategoriesSQL()
        {
            return _conn.Query<MenuItemCategory>("SELECT * FROM MENU_CATEGORIES;");
        }
        public MenuItem AssignMenuItemCategorySQL()
        {
            var menuItemCategoryList = GetMenuItemCategoriesSQL();
            var item = new MenuItem();
            item.MenuItemCategories = menuItemCategoryList;
            return item;
        }
        public IEnumerable<FoodBevIngredient> AssignMenuItemIngredientListSQL()
        {
            return _conn.Query<FoodBevIngredient>("SELECT * FROM FOOD_BEV_INGREDIENTS;");
        }

        public void DeleteMenuItemSQL(MenuItem menuItem)
        {
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE MENUITEMID = @id;", new {id = menuItem.MenuItemID});
        }

        public MenuItem IngredientSetNullValues(MenuItem item)
        {
            if (item.MenuItemIngredientName2 == "Default-NoIngredient" ||
                item.MenuItemIngredientQuantity2 == null)
            {
                item.MenuItemIngredientName2 = null;
                item.MenuItemIngredientQuantity2 = null;
            }
            if (item.MenuItemIngredientName3 == "Default-NoIngredient" ||
                item.MenuItemIngredientQuantity3 == null)
            {
                item.MenuItemIngredientName3 = null;
                item.MenuItemIngredientQuantity3 = null;
            }
            if (item.MenuItemIngredientName4 == "Default-NoIngredient" ||
                item.MenuItemIngredientQuantity4 == null)
            {
                item.MenuItemIngredientName4 = null;
                item.MenuItemIngredientQuantity4 = null;
            }
            if (item.MenuItemIngredientName5 == "Default-NoIngredient" ||
                item.MenuItemIngredientQuantity5 == null)
            {
                item.MenuItemIngredientName5 = null;
                item.MenuItemIngredientQuantity5 = null;
            }
            if (item.MenuItemIngredientName6 == "Default-NoIngredient" ||
                item.MenuItemIngredientQuantity6 == null)
            {
                item.MenuItemIngredientName6 = null;
                item.MenuItemIngredientQuantity6 = null;
            }
            if (item.MenuItemIngredientName7 == "Default-NoIngredient" ||
                item.MenuItemIngredientQuantity7 == null)
            {
                item.MenuItemIngredientName7 = null;
                item.MenuItemIngredientQuantity7 = null;
            }
            if (item.MenuItemIngredientName8 == "Default-NoIngredient" ||
                item.MenuItemIngredientQuantity8 == null)
            {
                item.MenuItemIngredientName8 = null;
                item.MenuItemIngredientQuantity8 = null;
            }
            if (item.MenuItemIngredientName9 == "Default-NoIngredient" ||
                item.MenuItemIngredientQuantity9 == null)
            {
                item.MenuItemIngredientName9 = null;
                item.MenuItemIngredientQuantity9 = null;
            }   
            if (item.MenuItemIngredientName10 == "Default-NoIngredient" ||
                item.MenuItemIngredientQuantity10 == null)
            {
                item.MenuItemIngredientName10 = null;
                item.MenuItemIngredientQuantity10 = null;
            }
            return item;
        }
    }
}
