namespace SushiProject.Models
{
    public class SalesTransaction
    {
        public int SalesTransactionID { get; set; }
        public bool SalesTransactionCompleted { get; set; }
        public bool AllYouCanEat { get; set; }
        public decimal FinalTransactionAmount { get; set; }
        public DateTime FinalTransactionDateAndTime { get; set; }
        public int EmployeeID { get; set; }

        public int RestaurantTableID { get; set; }

        public IEnumerable<FoodBevOrder>? OrderList { get; set; } //Note this is null

        public IEnumerable<Employee>? ServerList { get; set; } //Note this is null

        public IEnumerable<RestaurantTable>? RestaurantTableList { get; set; } //Note this is null

        public int OrderID1 { get; set; }
        public decimal OrderPrice1 { get; set; }

        public int OrderID2 { get; set; }
        public decimal OrderPrice2 { get; set; }

        public int OrderID3 { get; set; }
        public decimal OrderPrice3 { get; set; }

        public int OrderID4 { get; set; }
        public decimal OrderPrice4 { get; set; }

        public int OrderID5 { get; set; }
        public decimal OrderPrice5 { get; set; }

        public int OrderID6 { get; set; }
        public decimal OrderPrice6 { get; set; }

        public int OrderID7 { get; set; }
        public decimal OrderPrice7 { get; set; }

        public int OrderID8 { get; set; }
        public decimal OrderPrice8 { get; set; }

        public int OrderID9 { get; set; }
        public decimal OrderPrice9 { get; set; }

        public int OrderID10 { get; set; }
        public decimal OrderPrice10 { get; set; }

        public int OrderID11 { get; set; }
        public decimal OrderPrice11 { get; set; }

        public int OrderID12 { get; set; }
        public decimal OrderPrice12 { get; set; }

        public int OrderID13 { get; set; }
        public decimal OrderPrice13 { get; set; }

        public int OrderID14 { get; set; }
        public decimal OrderPrice14 { get; set; }

        public int OrderID15 { get; set; }
        public decimal OrderPrice15 { get; set; }

        public int OrderID16 { get; set; }
        public decimal OrderPrice16 { get; set; }

        public int OrderID17 { get; set; }
        public decimal OrderPrice17 { get; set; }

        public int OrderID18 { get; set; }
        public decimal OrderPrice18 { get; set; }

        public int OrderID19 { get; set; }
        public decimal OrderPrice19 { get; set; }

        public int OrderID20 { get; set; }
        public decimal OrderPrice20 { get; set; }
    }
}
