namespace SushiProject.Models
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }
        public string MenuItemName { get; set; }
        public double MenuItemPrice { get; set; }
        public string MenuItemCategory { get; set; }

        public IEnumerable<MenuItemCategory> MenuItemCategories { get; set; }
    }
}

//Comment

//COMMEnt