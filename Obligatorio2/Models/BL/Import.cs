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
        public Import(Product importedProduct, Client importingClient, uint ammount,
                        int priceByUnit, DateTime entryDate, DateTime departureDate, bool isStored)
        {
            ImportedProduct = importedProduct;
            ImporterClient = importingClient;
            Ammount = ammount;
            PriceByUnit = priceByUnit;
            EntryDate = entryDate;
            DepartureDate = departureDate;
            IsStored = isStored;
        }

        public Import(int id,Product importedProduct, Client importingClient, uint ammount,
                        int priceByUnit, DateTime entryDate, DateTime departureDate, bool isStored)
        {
            Id = id;
            ImportedProduct = importedProduct;
            ImporterClient = importingClient;
            Ammount = ammount;
            PriceByUnit = priceByUnit;
            EntryDate = entryDate;
            DepartureDate = departureDate;
            IsStored = isStored;
        }

        public bool IsImportValid()
        {
            if (DateTime.Compare(EntryDate, DepartureDate) >= 0) return false;

            return true;
        }

        [Key]
        public int Id { get; set; }
        public Product ImportedProduct { get; set; }
        [ForeignKey("ImportedProduct")]
        public string ProductId { get; set; } 
        public Client ImporterClient { get; set; }
        [ForeignKey("ImporterClient")]
        public long Tin { get; set; }
        [Required]
        public uint Ammount { get; set; }
        [Required]
        public int PriceByUnit { get; set; }
        [Required]
        public DateTime EntryDate { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public bool IsStored { get; set; }
    }
}