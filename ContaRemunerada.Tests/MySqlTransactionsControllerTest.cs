using ContaRemunerada.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ContaRemunerada.Tests
{
    public class MySqlTransactionsControllerTest : TransactionsControllerTest
    {
        public MySqlTransactionsControllerTest()
            : base(
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseMySql(@"Server=database-1.cbmkklylrdkc.sa-east-1.rds.amazonaws.com;Uid=ContaRemunerada;Password=ContaRemunerada;DataBase=ContaRemunerada")
                    .Options)
        {
        }

        [Fact]
        public void ShouldConnectToMySqlDatabase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
        .UseMySql("Server=database-1.cbmkklylrdkc.sa-east-1.rds.amazonaws.com;Uid=ContaRemunerada;Password=ContaRemunerada;DataBase=ContaRemunerada");

            AppDbContext dbContext = new AppDbContext(optionsBuilder.Options);
            bool expected = true;

            bool result = dbContext.Database.CanConnect();

            Assert.Equal(expected, result);
        }
    }
}
