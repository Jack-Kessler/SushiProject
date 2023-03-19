using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class TaxRate
    {
        public int TaxRateID { get; set; }

        [Required(ErrorMessage = "Please enter a valid number between 0.0000 and 1.0000")]
        [Range(0, 1, ErrorMessage = "Value entered must be between {1} and {2}")]
        public decimal? CurrentTaxRate { get; set; }

        [Required(ErrorMessage = "Please enter a valid number between 0.0000 and 1.0000")]
        [Range(0, 1, ErrorMessage = "Value entered must be between {1} and {2}")]
        public decimal? NewTaxRate { get; set; }

        [Required(ErrorMessage = "Please enter a valid number between 0.0000 and 1.0000")]
        [Range(0, 1, ErrorMessage = "Value entered must be between {1} and {2}")]
        public decimal? ConfirmNewTaxRate { get; set; }

        public bool Success { get; set; }
    }
}
