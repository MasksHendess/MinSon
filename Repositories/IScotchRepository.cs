using MinSon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Services
{
   public interface IScotchRepository
    {
        Task<List<ShowcaseProduct>> GetAllShowcaseProductsByNameAsync(string Name);
        Task<ShowcaseProduct> GetShowcaseProductByNameAsync( string Name);
        Task<ShowcaseProduct> GetRandomshowcaseProduct();
        Task<ShowcaseProduct> GetRandomShowcaseProductFromRegion(string region);
    }
}
