using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class BankManager
    {
        private AccountsManager _accountsManager;
        private IPrinter _printer;
        private List<CustomerData> _customerData;
        private List<Transaction> _history;

        public BankManager()
        {
            _accountsManager = new AccountsManager();
            _printer = new Printer();
            _customerData = new List<CustomerData> {new("Jordan", "Belfort", "12121212121", "admin", "admin123"),
                                                    new("Donnie", "Azoff", "13131313131", "user", "user123")};
            _history = new List<Transaction>();
        }
        public void Run()
        {
            Error:
            try
            {
                CustomerData customer = Login();
                int action;
                do
                {
                    if (customer.Username == "admin")
                    {
                        _printer.PrintAdminMainMenu();
                        action = SelectedAction();
                        switch (action)
                        {
                            case 1:
                                ListOfCustomers();
                                break;
                            case 2:
                                ListOfAllAccounts();
                                break;
                            case 3:
                                CloseMonth();
                                break;
                            case 4:
                                ExchangeRates();
                                break;
                            default:
                                Console.Write("Unkown Command");
                                break;
                        }

                    }
                    else
                    {
                        _printer.PrintCustomerMainMenu();
                        action = SelectedAction();
                        switch (action)
                        {
                            case 1:
                                ListOfAccountsFor(customer);
                                break;
                            case 2:
                                AddBillingAccount(customer);
                                break;
                            case 3:
                                AddSavingsAccount(customer);
                                break;
                            case 4:
                                AddMoney(customer);
                                break;
                            case 5:
                                TakeMoney(customer);
                                break;
                            case 6:
                                Blik();
                                break;
                            case 7:
                                History(customer);
                                break;
                            default:
                                Console.Write("Unkown Command");
                                break;
                        }
                    }
                }
                while (action != 0);
            }
            catch(Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Error");
                goto Error;
                Console.ReadKey();
            }
        }

        private int SelectedAction()
        {
            Console.Write("Action: ");
            string action = Console.ReadLine();
            if (string.IsNullOrEmpty(action))
            {
                return -1;
            }
            return int.Parse(action);
        }

        private void ListOfAccountsFor(CustomerData data)
        {

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Client accounts {0} {1} {2}", data.FirstName, data.LastName, data.Pesel);
            Console.WriteLine();

            foreach (Account account in _accountsManager.GetAllAccountsFor(data.FirstName, data.LastName, data.Pesel))
            {
                _printer.Print(account);
            }
            Console.ReadKey();
        }

        private void History(CustomerData data)
        {
            Console.Clear();
            foreach(Transaction transaction in _history.Where(x => x.Pesel == data.Pesel))
            {
                PrintHistory(transaction);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        public CustomerData Login()
        {
        Start:
            Console.Clear();
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Registration");
            var input = SelectedAction();
            bool successfull = false;
            while (!successfull)
            {
                if (input == 1)
                {
                Region:
                    Console.Clear();
                    Console.WriteLine("Write your username:");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter your password:");
                    string password = Console.ReadLine();

                    foreach (CustomerData customer in _customerData)
                    {
                        if (username == customer.Username && password == customer.Password)
                        {
                            Console.Clear();
                            Console.WriteLine("You have successfully logged in !!!");
                            Console.ReadLine();
                            successfull = true;
                            return customer;
                            break;
                        }
                    }
                    if (!successfull)
                    {
                        Console.Clear();
                        Console.WriteLine("Your username or password is incorect, try again !");
                        Console.ReadKey();
                        goto Region;
                    }
                }
                else if (input == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Enter your firstname:");
                    var firstname = Console.ReadLine();
                    Console.WriteLine("Enter your lastname:");
                    var lastname = Console.ReadLine();
                    Console.WriteLine("Enter your pesel:");
                    var pesel = Console.ReadLine();
                Regis:
                    Console.WriteLine("Enter your username:");
                    var username = Console.ReadLine();
                    Console.WriteLine("Enter your password:");
                    var password = Console.ReadLine();

                    foreach (CustomerData customer in _customerData)
                    {
                        if (username == customer.Username)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Your username is already taken !");
                            Console.WriteLine();
                            goto Regis;
                        }
                        else
                        {
                            CustomerData customerData = new CustomerData(firstname, lastname, pesel, username, password);
                            _customerData.Add(customerData);
                            Console.Clear();
                            Console.WriteLine("Welcome!");
                            Console.ReadKey();
                            goto Start;
                            return new CustomerData(firstname, lastname, pesel, username, password);
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Try again!");
                    Console.ReadKey();
                    goto Start;
                }
            }
            throw new Exception("Error");
        }

        private void AddBillingAccount(CustomerData data)
        {
            Console.Clear();
            Account billingAccount = _accountsManager.CreateBillingAccount(data.FirstName, data.LastName, data.Pesel);

            Console.WriteLine("Billing account opened:");
            _printer.Print(billingAccount);
            Console.ReadKey();
        }

        private void AddSavingsAccount(CustomerData data)
        {
            Console.Clear();
            Account savingsAccount = _accountsManager.CreateSavingsAccount(data.FirstName, data.LastName, data.Pesel);

            Console.WriteLine("Savings account opened:");
            _printer.Print(savingsAccount);
            Console.ReadKey();
        }
        public void ExchangeRates()
        {
            decimal currance;
            FixerApi fixer = new FixerApi();
            Console.Clear();
            Console.Write("Currance: ");
            currance = decimal.Parse( Console.ReadLine());

            fixer.Latest(currance);
            Console.ReadKey();

        }

        private void CloseMonth()
        {
            Console.Clear();
            _accountsManager.CloseMonth();
            Console.WriteLine("Month-end close done");
            Console.ReadKey();
        }

        private void ListOfAllAccounts()
        {
            Console.Clear();
            Console.WriteLine("All accounts list:");
            foreach (Account account in _accountsManager.GetAllAccounts())
            {
                _printer.Print(account);
            }
            Console.ReadKey();
        }

        private void ListOfCustomers()
        {
            Console.Clear();
            Console.WriteLine("Customers list:");
            foreach (string customer in _accountsManager.ListOfCustomers())
            {
                Console.WriteLine(customer);
            }
            Console.ReadKey();
        }

        private void AddMoney(CustomerData data)
        {
            string accountNo;
            decimal amount, value;
            string from;
            string to ="PLN";
            Console.Clear();
            Console.WriteLine("Deposit:");
            Console.Write("Account number: ");
            accountNo = Console.ReadLine();
            Console.Write("Amount: ");
            value = decimal.Parse(Console.ReadLine());
            Console.Write("Currency: ");
            from = Console.ReadLine();
            Account account = _accountsManager.GetAccount(accountNo);
            FixerApi fixer = new FixerApi();
            amount = fixer.Convert(value, from, to);

            if (account != null && account.Pesel == data.Pesel)
            {
                Console.WriteLine();
                _accountsManager.AddMoney(accountNo, amount);
                Transaction transaction = new Transaction(data.Pesel, value, from, "deposited", accountNo);
                _history.Add(transaction);
                _printer.Print(account);
            }
            Console.ReadKey();

        }

        private void PrintHistory(Transaction transaction)
        {
            Console.WriteLine($"---{transaction.AccountNumber}:{transaction.Value}{transaction.Currency} {transaction.Type} on {transaction.Date}");
        }
        private void TakeMoney(CustomerData data)
        {
            string accountNo;
            decimal amount, value;
            string from;
            string to = "PLN";
            Console.Clear();
            Console.WriteLine("Withdraw:");
            Console.Write("Account number: ");
            accountNo = Console.ReadLine();
            Console.Write("Amount: ");
            value = decimal.Parse(Console.ReadLine());
            Console.Write("Currency: ");
            from = Console.ReadLine();
            Account account = _accountsManager.GetAccount(accountNo);
            FixerApi fixer = new FixerApi();
            
            amount = fixer.Convert(value,from, to);

            if (account != null && account.Pesel == data.Pesel)
            {
                if (amount <= account.Balance && amount > 0)
                {
                    Console.WriteLine();
                    _accountsManager.TakeMoney(accountNo, amount);
                    Transaction transaction = new Transaction(data.Pesel, value, from, "withdrawed", accountNo);
                    _history.Add(transaction);
                    _printer.Print(account);
                }
                else if (amount == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Value must be larger than 0");
                }
                else if (amount > account.Balance)
                {
                    Console.WriteLine();
                    Console.WriteLine("Not enough money!");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Account ID is incorect, try again !");
            }
            Console.ReadKey();
        }

        public void Blik()
        {
            Console.Clear();

            int[] blikNumber = new int[6];
            Random random = new Random();

            for (int i = 0; i < blikNumber.Length; i++)
            {
                blikNumber[i] = random.Next(10);
            }
            Console.WriteLine(string.Join("", blikNumber));
            for (int a = 5; a >= 0; a--)
            {
                Console.Write("\rBlik Code expires in {0:00}sec", a);
                Thread.Sleep(1000);

            }

            Console.Clear();
            Console.WriteLine("Select an action:");
            Console.WriteLine("1 - Generate new BLIK code");
            Console.WriteLine("2 - Main menu");

            int action = SelectedAction();
            if (action == 1)
            {
                Blik();
            }
            else
            {
                _printer.PrintCustomerMainMenu();
            }
            Console.ReadKey();

        }
    }
}
