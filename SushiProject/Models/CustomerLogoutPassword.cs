namespace SushiProject.Models
{
    public class CustomerLogoutPassword
    {
        public int CustomerLogoutPasswordID { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
