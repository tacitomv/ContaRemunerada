using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContaRemunerada.Data.Domain;
using ContaRemunerada.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContaRemunerada.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _accounts;

        public AccountsController(AccountService accountService)
        {
            _accounts = accountService;
        }

        [HttpGet]
        public async Task<IEnumerable<Transaction>> Transactions(long contaId)
        {
            if (contaId > 0)
                return await _accounts.GetTransactions(contaId);

            return Enumerable.Empty<Transaction>();
        }

        [HttpGet]
        public async Task<Account> Funds(long contaId)
        {
            if (contaId > 0)
                return await _accounts.GetAccount(contaId);

            return null;
        }

        [HttpGet]
        public async Task<Account> Account(long contaId)
        {
            if (contaId > 0)
                return await _accounts.GetAccountWithTransactions(contaId);

            return null;
        }

        [HttpPost]
        public async Task<Transaction> Withdraw(TransactionDTO whitdraw)
        {
            if (whitdraw.ContaId > 0 && whitdraw.Valor > 0)
            {
                return await _accounts.Whitdraw(whitdraw.ContaId, whitdraw.Valor);
            }

            return null;
        }
        [HttpPost]
        public async Task<Transaction> Deposit(TransactionDTO deposit)
        {
            if (deposit.ContaId > 0 && deposit.Valor > 0)
            {
                return await _accounts.Deposit(deposit.ContaId, deposit.Valor);
            }

            return null;
        }

        [HttpPost]
        public async Task<Transaction> Pay(PaymentDTO payment)
        {
            if (payment.ContaId > 0 && payment.Valor > 0)
            {
                return await _accounts.Payment(payment.ContaId, payment.Valor, payment.Descricao);
            }

            return null;
        }
    }

    public class TransactionDTO
    {
        public TransactionDTO() { }
        public TransactionDTO(long contaId, int valor)
        {
            ContaId = contaId;
            Valor = valor;
        }

        public long ContaId { get; set; }
        public double Valor { get; set; }
    }

    public class PaymentDTO
    {
        public PaymentDTO() { }
        public PaymentDTO(long contaId, int valor, string descricao)
        {
            ContaId = contaId;
            Valor = valor;
            Descricao = descricao;
        }
        public long ContaId { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
    }
}
