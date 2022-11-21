using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BankAccount
{
    internal interface IFixerApi
    {
        public decimal Convert(decimal amount, string from, string to);
        public void Latest(string currance);
    }
}
