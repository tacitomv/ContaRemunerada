using ContaRemunerada.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ContaRemunerada.Tests
{
    public class MySqlTransactionsServiceTest : TransactionsServiceTest
    {
        public MySqlTransactionsServiceTest()
            : base(
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseMySql(@"Server=database-1.cbmkklylrdkc.sa-east-1.rds.amazonaws.com;Uid=ContaRemunerada;Password=ContaRemunerada;DataBase=ContaRemunerada")
                    .Options)
        {
        }
    }
}
