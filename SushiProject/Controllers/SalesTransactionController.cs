using Microsoft.AspNetCore.Mvc;
using SushiProject.Models;
using System.Data.Common;
using System.Transactions;

namespace SushiProject.Controllers
{
    public class SalesTransactionController : Controller
    {
        private readonly ISalesTransactionRepository repo;

        public SalesTransactionController(ISalesTransactionRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            var transactions = repo.GetAllSalesTransactionsSQL();
            return View(transactions);
        }

        public IActionResult ViewSalesTransaction(int transactionID)
        {
            var transaction = repo.GetSalesTransactionSQL(transactionID);
            return View(transaction);
        }

        public IActionResult UpdateSalesTransaction(int id)
        {
            SalesTransaction updateTransaction = repo.GetSalesTransactionSQL(id);

            var orderTrans = repo.GetOrderListSQL();
            updateTransaction.OrderList = orderTrans;

            var serverTrans = repo.GetServerListSQL();
            updateTransaction.ServerList = serverTrans;

            var tableTrans = repo.GetAllRestaurantTableListSQL();
            updateTransaction.RestaurantTableList = tableTrans;

            var paymentTrans = repo.GetPaymentMethodsListSQL();
            updateTransaction.PaymentMethodsList = paymentTrans;

            if (updateTransaction == null)
            {
                return View("ProductNotFound");
            }
            return View("UpdateSalesTransaction", updateTransaction);
        }

        public IActionResult UpdateSalesTransactionToDatabase(SalesTransaction transactionToUpdate)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateSalesTransactionToDatabaseSQL(transactionToUpdate);
                repo.UpdateOrderPricesInDatabaseSQL(transactionToUpdate);

                SalesTransaction updatedTransaction = repo.CalculateSubTotalAmountSQL(transactionToUpdate.SalesTransactionID);
                repo.CalculateFinalTransactionAmountSQL(updatedTransaction);

                return RedirectToAction("ViewSalesTransaction", new { transactionID = updatedTransaction.SalesTransactionID });
            }
            return View("UpdateSalesTransaction", transactionToUpdate);
        }
        public IActionResult CreateSalesTransaction1()
        {
            var transaction = repo.AssignServerListSQL();
            return View("CreateSalesTransaction1View", transaction);
        }
        public IActionResult CreateSalesTransaction2(SalesTransaction transaction)
        {
            var tempTransaction = repo.AssignRestaurantTableListSQL(transaction.EmployeeID);
            transaction.RestaurantTableList = tempTransaction.RestaurantTableList;
            return View("CreateSalesTransaction2View", transaction);
        }

        [HttpPost]
        public IActionResult InsertSalesTransactionToDatabase(SalesTransaction transactionToInsert)
        {
            transactionToInsert.FinalTransactionDateAndTime = DateTime.Now;

            transactionToInsert = repo.CreateShellSalesTransactionSQL(transactionToInsert);

            if (ModelState.IsValid)
            {
                repo.InsertSalesTransactionSQL(transactionToInsert);
                return RedirectToAction("Index");
            }
            else
            {
                return View("CreateSalesTransaction2View", transactionToInsert);
            }
        }
        public IActionResult DeleteSalesTransaction(SalesTransaction transaction)
        {
            repo.DeleteSalesTransactionSQL(transaction);
            return RedirectToAction("Index");
        }
        public IActionResult SetupNewCustomer1()
        {
            var transaction = repo.AssignServerListSQL();
            return View("SetupNewCustomerV1", transaction);
        }
        public IActionResult SetupNewCustomer2(SalesTransaction transaction)
        {
            var tempTransaction = repo.AssignRestaurantTableListSQL(transaction.EmployeeID);
            transaction.RestaurantTableList = tempTransaction.RestaurantTableList;
            return View("SetupNewCustomerV2", transaction);
        }
        public IActionResult SetupNewCustomer3(SalesTransaction transaction)
        {
            var tempTransaction = repo.GetInitialSalesTransactionSQL(transaction);
            return View("SetupNewCustomerV3", tempTransaction);
        }
        public IActionResult CustomerHomePage(int transactionID)
        {
            var transaction = repo.GetSalesTransactionSQL(transactionID);
            return View("CustomerHomePage", transaction);
        }
        public IActionResult PayFinalBill(int transactionID)
        {
            var transaction = repo.CalculateSubTotalAmountSQL(transactionID);
            var paymentMethodsList = repo.GetPaymentMethodsListSQL();
            transaction.PaymentMethodsList = paymentMethodsList;
            return View("CustomerPayBill", transaction);
        }
        public IActionResult CompleteSalesTransaction(SalesTransaction transaction)
        {
            if (transaction.TipAmount == null)
            { 
                transaction.TipAmount = 0;
            }

            if (ModelState.IsValid)
            {
                repo.CompleteSalesTransactionSQL(transaction);
                var fullTransaction = repo.GetSalesTransactionSQL(transaction.SalesTransactionID);
                return View("CustomerPaymentReceipt", fullTransaction);
            }
            else
            {
                var paymentMethodsList = repo.GetPaymentMethodsListSQL();
                transaction.PaymentMethodsList = paymentMethodsList;
                return View("CustomerPayBill", transaction);
            }
        }
        public IActionResult CloseOutCustomer(int transactionID)
        {
            var transaction = repo.GetSalesTransactionSQL(transactionID);
            return View("LogoutCustomer", transaction);
        }
        public IActionResult ShowFinalReceipt(int transactionID)
        {
            var transaction = repo.GetSalesTransactionSQL(transactionID);
            return View("CustomerPaymentReceipt", transaction);
        }
        public IActionResult CheckPassword (SalesTransaction transaction)
        {
            bool success = repo.CheckCustomerLogoutPasswordSQL(transaction.CustomerLogoutPassword);
            if(success == true)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                transaction.PasswordIncorrect = true;
                return View("LogoutCustomer", transaction);
            }
        }
    }
}
