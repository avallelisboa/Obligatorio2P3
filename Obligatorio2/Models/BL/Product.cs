using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Obligatorio2.Models.BL
{
    [Table("Products")]
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProductId { get; private set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int Weight { get; set; }
        public Client Importer { get; set; }
        [ForeignKey("Importer")]
        public long ClientTin { get; set; }
        public int Ammount { get; set; }

        public Product() { }
        public Product(string ProductId) { this.ProductId = ProductId;}
        public Product(string ProductId, string name, int weight, long tin) { this.ProductId = ProductId; Name = name; Weight = weight; ClientTin = tin;}
        public Product(string ProductId, string name, int weight, int ammount, long tin) { this.ProductId = ProductId; Name = name; Weight = weight; Ammount = ammount; ClientTin = tin;}
    }
}