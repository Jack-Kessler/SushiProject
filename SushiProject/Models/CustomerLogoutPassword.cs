using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class CustomerLogoutPassword
    {
        public int CustomerLogoutPasswordID { get; set; }

        [Required(ErrorMessage = "Please enter the current password")]
        [StringLength(50)] //Will not allow user to enter more than 50 chars.
        public string? CurrentPassword { get; set; }


        [Required(ErrorMessage = "Please enter the new desired password")]
        [StringLength(50)] //Will not allow user to enter more than 50 chars.
        public string? NewPassword { get; set; }


        [Required(ErrorMessage = "Please enter the new desired password")]
        [StringLength(50)] //Will not allow user to enter more than 50 chars.
        public string? ConfirmPassword { get; set; }

        public bool Success { get; set; }
    }
}
