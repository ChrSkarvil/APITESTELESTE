using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ManufacturerID { get; set; }
        public int CategoryID { get; set; }
        public ProductInfo ProductInfo { get; set; }
        //public int ProductInfoID { get; set; }

        //public int ProductInfoID { get; set; }
        public List<ProductInfo> ProductInfos { get; set; }

    }
}
