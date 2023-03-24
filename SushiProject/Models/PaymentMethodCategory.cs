using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class PaymentMethodCategory
    {
        public int PaymentMethodCategoryID { get; set; }

        [StringLength(50)] //Will not allow user to enter more than 50 chars.
        public string PaymentMethodCategoryName { get; set; }
    }
}
