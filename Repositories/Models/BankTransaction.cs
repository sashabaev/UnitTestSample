using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repositories.Models
{
    [Table("BankTransaction")]
    public class BankTransaction : IEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime TransactionDate { get; set; }

        public int Amount { get; set; }

        public bool IsDebit { get; set; }

        public string ATMAddress { get; set; }

        public Currency Currency { get; set; }

        public Country Country { get; set; }
    }

    public enum Currency {
        USD,
        EURO
    }

    public enum Country {
        USA,
        Germany
    }
}
