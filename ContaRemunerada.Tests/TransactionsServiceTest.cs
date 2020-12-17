using ContaRemunerada.Controllers;
using ContaRemunerada.Data;
using ContaRemunerada.Data.Domain;
using ContaRemunerada.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContaRemunerada.Tests
{
    public abstract class TransactionsServiceTest
    {
        protected TransactionsServiceTest(DbContextOptions<AppDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        protected DbContextOptions<AppDbContext> ContextOptions { get; }

        private void Seed()
        {
            using var context = new AppDbContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.AddRange(new Account[]{
                    new Account { Name = "Money", Value = 5000, Transactions = new Transaction[]{
                        new Transaction { Value = 2500, Type = TransactionType.Deposit },
                        new Transaction { Value = 1500, Type = TransactionType.Deposit },
                        new Transaction { Value = 1000, Type = TransactionType.Deposit }
                    }}
                });

            context.SaveChanges();
        }

        [Fact]
        public void YieldAccount()
        {
            //https://blog.paranabanco.com.br/investimento/cdi-diario/

            using var context = new AppDbContext(ContextOptions);
            var service = new AccountService(context);

            double cdi = 6.89;
            Transaction trans = service.Yield(1, cdi);

            var acc1 = service.GetAccountWithTransactions(1).Result;

            Assert.Equal(1.32M, trans.Value);
            Assert.Equal(5001.32M, acc1.Value);
            Assert.Equal(TransactionStatus.Completed, trans.Status);
            Assert.Equal(4, acc1.Transactions.Count);
            Assert.Equal(1.32M, acc1.Transactions
                .Where(s => s.Type == TransactionType.Yield && s.Status == TransactionStatus.Completed)
                .Select(s => s.Value)
                .Sum());
            Assert.Equal(5001.32M, acc1.Transactions
                .Where(s => s.Type == TransactionType.Yield || s.Type == TransactionType.Deposit && s.Status == TransactionStatus.Completed)
                .Select(s => s.Value)
                .Sum());
        }
    }
}
