using BankAccount;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        BankManager bankManager = new BankManager();
        bankManager.Run();

    }
}