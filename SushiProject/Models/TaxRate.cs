namespace SushiProject.Models
{
    public class TaxRate
    {
        public int TaxRateID { get; set; }
        public decimal CurrentTaxRate { get; set; }
        public decimal NewTaxRate { get; set; }
        public decimal ConfirmNewTaxRate { get; set; }
    }
}
