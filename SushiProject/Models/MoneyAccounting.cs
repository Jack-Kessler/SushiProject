using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class MoneyAccounting
    {
        public int CreditDebitID { get; set; }
        public string DebitOrCredit { get; set; }
        public string DebitCreditType { get; set; }

        [Required(ErrorMessage = "Enter a number greater than 0")]
        [Range(0.01, 100000000, ErrorMessage = "Enter a number greater than 0")]
        public decimal? DebitCreditAmount { get; set; }
        public int SalesTransactionID { get; set; }
        public  DateTime DateAndTime { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
