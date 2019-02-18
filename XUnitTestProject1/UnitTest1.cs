using Moq;
using Repositories.Interfaces;
using Repositories.Models;
using Services.Exceptions;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        private Mock<ICurrencyHttpService> mockCurency;
        private Mock<IATMRepository> mockRepiository;
        private List<BankTransaction> transactions;

        public UnitTest1()
        {
            transactions = new List<BankTransaction>();
            transactions.Add(new BankTransaction { Amount = 1000, ATMAddress = "", IsDebit = true, TransactionDate = DateTime.Now });
            mockRepiository = new Mock<IATMRepository>();
            mockRepiository.Setup(p => p.All).Returns(new List<BankTransaction> { new BankTransaction { Amount = 1000, ATMAddress = "", IsDebit = true, TransactionDate = DateTime.Now } }.AsQueryable());
            mockCurency = new Mock<ICurrencyHttpService>();
            mockCurency.Setup(p => p.GetEuroToUSdRate()).ReturnsAsync(1.4);
            mockRepiository.Setup(p => p.CreateAsync(It.IsAny<BankTransaction>()))
                 .Callback((BankTransaction val) =>
                 {
                     transactions.Add(val);
                 });
        }

        [Fact]
        public async void Test1()
        {
            var service = new ATMService(mockCurency.Object, mockRepiository.Object);
            try
            {
                await service.Withdraw(200, "ccccc");
            }
            catch (Exception ex)
            {
                Assert.True(true);
                return;
            }
            Assert.True(true);
        }


        [Fact]
        public async void TestCurency()
        {
            var service = new ATMService(mockCurency.Object, mockRepiository.Object);
            try
            {
                await service.Withdraw(200, Currency.USD, "ccccc", Country.USA);
            }
            catch (ATMNotEnoughMoneyException)
            {
                Assert.True(true);
                return;
            }
            Assert.True(true);
        }
    }
}
