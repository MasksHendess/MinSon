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
        public async Task<Product> GetProductAsync(string Name)
        {
            var result = dbContext.Products.Where(x => x.name == Name).FirstOrDefault();
            if (result != null)
                return result;
            else
                return null;
        }

        public async Task<PartialProduct> GetProductByShowcaseNameAsync(string showcaseName, int nr)
        {
            PartialProduct product = new PartialProduct();
            // check first showcase item
            if (nr == 1)
            {
                var result = dbContext.Products.Where(x => x.name == showcaseName).FirstOrDefault();
                if (result != null)
                {
                    product.showcase_Item1_Name = result.showcase_Item1_Name;
                    product.showcase_Item1_Text = result.showcase_Item1_Text;
                    product.webbImage_Item1 = result.webbImage_Item1;

                }
            }
            else if (nr == 2)
            {
                //check second shocase item
                var result = dbContext.Products.Where(x => x.name == showcaseName).FirstOrDefault();
                if (result != null)
                {
                    product.showcase_Item1_Name = result.showcase_Item2_Name;
                    product.showcase_Item1_Text = result.showcase_Item2_Text;
                    product.webbImage_Item1 = result.webbImage_Item2;

                }
            }
            return product;
        }

        public async Task<Product> GetRandomProduct()
        {
            var items = dbContext.Products.Where(x => x.webbImage_Item1 != null).ToList();
            Random rnd = new Random();
            int randomId = rnd.Next(1, items.Count - 1);

            return dbContext.Products.Where(x => x.Id == randomId).FirstOrDefault();
        }

        public async Task<Product> GetRandomIslayProduct(string region)
        {
            var items = dbContext.Products.Where(x => x.webbImage_Item1 != null && x.region == region).ToList();
            if (items != null)
            {
                Random rnd = new Random();
                int randomId = rnd.Next(items.First().Id, items.Last().Id);

                return dbContext.Products.Where(x => x.Id == randomId).FirstOrDefault();
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