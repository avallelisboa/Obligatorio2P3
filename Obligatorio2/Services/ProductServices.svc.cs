using Obligatorio2.Models.BL;
using Obligatorio2.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Obligatorio2.Services
{
    public class ProductServices : IProductServices
    {
        public IEnumerable<ProductDTO> GetProducts()
        {
            IRepository<Product> repository = new ProductRepository();
            var products = repository.FindAll();
            List<ProductDTO> productDTOs = new List<ProductDTO>();
            foreach (Product p in products)
            {
                productDTOs.Add(new ProductDTO(p.Id, p.Name, p.Ammount, p.Weight, p.Importer.Tin));
            }
            return productDTOs;
        }
    }

    [DataContract]
    public class ProductDTO
    {
        public ProductDTO(string id, string name, uint ammount,
            int productWeight, long clientTin)
        { Id = id; Name = name; Ammount = ammount; ProductWeight = productWeight; ClientTin = clientTin; }
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public uint Ammount { get; set; }
        [DataMember]
        public int ProductWeight { get; set; }
        [DataMember]
        public long ClientTin { get; set; }
    }


}
