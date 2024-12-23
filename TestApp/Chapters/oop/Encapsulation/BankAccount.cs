namespace TestApp.Chapters.oop.Encapsulation;

public class BankAccount
{

    public string AccountNumber { get; private set; }
    public decimal Balance
    {
        get { return Balance; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("Balance cannot be negative.");
            Balance = value;
        }
    }

    public BankAccount(string accountNumber, decimal initialBalance)
    {
        this.AccountNumber = accountNumber;
        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative.");
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive.");
        Balance += amount;
        Console.WriteLine($"Deposited {amount:C}. New Balance: {Balance:C}");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be positive.");
        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
        Console.WriteLine($"Withdrew {amount:C}. New Balance: {Balance:C}");
    }

    public BankAccount SetBalance(decimal balance)
    {
        Balance = balance;

        return this;
    }
}
