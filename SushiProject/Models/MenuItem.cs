using Microsoft.Build.Framework;

namespace SushiProject.Models
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }
        [Required]
        public string MenuItemName { get; set; }
        [Required]
        public double MenuItemPrice { get; set; }
        [Required]
        public string MenuItemCategory { get; set; }

        public IEnumerable<MenuItemCategory> MenuItemCategories { get; set; }
    }
}