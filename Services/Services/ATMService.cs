﻿using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;
using Repositories.Models;

namespace Services.Services
{
    public class ATMService : IATMService
    {
        private readonly IATMRepository _aTMRepository;
        private readonly ICurrencyHttpService _currencyHttpService;

        public ATMService(ICurrencyHttpService currencyHttpService, IATMRepository aTMRepository)
        {
            _aTMRepository = aTMRepository;
            _currencyHttpService = currencyHttpService;
        }

        public async Task Withdraw(int amount, string address)
        {
            if (amount <= 0)
                throw new Exception("Amount cant be less or equals then zero");
            if (amount % 100 > 0)
                throw new Exception("Amount is incorrect");
            if (string.IsNullOrWhiteSpace(address))
                throw new Exception("Address is incorrect");

            var totalAmount = _aTMRepository.All.Sum(x => x.Amount);

            if (totalAmount < amount)
                throw new Exception("Not enough money");

            await _aTMRepository.CreateAsync(new BankTransaction() { Amount = totalAmount - amount, ATMAddress = address, IsDebit = false, TransactionDate = DateTime.Now });
        }

        public async Task Withdraw(int amount, Currency currency, string address, Country country)
        {
            //if we withdraw euro we need to conver it to usd, out ATM suppors only USD
            if (currency.Equals(Currency.EURO))
            {
                var rate = await _currencyHttpService.GetEuroToUSdRate();
                amount = Convert.ToInt32(amount * rate);
            }
            //if country is not USA then we need to add tax 1% from amount
            if (!country.Equals(Country.USA))
            {
                amount = amount - Convert.ToInt32(amount * .1);
            }

            await Withdraw(amount, address);
        }
    }
}
