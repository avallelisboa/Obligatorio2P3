using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Obligatorio2.Models.BL
{
    [Table("Imports")]
    public class Import
    {
        public Import() { }
        public Import(string productId, int ammount,
                        int priceByUnit, DateTime entryDate, DateTime departureDate, bool isStored)
        {
            ProductId = productId;
            Ammount = ammount;
            PriceByUnit = priceByUnit;
            EntryDate = entryDate;
            DepartureDate = departureDate;
            IsStored = isStored;
        }
        public Import(int id,string productId, int ammount,
                        int priceByUnit, DateTime entryDate, DateTime departureDate, bool isStored)
        {
            Id = id;
            ProductId = productId;
            Ammount = ammount;
            PriceByUnit = priceByUnit;
            EntryDate = entryDate;
            DepartureDate = departureDate;
            IsStored = isStored;
        }

        public bool IsImportValid()
        {
            if (DateTime.Compare(EntryDate, DepartureDate) >= 0) return false;
            if (Ammount < 0) return false;

            return true;
        }

        [Key]
        public int Id { get; set; }
        public Product ImportedProduct { get; set; }
        [ForeignKey("ImportedProduct")]
        public string ProductId { get; set; }
        public int Ammount { get; set; }
        public int PriceByUnit { get; set; }
        [Required]
        public DateTime EntryDate { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public bool IsStored { get; set; }
    }
}