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
        private Mock<IATMRepository> mockRepository;
        private List<BankTransaction> transactions = new List<BankTransaction> { new BankTransaction { Amount = 1000, ATMAddress = "", IsDebit = true, TransactionDate = DateTime.Now } };
        private ATMService service;

        public UnitTest1()
        {
            mockRepository = new Mock<IATMRepository>();
            mockRepository.Setup(p => p.All).Returns(transactions.AsQueryable());
            mockCurency = new Mock<ICurrencyHttpService>();
            mockCurency.Setup(p => p.GetEuroToUSdRate()).ReturnsAsync(1.13);
            service = new ATMService(mockCurency.Object, mockRepository.Object);
        }

        [Fact]
        public async void TestTransaction()
        {            
            try
            {
                await service.Withdraw(300, "ccccc");
            }
            catch (ATMNotEnoughMoneyException ex)
            {
                Assert.True(false);
                return;
            }
            Assert.True(true);
        }

        [Fact]
        public async void TestCurencyRateSuccess()
        {
            try
            {
                mockRepository.Setup(p => p.CreateAsync(It.IsAny<BankTransaction>()))
                     .Callback((BankTransaction val) =>
                     {
                         transactions.Add(val);
                     });
                service = new ATMService(mockCurency.Object, mockRepository.Object);
                await service.Withdraw(300, Currency.EURO, "ccccc", Country.Germany);
                Assert.Equal(695, mockRepository.Object.All.Sum(x => x.Amount));
            }
            catch(Exception ex)
            {
                Assert.Equal(695, transactions.Sum(x => x.Amount));
            }
        }

        [Fact]
        public async void TestCountryCheckAmmountForNotUSASuccess()
        {
            try
            {
                mockRepository.Setup(p => p.CreateAsync(It.IsAny<BankTransaction>()))
                     .Callback((BankTransaction val) =>
                     {
                         transactions.Add(val);
                     });
                service = new ATMService(mockCurency.Object, mockRepository.Object);
                await service.Withdraw(300, Currency.USD, "ccccc", Country.Germany);
                Assert.Equal(730, mockRepository.Object.All.Sum(x => x.Amount));
            }
            catch (Exception ex)
            {
                Assert.Equal(730, transactions.Sum(x => x.Amount));
            }
        }

        [Fact]
        public async void TestRateEqualZero()
        {
            mockCurency = new Mock<ICurrencyHttpService>();
            mockCurency.Setup(p => p.GetEuroToUSdRate()).ReturnsAsync(0);
            service = new ATMService(mockCurency.Object, mockRepository.Object);
            try
            {              
                await service.Withdraw(300, Currency.EURO, "ccccc", Country.Germany);
            }
            catch (ATMRateEqualZeroException ex)
            {
                Assert.True(true);
                return;
            }
            Assert.True(false);
        }
    }
}
