using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class BankManager
    {
        private AccountsManager _accountsManager;
        private IPrinter _printer;
        private List<CustomerData> _customerData;

        public BankManager()
        {
            _accountsManager = new AccountsManager();
            _printer = new Printer();
            _customerData = new List<CustomerData> { new("Jordan", "Belfort", "12121212121", "admin", "admin123"),
                                                     new("Donnie", "Azoff", "13131313131", "user", "user123")};
        }
        public void Run()
        {
            int action;
            do
            {
                PrintMainMenu();
                action = SelectedAction();

                switch (action)
                {
                    case 1:
                        ListOfAccounts();
                        break;
                    case 2:
                        AddBillingAccount();
                        break;
                    case 3:
                        Console.Clear();
                        AddSavingsAccount();
                        Console.ReadKey();
                        break;
                    case 4:
                        AddMoney();
                        break;
                    case 5:
                        Console.Clear();
                        TakeMoney();
                        Console.ReadKey();
                        break;
                    case 6:
                        ListOfCustomers();
                        break;
                    case 7:
                        ListOfAllAccounts();
                        break;
                    case 8:
                        CloseMonth();
                        break;
                    case 9:
                        Blik();
                        Console.ReadKey();
                        break;
                    case 10:
                        Converter();
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("Nieznane polecenie");
                        break;
                }
            }
            while (action != 0);
        }


        private void PrintLoginMenu()
        {

        }

        private void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Wybierz akcję:");
            Console.WriteLine("1 - Lista kont klienta");
            Console.WriteLine("2 - Dodaj konto rozliczeniowe");
            Console.WriteLine("3 - Dodaj konto oszczędnościowe");
            Console.WriteLine("4 - Wpłać pieniądze na konto");
            Console.WriteLine("5 - Wypłać pieniądze z konta");
            Console.WriteLine("6 - Lista klientów");
            Console.WriteLine("7 - Wszystkie konta");
            Console.WriteLine("8 - Zakończ miesiąc");
            Console.WriteLine("9 - BLIK");
            Console.WriteLine("10 - Exchange money");
            Console.WriteLine("0 - Zakończ");
        }

        private int SelectedAction()
        {
            Console.Write("Akcja: ");
            string action = Console.ReadLine();
            if (string.IsNullOrEmpty(action))
            {
                return -1;
            }
            return int.Parse(action);
        }

        private void ListOfAccounts()
        {
            Console.Clear();
            CustomerData data = ReadCustomerData();
            Console.WriteLine();
            Console.WriteLine("Konta klienta {0} {1} {2}", data.FirstName, data.LastName, data.Pesel);

            foreach (Account account in _accountsManager.GetAllAccountsFor(data.FirstName, data.LastName, data.Pesel))
            {
                _printer.Print(account);
            }
            Console.ReadKey();
        }
        public void Login()
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
                            Run();
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

                    foreach(CustomerData customer in _customerData)
                    {
                        if(username == customer.Username)
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
        }

        private CustomerData ReadCustomerData()
        {
            string firstName;
            string lastName;
            string pesel;
            string username;
            string password;
            Console.WriteLine("Podaj dane klienta:");
            Console.Write("Imię: ");
            firstName = Console.ReadLine();
            Console.Write("Nazwisko: ");
            lastName = Console.ReadLine();
            Console.Write("PESEL: ");
            pesel = Console.ReadLine();
            Console.Write("Username: ");
            username = Console.ReadLine();
            Console.Write("Password: ");
            password = Console.ReadLine();

            return new CustomerData(firstName, lastName, pesel, username, password);
        }

        private void AddBillingAccount()
        {
            Console.Clear();
            CustomerData data = ReadCustomerData();
            Account billingAccount = _accountsManager.CreateBillingAccount(data.FirstName, data.LastName, data.Pesel);

            Console.WriteLine("Utworzono konto rozliczeniowe:");
            _printer.Print(billingAccount);
            Console.ReadKey();
        }

        private void AddSavingsAccount()
        {
            Console.Clear();
            CustomerData data = ReadCustomerData();
            Account savingsAccount = _accountsManager.CreateSavingsAccount(data.FirstName, data.LastName, data.Pesel);

            Console.WriteLine("Utworzono konto rozliczeniowe:");
            _printer.Print(savingsAccount);
            Console.ReadKey();
        }

        public void Converter()
        {
            string amount, from, to;
            FixerApi fixer = new FixerApi();

            Console.Clear();
            Console.Write("Amount: ");
            amount = Console.ReadLine();
            Console.Write("From: ");
            from = Console.ReadLine();
            Console.Write("To: ");
            to = Console.ReadLine();

            var result = fixer.Convert(amount, from, to);

            Console.WriteLine(result);
            Console.ReadKey();
        }

        private void CloseMonth()
        {
            Console.Clear();
            _accountsManager.CloseMonth();
            Console.WriteLine("Miesiąc zamknięty");
            Console.ReadKey();
        }

        private void ListOfAllAccounts()
        {
            Console.Clear();
            Console.WriteLine("Wszystkie konta:");
            foreach (Account account in _accountsManager.GetAllAccounts())
            {
                _printer.Print(account);
            }
            Console.ReadKey();
        }

        private void ListOfCustomers()
        {
            Console.Clear();
            Console.WriteLine("Lista klientów:");
            foreach (string customer in _accountsManager.ListOfCustomers())
            {
                Console.WriteLine(customer);
            }
            Console.ReadKey();
        }

        private void AddMoney()
        {
            string accountNo;
            decimal value;

            Console.WriteLine("Wpłata pieniędzy");
            Console.Write("Numer konta: ");
            accountNo = Console.ReadLine();
            Console.Write("Kwota: ");
            value = decimal.Parse(Console.ReadLine());
            _accountsManager.AddMoney(accountNo, value);

            Account account = _accountsManager.GetAccount(accountNo);
            _printer.Print(account);

            Console.ReadKey();
        }

        private void TakeMoney()
        {
            string accountNo;
            decimal value;

            Console.WriteLine("Wypłata pieniędzy");
            Console.Write("Numer konta: ");
            accountNo = Console.ReadLine();
            Console.Write("Kwota: ");
            value = decimal.Parse(Console.ReadLine());
            _accountsManager.TakeMoney(accountNo, value);

            Account account = _accountsManager.GetAccount(accountNo);
            _printer.Print(account);

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
            Console.WriteLine("Wybierz akcję:");
            Console.WriteLine("1 - Generate new BLIK code");
            Console.WriteLine("2 - Main menu");

            int action = SelectedAction();
            if (action == 1)
            {
                Blik();
            }
            else
            {
                PrintMainMenu();
            }

        }
    }
}
