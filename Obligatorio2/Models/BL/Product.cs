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
        [Key]
        public string Id { get; private set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int Weight { get; set; }
        public Client Importer { get; set; }
        [ForeignKey("Importer")]
        public long Tin { get; set; }
        public uint Ammount { get; set; }

        public Product(string id) { Id = id;}
        public Product(string id, string name, int weight, Client importer) { Id = id; Name = name; Weight = weight; Importer = importer; }
        public Product(string id, string name, int weight, uint ammount, Client importer) { Id = id; Name = name; Weight = weight; Ammount = ammount; Importer = importer; }
    }
}