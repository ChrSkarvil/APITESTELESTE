using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Models
{
    public class ProductInfoModel
    {
        public int ProductInfoID { get; set; }
        public string ProductDescription { get; set; }
        public string NetDimensions { get; set; }
        public decimal NetWeight { get; set; }
        public string GrossDimensions { get; set; }
        public decimal GrossWeight { get; set; }
        public Nullable<long> EAN { get; set; }
        public Nullable<long> GTIN { get; set; }
    }
}
