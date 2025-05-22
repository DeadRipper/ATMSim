using System.Reflection.Metadata;

namespace ATMSim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to the ATM ===");
            Console.WriteLine("Enter your PIN: ");

            ATMCore atm = new ATMCore();
            atm.Start();
        }
    }
}