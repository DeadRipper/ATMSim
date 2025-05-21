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
        private decimal balance = 1000.00m;
        private int pin = 1234;
        private bool authenticated = false;

        public void Start()
        {
            Console.WriteLine("=== Welcome to the ATM ===");

            Authenticate();

            if (authenticated)
            {
                int choice;
                do
                {
                    ShowMenu();
                    Console.Write("Enter your choice: ");
                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            ShowBalance();
                            break;
                        case 2:
                            Deposit();
                            break;
                        case 3:
                            Withdraw();
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

        private void Authenticate()
        {
            int attempts = 0;

            while (attempts < 3)
            {
                Console.Write("Enter your PIN: ");
                if (int.TryParse(Console.ReadLine(), out int enteredPin) && enteredPin == pin)
                {
                    authenticated = true;
                    Console.WriteLine("Authentication successful.\n");
                    return;
                }
                else
                {
                    Console.WriteLine("Incorrect PIN.");
                    attempts++;
                }
            }

            Console.WriteLine("Too many failed attempts. Exiting...");
        }

        private void ShowMenu()
        {
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Exit");
        }

        private void ShowBalance()
        {
            Console.WriteLine($"Your current balance is: ${balance}");
        }

        private void Deposit()
        {
            Console.Write("Enter deposit amount: $");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                balance += amount;
                Console.WriteLine($"Deposited ${amount}. New balance is ${balance}");
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        private void Withdraw()
        {
            Console.Write("Enter withdrawal amount: $");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                if (amount <= balance)
                {
                    balance -= amount;
                    Console.WriteLine($"Withdrew ${amount}. New balance is ${balance}");
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
