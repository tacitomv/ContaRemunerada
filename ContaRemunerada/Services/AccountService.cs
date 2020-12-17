using ContaRemunerada.Data;
using ContaRemunerada.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaRemunerada.Services
{
    public class AccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<Transaction>> GetTransactions(long accountId)
        {
            var query = _context.Transactions
                        .Where(t => t.AccountId == accountId);
            return await query.ToListAsync();
        }

        public async Task<Account> GetAccountWithTransactions(long accountId) => await _context.Accounts
                    .Include(i => i.Transactions)
                    .SingleOrDefaultAsync(a => a.Id == accountId);

        private async Task<Transaction> AddTransaction(long accountId, Transaction transaction)
        {
            var account = await _context.Accounts
                    .Include(i => i.Transactions)
                    .SingleOrDefaultAsync(a => a.Id == accountId);

            CalcAccountTransaction(account, transaction);

            account.Transactions.Add(transaction);

            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Account> GetAccount(long accountId) => await _context.Accounts.SingleOrDefaultAsync(a => a.Id == accountId);

        private void CalcAccountTransaction(Account account, Transaction transaction)
        {
            try
            {
                switch (transaction.Type)
                {
                    case TransactionType.Yield:
                        
                        break;
                    case TransactionType.Deposit:
                        account.AddFunds(transaction.Value);
                        break;
                    case TransactionType.Whitdraw:
                    case TransactionType.Payment:
                        account.RemoveFunds(transaction.Value);
                        break;
                }
            }
            catch (NoFundsException)
            {
                transaction.Status = TransactionStatus.Unauthorized;
            }
        }

        public async Task<Transaction> Payment(long accountId, double value, string descricao) => await AddTransaction(accountId, new Transaction()
        {
            Type = TransactionType.Payment,
            Value = Convert.ToDecimal(value),
            Description = descricao
        });

        public async Task<Transaction> Deposit(long accountId, double value) => await AddTransaction(accountId, new Transaction()
        {
            Type = TransactionType.Deposit,
            Value = Convert.ToDecimal(value)
        });

        public async Task<Transaction> Whitdraw(long accountId, double value) => await AddTransaction(accountId, new Transaction()
        {
            Type = TransactionType.Whitdraw,
            Value = Convert.ToDecimal(value)
        });

        //public async Task<(Transaction, Transaction)> Transfer(long accountId, decimal value, long targetAccountId) => (await AddTransaction(accountId,
        //    new Transaction()
        //    {
        //        Type = TransactionType.Payment,
        //        Value = value
        //    }),
        //    await AddTransaction(targetAccountId, new Transaction()
        //    {
        //        Type = TransactionType.Deposit,
        //        Value = value
        //    }));

        public Transaction Yield(long accountId, double cdi)
        {
            var account = this.GetAccountWithTransactions(accountId).Result;
            var funds = account.GetFunds();
            var newFunds = (decimal)Math.Pow((1 + (cdi / 100)), 1 / 252D) * funds;

            var transaction = new Transaction()
            {
                Value = Math.Round(newFunds - funds, 2),
                Type = TransactionType.Yield
            };
            account.AddFunds(transaction.Value);

            account.Transactions.Add(transaction);

            _context.SaveChanges();

            return transaction;
        }
        public async Task<IEnumerable<Account>> GetAllAccounts() => await _context.Accounts.ToListAsync();

        public IEnumerable<long> GetAllAccountIds() => _context.Accounts.Select(s => s.Id).ToList();
    }
}
