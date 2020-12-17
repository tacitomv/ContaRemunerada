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
    public abstract class TransactionsControllerTest
    {
        protected TransactionsControllerTest(DbContextOptions<AppDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        protected DbContextOptions<AppDbContext> ContextOptions { get; }

        private void Seed()
        {
            using var context = new AppDbContext(ContextOptions);
            DbInitializer.Initialize(context);
        }

        [Fact]
        public async Task CheckAccount1()
        {
            using var context = new AppDbContext(ContextOptions);
            var service = new AccountService(context);
            var controller = new AccountsController(service);

            var dbAcc = await service.GetAccountWithTransactions(1);
            Assert.Equal(60, dbAcc.Value);
            Assert.Equal(3, dbAcc.Transactions.Count);
            Assert.Equal(dbAcc.Value, dbAcc.Transactions
                .Where(s => s.Type == TransactionType.Deposit)
                .Select(s => s.Value)
                .Sum());
        }

        [Fact]
        public async Task CheckAccount2()
        {
            using var context = new AppDbContext(ContextOptions);
            var service = new AccountService(context);
            var controller = new AccountsController(service);

            var dbAcc = await service.GetAccountWithTransactions(2);
            Assert.Equal(96, dbAcc.Value);
            Assert.Equal(2, dbAcc.Transactions.Count);
            Assert.Equal(dbAcc.Value, dbAcc.Transactions
                .Where(s => s.Type == TransactionType.Deposit)
                .Select(s => s.Value)
                .Sum());
        }

        [Fact]
        public async Task ShouldDeposit()
        {
            using var context = new AppDbContext(ContextOptions);
            var service = new AccountService(context);
            var controller = new AccountsController(service);

            Transaction trans = await controller.Deposit(new TransactionDTO(1, 20));

            var dbAcc = await service.GetAccountWithTransactions(1);
            Assert.Equal(80, dbAcc.Value);
            Assert.Equal(20, trans.Value);
            Assert.Equal(4, dbAcc.Transactions.Count);
            Assert.Equal(80, dbAcc.Transactions
                .Where(s => s.Type == TransactionType.Deposit)
                .Select(s => s.Value)
                .Sum());
        }

        [Fact]
        public async Task ShouldWhitdraw()
        {
            using var context = new AppDbContext(ContextOptions);
            var service = new AccountService(context);
            var controller = new AccountsController(service);

            Transaction trans = await controller.Withdraw(new TransactionDTO(1, 50));

            var dbAcc = await service.GetAccountWithTransactions(1);
            Assert.Equal(10, dbAcc.Value);
            Assert.Equal(50, trans.Value);
            Assert.Equal(4, dbAcc.Transactions.Count);
            Assert.Equal(60, dbAcc.Transactions
                .Where(s => s.Type == TransactionType.Deposit)
                .Select(s => s.Value)
                .Sum());
            Assert.Equal(50, dbAcc.Transactions
                .Where(s => s.Type == TransactionType.Whitdraw)
                .Select(s => s.Value)
                .Sum());
        }

        [Fact]
        public async Task ShouldPay()
        {
            using var context = new AppDbContext(ContextOptions);
            var service = new AccountService(context);
            var controller = new AccountsController(service);

            Transaction trans = await controller.Pay(new PaymentDTO(1, 25, "balde de abobrinha"));

            var acc1 = await service.GetAccountWithTransactions(1);

            Assert.Equal(35, acc1.Value);
            Assert.Equal(4, acc1.Transactions.Count);
            Assert.Equal(60, acc1.Transactions
                .Where(s => s.Type == TransactionType.Deposit)
                .Select(s => s.Value)
                .Sum());
            Assert.Equal(25, acc1.Transactions
                .Where(s => s.Type == TransactionType.Payment)
                .Select(s => s.Value)
                .Sum());
        }

        [Fact]
        public async Task ShouldNotWhitdraw()
        {
            using var context = new AppDbContext(ContextOptions);
            var service = new AccountService(context);
            var controller = new AccountsController(service);

            Transaction trans = await controller.Withdraw(new TransactionDTO(1, 80));

            var dbAcc = await service.GetAccountWithTransactions(1);
            Assert.Equal(60, dbAcc.Value);
            Assert.Equal(80, trans.Value);
            Assert.Equal(TransactionStatus.Unauthorized, trans.Status);
            Assert.Equal(4, dbAcc.Transactions.Count);
            Assert.Equal(60, dbAcc.Transactions
                .Where(s => s.Type == TransactionType.Deposit)
                .Select(s => s.Value)
                .Sum());
            Assert.Equal(0, dbAcc.Transactions
                .Where(s => s.Type == TransactionType.Whitdraw && s.Status == TransactionStatus.Completed)
                .Select(s => s.Value)
                .Sum());
            Assert.Equal(80, dbAcc.Transactions
                .Where(s => s.Type == TransactionType.Whitdraw && s.Status == TransactionStatus.Unauthorized)
                .Select(s => s.Value)
                .Sum());
        }

        [Fact]
        public async Task ShouldNotPay()
        {
            using var context = new AppDbContext(ContextOptions);
            var service = new AccountService(context);
            var controller = new AccountsController(service);

            Transaction trans = await controller.Pay(new PaymentDTO(1, 75, "balde de abobrinha"));

            var acc1 = await service.GetAccountWithTransactions(1);

            Assert.Equal(60, acc1.Value);
            Assert.Equal(TransactionStatus.Unauthorized, trans.Status);
            Assert.Equal(4, acc1.Transactions.Count);
            Assert.Equal(60, acc1.Transactions
                .Where(s => s.Type == TransactionType.Deposit && s.Status == TransactionStatus.Completed)
                .Select(s => s.Value)
                .Sum());
            Assert.Equal(75, acc1.Transactions
                .Where(s => s.Type == TransactionType.Payment && s.Status == TransactionStatus.Unauthorized)
                .Select(s => s.Value)
                .Sum());
        }

        [Fact]
        public async Task GetAccount() { }
        [Fact]
        public async Task GetTransactions() { }
    }
}
