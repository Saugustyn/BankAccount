using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class AccountsManager
    {
        private IList<Account> _accounts;

        public AccountsManager()
        {
            _accounts = new List<Account>{ new SavingsAccount(1, "Jordan", "Belfort", 12121212121),
                                           new SavingsAccount( 2 ,"Donnie", "Azoff", 13131313131)};
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accounts;
        }

        public SavingsAccount CreateSavingsAccount(string firstName, string lastName, long pesel)
        {
            int id = generateId();

            SavingsAccount account = new SavingsAccount(id, firstName, lastName, pesel);

            _accounts.Add(account);

            return account;
        }

        public BillingAccount CreateBillingAccount(string firstName, string lastName, long pesel)
        {
            int id = generateId();

            BillingAccount account = new BillingAccount(id, firstName, lastName, pesel);

            _accounts.Add(account);

            return account;
        }

        public IEnumerable<Account> GetAllAccountsFor(string firstName, string lastName, long pesel) 
        {
            return _accounts.Where(x => x.FirstName == firstName && x.LastName == lastName && x.Pesel == pesel); 
        }

        public Account GetAccount(string accountNo) 
        {
            try
            {
                return _accounts.Single(x => x.AccountNumber == accountNo);
            }
            catch(Exception e)
            {
                Console.Clear();
                Console.WriteLine("Account ID is incorect, try again");
            }
            return null;
            
        }

        public IEnumerable<string> ListOfCustomers() 
        {
            return _accounts.Select(a => string.Format("Firstname: {0} | Lastname: {1} | PESEL: {2}", a.FirstName, a.LastName, a.Pesel)).Distinct(); 
        }

        public void CloseMonth() 
        { 
            if(_accounts.Count > 0)
            {
                foreach (SavingsAccount account in _accounts.Where(x => x is SavingsAccount))
                {
                    account.AddInterest(0.04M);
                }
                foreach (BillingAccount account in _accounts.Where(x => x is BillingAccount))
                {
                    account.TakeCharge(5.0M);
                }
            }
            else Console.WriteLine("No accounts");
        }

        public void AddMoney(string accountNo, decimal value)
        {
            Account account = GetAccount(accountNo);
            account.ChangeBalance(value);
        }
        public void TakeMoney(string accountNo, decimal value)
        {
            Account account = GetAccount(accountNo);
            account.ChangeBalance(-value);
        }

        private int generateId()
        {
            int id = 1;

            if (_accounts.Any())
            {
                id = _accounts.Max(x => x.Id) + 1;
            }
            return id;
        }
    }
}
