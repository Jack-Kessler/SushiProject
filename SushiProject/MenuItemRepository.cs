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

        public void UpdateMenuItemSQL(MenuItem menuItem)
        {
            _conn.Execute("UPDATE MENU_ITEMS SET MENUITEMNAME = @name, MENUITEMPRICE = @price WHERE MENUITEMID = @id;",
                new { name = menuItem.MenuItemName, price = menuItem.MenuItemPrice, id = menuItem.MenuItemID });
        }

        public void InsertMenuItemSQL(MenuItem menuItemToInsert)
        {
            _conn.Execute("INSERT INTO MENU_ITEMS (MENUITEMNAME, MENUITEMPRICE, MENUITEMCATEGORY) VALUES (@name, @price, @category);",
                new { name = menuItemToInsert.MenuItemName, price = menuItemToInsert.MenuItemPrice, category = menuItemToInsert.MenuItemCategory });
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

        public void DeleteMenuItemSQL(MenuItem menuItem)
        {
            _conn.Execute("DELETE FROM MENU_ITEMS WHERE MENUITEMID = @id;", new {id = menuItem.MenuItemID});
        }
    }
}
