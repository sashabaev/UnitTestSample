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
            mockCurency.Setup(p => p.GetEuroToUSdRate()).ReturnsAsync(1.13);
            
            service = new ATMService(mockCurency.Object, mockRepiository.Object);
        }

        [Fact]
        public async void TestTransaction()
        {
            try
            {
                mockRepiository.Setup(p => p.CreateAsync(It.IsAny<BankTransaction>()))
                    .Callback((BankTransaction val) =>
                    {
                        transactions.Add(val);                       
                    });
                
                 service.Withdraw(300, Currency.EURO, "ccccc", Country.Germany);
            }
            catch (ATMNotEnoughMoneyException ex)
            {
                Assert.True(false);
                return;
            }
            Assert.Equal(transactions,  mockRepiository.Object.All);           
        }
    }
}
