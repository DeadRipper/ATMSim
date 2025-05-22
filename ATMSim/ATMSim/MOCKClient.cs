using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSim
{
    internal class MOCKClient
    {
        public Account GetAccount()
        {
            return new Account()
            {
                Balance = 1500,
                Name = "Test",
                Pin = 0000
            };
        }
    }
}