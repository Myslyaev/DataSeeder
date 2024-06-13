using DataSeeder.Models;
using Bogus;
using DataSeeder.Enums;
using DataSeeder.Database;
using System.Runtime.CompilerServices;

namespace DataSeeder;

public class DataGenerator
{
    public static readonly List<TransactionDto> Transactions = [];
    //private static readonly Random _random = new();
    private const int numberOfLeads = 4000000;
    private const int percentDeposits = 20;
    private const int percentWithdraws = 10;
    private const int percentAll = 100;
    //private const string passwordLead = "Password_ENVIRONMENT";
    //private const string secret = "SecretPassword_ENVIRONMENT";
    //private const string provider = "crm.ru";

    //private static string RandomString(int length)
    //{
    //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    //    return new string(Enumerable.Repeat(chars, length)
    //        .Select(s => s[_random.Next(s.Length)]).ToArray());
    //}

    private static List<Account>GetAccounts()
    {
        var context = new CrmContext();
        return context.Accounts.ToList();
    }
    private static Faker<TransactionDto> GetTransactionGenerator(TransactionType transactionType)
    {
        //var (hash, salt) = PasswordsService.HashPassword(Environment.GetEnvironmentVariable(passwordLead), Environment.GetEnvironmentVariable(secret));
        //var counter = 0;
        var accounts = GetAccounts();
        return new Faker<TransactionDto>()
            .RuleFor(e => e.Id, _ => Guid.NewGuid())
            //.RuleFor(e => e.AccountId, f => f.PickRandom(accounts.)
            .RuleFor(e => e.Amount, f => f.Random.Int(100, 1000000))
            .RuleFor(e => e.Date, f => f.Address.StreetAddress())
            .RuleFor(e => e.BirthDate, f => f.Date.BetweenDateOnly(new DateOnly(1950, 1, 1), new DateOnly(2005, 1, 1)))
            .RuleFor(e => e.Status, _ => transactionType);
            //.RuleFor(e => e.Password, _ => hash)
            //.RuleFor(e => e.Salt, _ => salt);
    }
    
    public static void InitBogusData()
    {
        GeneratedLeads(numberOfLeads * percentDeposits/percentAll, TransactionType.Deposit);
        GeneratedLeads(numberOfLeads * percentWithdraws / percentAll, TransactionType.Withdraw);

        foreach (var lead in Leads)
        {
            lead.Accounts = GeneratedAccountsForLead(lead);
            if (lead.Status == LeadStatus.Vip)
            {
                lead.Accounts.AddRange(GeneratedAccountsForVipLead(lead));
            }
        }

    }

    private static void GeneratedLeads(int numberOfLeadsWithStatus, TransactionType transactionType =TransactionType.Unknown)
    {
        var leadGenerator = GetLeadGenerator(transactionType);
        var generatedLeads = leadGenerator.Generate(numberOfLeadsWithStatus);
        Leads.AddRange(generatedLeads);
    }

    private static List<Account> GeneratedAccountsForLead(Lead lead)
    {
        List<Account> accountsForLead =
        [
            new Account()
            {
                Id = Guid.NewGuid(),
                Currency = Currency.Rub,
                LeadId = lead.Id,
                Status = AccountStatus.Active

            },
            new Account()
            {
                Id = Guid.NewGuid(),
                Currency = Currency.Usd,
                LeadId = lead.Id,
                Status = AccountStatus.Active
            },
            new Account()
            {
                Id = Guid.NewGuid(),
                Currency = Currency.Eur,
                LeadId = lead.Id,
                Status = AccountStatus.Active
            }
        ];

        return accountsForLead;
    }
    
    private static List<Account> GeneratedAccountsForVipLead(Lead lead)
    {
        List<Account> accountsForVipLead =
        [
            new Account()
            {
                Id = Guid.NewGuid(),
                Currency = Currency.Jpy,
                LeadId = lead.Id,
                Status = AccountStatus.Active

            },
            new Account()
            {
                Id = Guid.NewGuid(),
                Currency = Currency.Cny,
                LeadId = lead.Id,
                Status = AccountStatus.Active
            },
            new Account()
            {
                Id = Guid.NewGuid(),
                Currency = Currency.Rsd,
                LeadId = lead.Id,
                Status = AccountStatus.Active
            },
            new Account()
            {
                Id = Guid.NewGuid(),
                Currency = Currency.Bgn,
                LeadId = lead.Id,
                Status = AccountStatus.Active
            },
            new Account()
            {
                Id = Guid.NewGuid(),
                Currency = Currency.Ars,
                LeadId = lead.Id,
                Status = AccountStatus.Active
            }
        ];

        return accountsForVipLead;
    }
}
