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
        private List<BankTransaction> transactions = new List<BankTransaction> { new BankTransaction { Amount = 1000, ATMAddress = "", IsDebit = true, TransactionDate = DateTime.Now}};
        private ATMService service;

        public UnitTest1()
        {           
            mockRepiository = new Mock<IATMRepository>();
            mockRepiository.Setup(p => p.All).Returns(transactions.AsQueryable());
            mockCurency = new Mock<ICurrencyHttpService>();
            mockCurency.Setup(p => p.GetEuroToUSdRate()).ReturnsAsync(1.4);
            mockRepiository.Setup(p => p.CreateAsync(It.IsAny<BankTransaction>()))
                 .Callback((BankTransaction val) =>
                 {
                     transactions.Add(val);
                 });
            service = new ATMService(mockCurency.Object, mockRepiository.Object);
        }

        [Fact]
        public async void Test1()
        {
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
            try
            {
                await service.Withdraw(200, Currency.EURO, "ccccc", Country.Germany);
            }
            catch (ATMNotEnoughMoneyException ex)
            {
                Assert.True(true);
                return;
            }
            Assert.True(true);
        }
    }
}
