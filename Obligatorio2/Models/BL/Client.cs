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
        public string Name { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Tin { get; set; }
        [Required]
        [Range(0,10000)]
        public int Seniority { get; set; }
        [Range(0,100)]
        public int Discount { get; set; }
        [Required]
        public DateTime RegisterDate {get; set;}
        public List<Product> Products { get; set; }
        public List<Import> Imports { get; set; }

        public Client(string name, long tin, DateTime registerDate)
        { Name = name; Tin = tin; RegisterDate = registerDate; }

        public Client(string name, long tin, int discount, DateTime registerDate)
        { Name = name; Tin = tin; Discount = discount; RegisterDate = registerDate; }

        public Client(string name, long tin, int discount, int seniority, DateTime registerDate)
        { Name = name; Tin = tin; Discount = discount; Seniority = seniority; RegisterDate = registerDate; }

        public Client(long tin) { Tin = tin;}

        public Client() { }

        public bool isTinValid()
        {
            int digitsNumber = Utilities.digitsNumber(Tin);
            if (digitsNumber == 12) return true;
            else return false;
        }

    }
}