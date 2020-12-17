using ContaRemunerada.Data;
using ContaRemunerada.Services;
using FluentScheduler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaRemunerada.Jobs
{
    public class YieldRegistry : Registry
    {
        public YieldRegistry() {
            YieldAccounts();
        }
        
        private void YieldAccounts()
        {
            JobManager.AddJob(() =>
            {
                var accounts = Program.ServiceProvider.GetService<AccountService>();
                var rates = Program.ServiceProvider.GetService<RatesService>();

                var accountIds = accounts.GetAllAccountIds();

                var dayRate = rates.GetCDI();
                foreach(long accountId in accountIds)
                    accounts.Yield(accountId, dayRate);

            }, s => s.ToRunNow().AndEvery(1).Days());
        }
    }
}
