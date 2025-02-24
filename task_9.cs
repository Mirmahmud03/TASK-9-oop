using System;
using System.Collections.Generic;

class BankAccount
{
    public string AccountNumber { get; private set; }
    public double Balance { get; private set; }

    public BankAccount(string accountNumber, double initialBalance = 0)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            Console.WriteLine($"{amount} so'm hisobga qo'shildi. Yangi balans: {Balance} so'm");
        }
    }

    public bool Withdraw(double amount)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
            Console.WriteLine($"{amount} so'm yechildi. Yangi balans: {Balance} so'm");
            return true;
        }
        Console.WriteLine("Yetarli mablag' mavjud emas!");
        return false;
    }
}

class Customer
{
    public string Name { get; private set; }
    public BankAccount Account { get; private set; }

    public Customer(string name, string accountNumber, double initialBalance = 0)
    {
        Name = name;
        Account = new BankAccount(accountNumber, initialBalance);
    }
}

class Bank
{
    private Dictionary<string, Customer> customers = new Dictionary<string, Customer>();

    public void OpenAccount(string name, string accountNumber, double initialBalance = 0)
    {
        if (!customers.ContainsKey(accountNumber))
        {
            customers[accountNumber] = new Customer(name, accountNumber, initialBalance);
            Console.WriteLine($"{name} uchun {accountNumber} raqamli hisob ochildi.");
        }
    }

    public void CloseAccount(string accountNumber)
    {
        if (customers.ContainsKey(accountNumber))
        {
            customers.Remove(accountNumber);
            Console.WriteLine($"{accountNumber} raqamli hisob yopildi.");
        }
    }

    public void Transfer(string fromAccount, string toAccount, double amount)
    {
        if (customers.ContainsKey(fromAccount) && customers.ContainsKey(toAccount))
        {
            if (customers[fromAccount].Account.Withdraw(amount))
            {
                customers[toAccount].Account.Deposit(amount);
                Console.WriteLine($"{fromAccount} dan {toAccount} ga {amount} so'm o'tkazildi.");
            }
        }
        else
        {
            Console.WriteLine("Hisoblardan biri topilmadi!");
        }
    }

    public void ShowBalance(string accountNumber)
    {
        if (customers.ContainsKey(accountNumber))
        {
            Console.WriteLine($"{customers[accountNumber].Name}ning balans: {customers[accountNumber].Account.Balance} so'm");
        }
    }
}

class BankApp
{
    static void Main()
    {
        Bank bank = new Bank();
        
        bank.OpenAccount("Ali", "12345", 100000);
        bank.OpenAccount("Vali", "67890", 50000);
        
        bank.ShowBalance("12345");
        bank.ShowBalance("67890");

        bank.Transfer("12345", "67890", 20000);
        
        bank.ShowBalance("12345");
        bank.ShowBalance("67890");
    }
}
