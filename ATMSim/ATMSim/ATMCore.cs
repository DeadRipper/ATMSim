using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ATMSim
{
    public class ATMCore
    {
        public void Start()
        {
            Account acc = null;
            try
            {
                acc = Authenticate();
                if (acc == null)
                {
                    Console.WriteLine($"inncorrect pin");
                    return;
                }
            }
            catch(Exception ex)
            {
                return;
            }

            while (true)
            {
                try
                {
                    ShowMenu();
                    Actions(acc);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Authenticate error :: {ex}");
                }
            }
        }

        private void Actions(Account acc)
        {
            Console.Write("Enter your choice: ");
            try
            {
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        ShowBalance(acc);
                        break;
                    case 2:
                        Deposit(acc);
                        break;
                    case 3:
                        Withdraw(acc);
                        break;
                    case 4:
                        Console.WriteLine("Thank you for using the ATM. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"inncorrect action :: {ex}");
            }
        }

        private Account Authenticate()
        {
            int attempts = 0;
            var testAcc = new MOCKClient().GetAccount();
            while (attempts < 3)
            {
                try
                {
                    if (int.Parse(Console.ReadLine()) == testAcc.Pin)
                    {
                        Console.WriteLine("Authentication successful.\n");
                        return testAcc;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect PIN.");
                        attempts++;
                        if (attempts < 3)
                        {
                            Console.WriteLine("Enter your PIN: ");
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Incorrect PIN format");
                    attempts++;
                    Console.WriteLine("Enter your PIN: ");
                }
            }

            if (attempts == 3)
            {
                Console.WriteLine("Too many failed attempts. Exiting...");
                return null;
            }

            return null;
        }

        private void ShowMenu()
        {
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Exit");
        }

        private void ShowBalance(Account acc)
        {
            Console.WriteLine($"Your current balance is: ${acc.Balance}");
        }

        private void Deposit(Account acc)
        {
            Console.Write("Enter deposit amount: $");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                acc.Balance += amount;
                Console.WriteLine($"Deposited ${amount}. New balance is ${acc.Balance}");
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        private void Withdraw(Account acc)
        {
            Console.Write("Enter withdrawal amount: $");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                if (amount <= acc.Balance)
                {
                    acc.Balance -= amount;
                    Console.WriteLine($"Withdrew ${amount}. New balance is ${acc.Balance}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
    }
}