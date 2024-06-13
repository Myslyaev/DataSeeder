using DataSeeder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSeeder.Models;

public class TransactionDto
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public TransactionType TransactionType { get; set; }
    public Currency CurrencyType { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
