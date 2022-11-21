using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class Printer : IPrinter
    {
        public void Print(Account account)
        {
            Console.WriteLine("Account number: {0}", account.AccountNumber);
            Console.WriteLine("Type: {0}",  account.GetType());
            Console.WriteLine("Balance: {0}zł", account.Balance);
            Console.WriteLine("Owner's fisrtname: {0}", account.FirstName);
            Console.WriteLine("Owner's lastname: {0}", account.LastName);
            Console.WriteLine("Owner's PESEL: {0}", account.Pesel);
            Console.WriteLine();
        }

        public void PrintCustomerMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose action:");
            Console.WriteLine("0 - End");
            Console.WriteLine("1 - Your accounts");
            Console.WriteLine("2 - Open Billing Account");
            Console.WriteLine("3 - Open Savings Account");
            Console.WriteLine("4 - Deposit money");
            Console.WriteLine("5 - Withdraw money");
            Console.WriteLine("6 - BLIK");
            Console.WriteLine("7 - History");
        }

        public void PrintAdminMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose action:");
            Console.WriteLine("0 - End");
            Console.WriteLine("1 - Customers list");
            Console.WriteLine("2 - All accounts list");
            Console.WriteLine("3 - Close month");
            Console.WriteLine("4 - Exchange rate data");
        }

        public void PrintHistory(Transaction transaction)
        {
            Console.WriteLine($"---{transaction.AccountNumber}:{transaction.Value}{transaction.Currency} {transaction.Type} on {transaction.Date}");
        }

    }
}
