using ContaRemunerada.Data.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContaRemunerada.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.AddRange(new Account[]{
                    new Account { Name = "Money", Value = 60, Transactions = new Transaction[]{
                        new Transaction { Value = 10, Type = TransactionType.Deposit },
                        new Transaction { Value = 20, Type = TransactionType.Deposit },
                        new Transaction { Value = 30, Type = TransactionType.Deposit }
                    }},
                    new Account { Name = "Especial", Value = 96, Transactions = new Transaction[]{
                        new Transaction { Value = 42.54M, Type = TransactionType.Deposit },
                        new Transaction { Value = 53.46M, Type = TransactionType.Deposit }
                    }}
                });

            context.SaveChanges();
        }
    }
}
