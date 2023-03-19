using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class AllYouCanEat
    {
        public int AllYouCanEatID { get; set; }

        [Required(ErrorMessage = "Please enter a valid number between $0.00 and $99.99")]
        [Range(0, 99.99, ErrorMessage = "Value entered must be between ${1} and ${2}")]
        public decimal? AllYouCanEatRate { get; set; }



        [Required(ErrorMessage = "Please enter a valid number between $0.00 and $99.99")]
        [Range(0, 99.99, ErrorMessage = "Value entered must be between ${1} and ${2}")]
        public decimal? NewAllYouCanEatRate { get; set; }


        [Required(ErrorMessage = "Please enter a valid number between $0.00 and $99.99")]
        [Range(0, 99.99, ErrorMessage = "Value entered must be between ${1} and ${2}")]
        public decimal? ConfirmAllYouCanEatRate { get; set; }

        public bool Success { get; set; }
        public bool Adult { get; set; }
    }
}
