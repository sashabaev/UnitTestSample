//using Repositories;
//using Repositories.Interfaces;
//using Repositories.Repositories;
//using Services.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using System.Linq;

//namespace Services.Services
//{
//    public class ATMService : IATMService
//    {
//        private readonly IATMRepository _aTMRepository;

//        public ATMService(IATMRepository aTMRepository)
//        {
//            _aTMRepository = aTMRepository;
//        }

//        public async Task InitATM()
//        {
//            await _aTMRepository.CreateAsync(new Repositories.Models.BankTransaction() { Amount = 10000, ATMAddress = "Prospekt Nauki 37", IsDebit = true, TransactionDate = DateTime.Now });
//        }

//        public async Task Withdraw(int amount, string address)
//        {
//            if (amount <= 0)
//                throw new Exception("Amount cant be less or equals then zero");
//            if (amount % 100 > 0)
//                throw new Exception("Amount is incorrect");
//            if (string.IsNullOrWhiteSpace(address))
//                throw new Exception("Address is incorrect");

//            var totalAmount = _aTMRepository.All.Sum(x => x.Amount);

//            if (totalAmount < amount)
//                throw new Exception("Not enough money");

//            await _aTMRepository.CreateAsync(new Repositories.Models.BankTransaction() { Amount = -amount, ATMAddress = "Prospekt Nauki 37", IsDebit = false, TransactionDate = DateTime.Now });
//        }
//    }
//}
