namespace TestApp.Chapters.oop.Encapsulation;

public class BankAccount
{

    //property 
    // const
    // action
    private string accountNumber;
    private decimal balance;

    public BankAccount(string accountNumber, decimal initialBalance)
    {
        this.accountNumber = accountNumber;
        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative.");
        Balance = initialBalance;
    }

    public string GetAccountNumber()
    {
        return accountNumber;
    }

    public decimal Balance
    {
        get { return balance; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("Balance cannot be negative.");
            balance = value;
        }
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
