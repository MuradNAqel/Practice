namespace TestApp.Chapters.oop.Encapsulation;

public class EncapsulteBankAccount
{
    public static void RunBankAccountExample()
    {
        // Create a bank account
        BankAccount account = new BankAccount("123456789", 500.00m);

        // Display account number (read-only)
        Console.WriteLine($"Account Number: {account.GetAccountNumber()}");

        // Deposit money
        account.Deposit(200.00m);

        // Withdraw money
        account.Withdraw(100.00m);

        // Attempt to withdraw more than the balance
        try
        {
            account.Withdraw(700.00m);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        account.SetBalance(45);
    }
}

