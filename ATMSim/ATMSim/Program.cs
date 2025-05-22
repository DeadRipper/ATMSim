using System.Reflection.Metadata;

namespace ATMSim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ATM atm = new ATM();
            atm.Start();
        }
    }

    class ATM
    {
        Dictionary<int, Account> accounts = new Dictionary<int, Account>
        {
            {1234, new Account {Pin = 1234, Name = "Alice", Balance = 1500}},
            {4321, new Account {Pin = 4321, Name = "Bob", Balance = 900}},
        };

        public void Start()
        {
            Console.WriteLine("=== Welcome to the ATM ===");
            Console.Write("Enter your PIN: ");
            var pin = int.TryParse(Console.ReadLine(), out int enteredPin);

            if (Authenticate(enteredPin))
            {
                var userAcc = accounts.Where(x => x.Value.Pin == enteredPin).Select(x => x.Value).First();

                int choice;
                do
                {


                    ShowMenu();
                    Console.Write("Enter your choice: ");
                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            ShowBalance(userAcc);
                            break;
                        case 2:
                            Deposit(userAcc);
                            break;
                        case 3:
                            Withdraw(userAcc);
                            break;
                        case 4:
                            Console.WriteLine("Thank you for using the ATM. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    Console.WriteLine();
                } while (choice != 4);
            }
        }

        private bool Authenticate(int pin)
        {
            int attempts = 0;

            while (attempts < 3)
            {
                
                if (pin == accounts.Select(x => x.Value.Pin).First())
                {
                    Console.WriteLine("Authentication successful.\n");
                    return true;
                }
                else
                {
                    Console.WriteLine("Incorrect PIN.");
                    attempts++;
                }
            }

            if (attempts == 3)
            {
                Console.WriteLine("Too many failed attempts. Exiting...");
                return false;
            }
            return false;
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
