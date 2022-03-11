using CoreCodeCamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ManufacturerID { get; set; }
        public int CategoryID { get; set; }
        public ICollection<ProductInfo> ProductInfos { get; set; }
    }
}
