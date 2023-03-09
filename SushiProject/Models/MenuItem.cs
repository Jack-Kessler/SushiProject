using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }

        [Required]
        [StringLength(50)] //Will not allow user to enter more than 50 chars.
        public string MenuItemName { get; set; }

        [Required(ErrorMessage = "Please enter a valid ingredient cost per unit between $0.00 - $99.00")]
        [DataType(DataType.Currency)]
        [Range(0, 99.99, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public double MenuItemPrice { get; set; }

        [Required]
        [StringLength(50)] //Will not allow user to enter more than 50 chars.
        public string MenuItemCategory { get; set; }

        public IEnumerable<MenuItemCategory> MenuItemCategories { get; set; }
    }
}