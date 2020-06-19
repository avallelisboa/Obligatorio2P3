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
            ImportingClient = importingClient;
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
            ImportingClient = importingClient;
            Ammount = ammount;
            PriceByUnit = priceByUnit;
            EntryDate = entryDate;
            DepartureDate = departureDate;
            IsStored = isStored;
        }

        [Key]
        public int Id { get; set; }
        public Product ImportedProduct { get; set; }
        public Client ImportingClient { get; set; }
        public uint Ammount { get; set; }
        public int PriceByUnit { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DepartureDate { get; set; }
        [Required]
        public bool IsStored { get; set; }
    }
}