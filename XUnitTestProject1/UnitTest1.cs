using Moq;
using Repositories.Interfaces;
using Repositories.Models;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        public UnitTest1()
        {

        }

        [Fact]
        public async void Test1()
        {
            //var mock = new Mock<IATMRepository>();
            //mock.Setup(p => p.All).Returns(new List<BankTransaction> { new BankTransaction { Amount = 1000, ATMAddress = "", IsDebit = true, TransactionDate = DateTime.Now } }.AsQueryable());

            //var service = new ATMService(mock.Object);
            //try
            //{
            //    await service.Withdraw(2000, "ccccc");
            //}
            //catch (Exception ex)
            //{
            //    Assert.True(true);
            //    return;
            //}
            //Assert.True(false);
        }
    }
}
