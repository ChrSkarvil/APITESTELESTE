using CoreCodeCamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data
{
    public interface IElgigantenRepository
    {
        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // ProductInfo
        Task<ProductInfo[]> GetAllProductInfosAsync();
        Task<ProductInfo> GetProductInfoAsync(long? ean, long? gtin);

        // Product
        Task<Product> GetProductAsync(string productName);
        Task<Product[]> GetAllProductsAsync(bool includeProductInfos = false);
    }
}
