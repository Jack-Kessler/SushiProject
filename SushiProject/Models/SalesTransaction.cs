﻿using System.ComponentModel.DataAnnotations;

namespace SushiProject.Models
{
    public class SalesTransaction
    {
        public int SalesTransactionID { get; set; }
        public bool SalesTransactionCompleted { get; set; }

        public bool AllYouCanEat { get; set; }

        // Need to make all of the following variables nullable to customize error message next to "Required" attribute

        [Required(ErrorMessage = "Please enter a number between 0 and 100")]

        [Range(0, 100, ErrorMessage = "Please enter a number between {1} and {2}")]
        public int? NumOfCustomersAdult { get; set; }

        [Required(ErrorMessage = "Please enter a number between 0 and 100")]
        [Range(0, 100, ErrorMessage = "Please enter a number between {1} and {2}.")]
        public int? NumOfCustomersChild { get; set; }


        public decimal SubTotalPreTax { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal SubTotalAfterTax { get; set; }

        [Required(ErrorMessage = "Please enter a number of at least 0")]
        [Range(0, 1000.00, ErrorMessage = "The tip amount entered must be equal to or greater than ${1}")]
        public decimal? TipAmount { get; set; }


        //NOTE: Next two variables if turned nullable created problems with ModelState.IsValid of InsertSalesTransactionToDatabase Method
        [Required(ErrorMessage = "Please enter a valid tax rate between 0 and 1 up to 4 decimal places")]
        [Range(0.0000, 1.0000, ErrorMessage = "The tip amount entered {0} must be greater than {1} and less than {2}.")]
        public decimal TaxRateFractionalEquivalent { get; set; }



        [Required(ErrorMessage = "Please enter a valid total transaction amount up to 2 decimal places")]
        [Range(0.00, 1000000.00, ErrorMessage = "The tip amount entered {0} must be greater than {1} and less than {2}.")]
        public decimal FinalTransactionAmount { get; set; }



        public string? PaymentMethod { get; set; }
        public IEnumerable<PaymentMethodCategory>? PaymentMethodsList { get; set; }


        public DateTime FinalTransactionDateAndTime { get; set; }
        public int EmployeeID { get; set; }

        public int RestaurantTableID { get; set; }

        public string? CustomerLogoutPassword { get; set; }

        public bool PasswordIncorrect { get; set; }

        public IEnumerable<FoodBevOrder>? OrderList { get; set; } //Note this is null

        public IEnumerable<Employee>? ServerList { get; set; } //Note this is null

        public IEnumerable<RestaurantTable>? RestaurantTableList { get; set; } //Note this is null

        public int? OrderID1 { get; set; }
        public decimal? OrderPrice1 { get; set; }

        public int? OrderID2 { get; set; }
        public decimal? OrderPrice2 { get; set; }

        public int? OrderID3 { get; set; }
        public decimal? OrderPrice3 { get; set; }

        public int? OrderID4 { get; set; }
        public decimal? OrderPrice4 { get; set; }

        public int? OrderID5 { get; set; }
        public decimal? OrderPrice5 { get; set; }

        public int? OrderID6 { get; set; }
        public decimal? OrderPrice6 { get; set; }

        public int? OrderID7 { get; set; }
        public decimal? OrderPrice7 { get; set; }

        public int? OrderID8 { get; set; }
        public decimal? OrderPrice8 { get; set; }

        public int? OrderID9 { get; set; }
        public decimal? OrderPrice9 { get; set; }

        public int? OrderID10 { get; set; }
        public decimal? OrderPrice10 { get; set; }

        public int? OrderID11 { get; set; }
        public decimal? OrderPrice11 { get; set; }

        public int? OrderID12 { get; set; }
        public decimal? OrderPrice12 { get; set; }

        public int? OrderID13 { get; set; }
        public decimal? OrderPrice13 { get; set; }

        public int? OrderID14 { get; set; }
        public decimal? OrderPrice14 { get; set; }

        public int? OrderID15 { get; set; }
        public decimal? OrderPrice15 { get; set; }

        public int? OrderID16 { get; set; }
        public decimal? OrderPrice16 { get; set; }

        public int? OrderID17 { get; set; }
        public decimal? OrderPrice17 { get; set; }

        public int? OrderID18 { get; set; }
        public decimal? OrderPrice18 { get; set; }

        public int? OrderID19 { get; set; }
        public decimal? OrderPrice19 { get; set; }

        public int? OrderID20 { get; set; }
        public decimal? OrderPrice20 { get; set; }
    }
}
