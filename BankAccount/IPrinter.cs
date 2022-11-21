using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal interface IPrinter
    {
        void Print(Account account);
        void PrintCustomerMainMenu();
        void PrintAdminMainMenu();
        void PrintHistory(Transaction transaction);
    }
}
