using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Obligatorio2.Models.BL
{
    [Table("Clients")]
    public class Client
    {
        [Required]
        [StringLength(50)]
        public string Name { get; private set; }
        [Key]
        public long Tin { get; private set; }
        public uint Seniority { get; set; }
        public uint Discount { get; set; }
        [Required]
        public DateTime RegisterDate {get; set;}
        public IEnumerable<Product> Products { get; private set; }
        public IEnumerable<Import> Imports { get; private set; }

        public Client(string name, long tin, DateTime registerDate)
        { Name = name; Tin = tin; RegisterDate = registerDate; }

        public Client(string name, long tin, uint discount, DateTime registerDate)
        { Name = name; Tin = tin; Discount = discount; RegisterDate = registerDate; }

        public Client(string name, long tin, uint discount, uint seniority, DateTime registerDate)
        { Name = name; Tin = tin; Discount = discount; Seniority = seniority; RegisterDate = registerDate; }

        public Client(long tin) { Tin = tin;}

        public bool isTinValid()
        {
            int digitsNumber = Utilities.digitsNumber(Tin);
            if (digitsNumber == 12) return true;
            else return false;
        }

    }
}