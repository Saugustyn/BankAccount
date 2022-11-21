using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class Transaction
    {
        public string AccountNumber;
        public long Pesel;
        public decimal Value;
        public string Currency;
        public DateTime Date;
        public string Type;

        public Transaction(long pesel, decimal value, string currency, string type, string accountNumber)
        {
            Pesel = pesel;
            Value = value;
            Date = DateTime.Now;
            Currency = currency;
            Type = type;
            AccountNumber = accountNumber;
        }
    }
}
