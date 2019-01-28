using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_6
{
    class Member
    {
        private string MemberName;
        //I made the MemberName private, so that it can only be accessed through methods. The name is not going to change except when it is initialized, so there doesn't need to be a function
        //to change the name, and there only needs to be a function to get the name
        private List<Account> BankAccounts { get; set;}
        //I set the BankAccount list to be private, since this is sensitive information. Setting the list as private makes it so that it can only be edited or viewed through a method in the class
        public Member (string MemberName, Account BankAccount)
        {
            BankAccounts = new List<Account>();
            this.MemberName = MemberName;
            BankAccounts.Add(BankAccount);

        }
        public string GetMemberName()
        {
            return MemberName;
        }
        public void AddAccount(Account BankAccount)
        {
            BankAccounts.Add(BankAccount);
        }
        public void CloseAccount(Account BankAccount)
        {
            BankAccount.InActive();
        }
        public List<Account> getAccounts()
        {
            return BankAccounts;
        }
    }
    class Account
    {
        private string AccountType;
        private string AccountID;
        //The AccountType and AccountID are going to be set in the constructor and then doesn't need to be changed again. 
        private double AccountBalance { get; set; }
        private int TransactionCount { get; set; }
        //The AccountBalance and the TransactionCount would normally only be changed in a database, but to simulate this, we need to be able to change the AccountBalance and TransactionCount
        //through the code. They are both set to private so that they can only be edited through methods in the class
        private bool Active { get; set; }

        public Account ( string AccountType, double AccountBalance, string AccountID )
        {
            this.AccountType = AccountType;
            this.AccountBalance = AccountBalance;
            this.TransactionCount = 1;
            this.AccountID = AccountID;
            this.Active = true;
        }
        public void InActive()
        {
            this.Active = false;
        }
        public string GetAccountType()
        {
            if (this.Active == false)
                return AccountType + " This Account is Inactive.";
            else return AccountType;
        }
       
        public string GetAccountBalance()
        {
            if (this.Active == false)
                return "N/A (Inactive Account)";
            else return AccountBalance.ToString();
        }
        
        public void AddFunds(double Funds)
        {
            AccountBalance += Funds;
            TransactionCount += 1;
        }

        public string CheckBalance()
        {
            if (this.Active == false)
                return "N/A (Inactive Account)";
            else return AccountBalance.ToString();
        }

        public int GetTransactionCount()
        {
            return TransactionCount;
        }
        public virtual void PrintBalance()
        {
            Console.WriteLine(AccountBalance);
        }
    }
    class Checking
    {

    }
    class Savings
    {

    }
    class Bank
    {
        private string BankName; 
        //The BankName only needs to be set in the constructor, then it doesn't get changed again. 
        private List<Member> MemberList { get; set; }
        //the list is private information for the bank, therefore it should be private. It can only be accessed through a method in the class
        public Bank(string BankName)
        {
            this.BankName = BankName;
            MemberList = new List<Member>();
        }
        public void AddMember(Member MemberName)
        {
            MemberList.Add(MemberName);
        }
        public string GetBankName()
        {
            return BankName;
        }
        
        public List<Member> ListAllMembers()
        {

            return MemberList;
        } 
        public int GetAccountTypeCount(string AccountType)
        {
            int count = 0;
            foreach(Member Member in MemberList)
            {
                foreach (Account Account in Member.getAccounts())
                    if (Account.GetAccountType() == AccountType)
                        count += 1;
            }
            return count;
        }
        public int GetTransactionCount()
        {
            int count = 0;
            foreach(Member Member in MemberList)
            {
                foreach (Account Account in Member.getAccounts())
                    count += Account.GetTransactionCount();
            }
            return count;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            //Creating Accounts
            Account Account1 = new Account("checking", 50.33, "111"); //creating an account with money in it counts as a transaction, so the creation of each account also gives each account 1 transaction
            Account Account2 = new Account("savings", 1100.33, "222");
            Account Account3 = new Account("checking", 599.2, "333");
            Account Account4 = new Account("savings", 83.99, "444");
            Account Account5 = new Account("checking", 88888.88, "555");
            Account Account6 = new Account("savings", 33.88, "666");
            Account Account7 = new Account("checking", 100.10, "777");
            Account Account8 = new Account("savings", 83.66, "888");
            Account Account9 = new Account("checking", 22.30, "999");
            //Creating Members, and assigning each member an account
            Member Luke = new Member("Luke", Account1);  //Luke now has Account1, which has 1 transaction so far
            Member Julie = new Member("Julie", Account3); //Julie has Account2, which has 1 transaction so far
            Member Ken = new Member("Ken", Account4); 
            Member Lisa = new Member("Lisa", Account5);
            Member Sierrah = new Member("Sierrah", Account6);
            //Assigning additional accounts to members
            Luke.AddAccount(Account2); //Luke now has 2 accounts, each with 1 transaction
            Luke.AddAccount(Account9); //Luke now has 3 accounts, each with 1 transaction
            Ken.AddAccount(Account7); //Ken now has 2 accounts, each with 1 transaction
            Lisa.AddAccount(Account8); //Lisa now has 2 accounts, each with 1 transaction
            //Adding some funds to each account, each time funds are added, it creates a transaction
            Account1.AddFunds(200.20); //This is another transaction with Luke's account (4 transactions total)
            Account1.AddFunds(33.20); // 5 transactions total with Luke's account
            Account2.AddFunds(88.2);
            Account3.AddFunds(50.00);
            Account4.AddFunds(88.88);
            Account5.AddFunds(375);
            Account6.AddFunds(220);
            Account7.AddFunds(38);
            Account8.AddFunds(3.00);
            //Creating the Banks
            Bank WellsFargo = new Bank("WellsFargo");
            Bank USBank = new Bank("US Bank");
            //Assigning different members to each bank
            WellsFargo.AddMember(Luke);
            WellsFargo.AddMember(Julie);
            WellsFargo.AddMember(Ken);
            USBank.AddMember(Lisa);
            USBank.AddMember(Sierrah);
            Console.WriteLine("List of all US Bank Members: ");
            foreach (Member Member in USBank.ListAllMembers())
                Console.WriteLine(Member.GetMemberName());
            Console.WriteLine("\r\n" + "List of all Wells Fargo Members: ");
            //Create a foreach loop which will list all of the members in the Well's Fargo Bank
            foreach (Member Member in WellsFargo.ListAllMembers())
            {
                Console.WriteLine(Member.GetMemberName());
            }
            Console.WriteLine("The number of checking accounts in Wells Fargo: ");
            Console.WriteLine(WellsFargo.GetAccountTypeCount("checking"));
            Console.WriteLine("The number of savings accounts in Wells Fargo: ");
            Console.WriteLine(WellsFargo.GetAccountTypeCount("savings"));
            Console.WriteLine("The number of transactions in Wells Fargo: ");
            Console.WriteLine(WellsFargo.GetTransactionCount());
            Console.WriteLine("\r\n" + "Breakdown of Wells Fargo Members and accounts: ");
            Console.WriteLine("Luke's accounts: ");
            //Assigning Account 2 to be InActive
            Account2.InActive();
            foreach (Account Account in Luke.getAccounts())
            {
                Console.WriteLine(Account.GetAccountType() + " balance: $" + Account.GetAccountBalance() + " transactions: " + Account.GetTransactionCount());
            }
            Console.WriteLine("\r\n" + "Julie's accounts: ");
            foreach (Account Account in Julie.getAccounts())
            {
                Console.WriteLine(Account.GetAccountType() + " balance: $" + Account.GetAccountBalance() + " transactions: " + Account.GetTransactionCount());
            }
            Console.WriteLine("\r\n" + "Ken's accounts: ");
            foreach (Account Account in Ken.getAccounts())
            {
                Console.WriteLine(Account.GetAccountType() + " balance: $" + Account.GetAccountBalance() + " transactions: " + Account.GetTransactionCount());
            }
            Console.ReadLine();

        }
    }
}
