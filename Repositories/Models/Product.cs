using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repositories.Models
{
    [Table("Product")]
    public class Product : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }
    }
}
