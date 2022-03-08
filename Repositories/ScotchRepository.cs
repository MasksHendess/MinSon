using MinSon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Services
{
    public class ScotchRepository : IScotchRepository
    {
        private readonly MinSonDBContext dbContext;
        public ScotchRepository(MinSonDBContext DBContext)
        {
            dbContext = DBContext;
        }
        public async Task<List<ShowcaseProduct>> GetAllShowcaseProductsByNameAsync(string Name)
        {
            var result =  dbContext.showcaseProducts.AsAsyncEnumerable().Where(x => x.brandName == Name);
            if (result != null)
                return await result.ToListAsync();
            else
                return null;
        }

        public async Task<ShowcaseProduct> GetShowcaseProductByNameAsync( string Name)
        {
            var result = dbContext.showcaseProducts.AsAsyncEnumerable().Where(x => x.showcase_Item1_Name == Name).FirstOrDefaultAsync();
            return result.Result;
        }
        public async Task<ShowcaseProduct> GetRandomshowcaseProduct()
        {
            // get one single random item to dispaly
            var items = dbContext.showcaseProducts.AsAsyncEnumerable().Where(x => x.webbImage_Item1 != null);
            var ytems = items.ToListAsync();
            Random rnd = new Random();
            int randomId = rnd.Next(1, ytems.Result.Count);

            return ytems.Result[randomId];
        }

        public async Task<ShowcaseProduct> GetRandomShowcaseProductFromRegion(string region)
        {
            var result = dbContext.showcaseProducts.AsAsyncEnumerable().Where(x => x.webbImage_Item1 != null && x.region == region);
            if (result != null)
            {
                var items = result.ToListAsync();
                Random rnd = new Random();
                int randomId = rnd.Next(1, items.Result.Count);

                return items.Result[randomId];
            }
            else
                return null;
        }
    }
}

/*
 
            var result = dbContext.Products.FirstOrDefaultAsync(x => x.name == Name);
            var item = result?.Result.name;
 */