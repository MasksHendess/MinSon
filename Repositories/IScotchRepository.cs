using MinSon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Services
{
   public interface IScotchRepository
    {
        Task<Product> GetProductAsync(string Name);
        Task<PartialProduct> GetProductByShowcaseNameAsync(string showcaseName, int nr);
        Task<Product> GetRandomProduct();
        Task<Product> GetRandomIslayProduct(string region);
    }
}
