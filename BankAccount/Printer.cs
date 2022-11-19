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
            Console.WriteLine("Dane konta: {0}", account.AccountNumber);
            Console.WriteLine("Typ: {0}",  account.GetType());
            Console.WriteLine("Saldo: {0}zł", account.Balance);
            Console.WriteLine("Imię właściciela: {0}", account.FirstName);
            Console.WriteLine("Nazwisko właściciela: {0}", account.LastName);
            Console.WriteLine("PESEL właściciela: {0}", account.Pesel);
            Console.WriteLine();
        }

    }
}
