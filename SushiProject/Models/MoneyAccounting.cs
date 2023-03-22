namespace SushiProject.Models
{
    public class MoneyAccounting
    {
        public int CreditDebitID { get; set; }
        public string DebitOrCredit { get; set; }
        public string DebitCreditType { get; set; }
        public decimal DebitCreditAmount { get; set; }
        public int SalesTransactionID { get; set; }
        public  DateTime DateAndTime { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
