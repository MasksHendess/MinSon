using DSharpPlus.Entities;
using MinSon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Services
{
    public class ZeldaRepository : IZeldaRepository
    {
        private readonly MinSonDBContext dbContext;
        public ZeldaRepository(MinSonDBContext DBContext)
        {
            dbContext = DBContext;
        }

        public async Task<ZeldaQuote> generateQuote()
        {
            Random rnd = new Random();
            var quotes = dbContext.zeldaQuotes.ToList();

            int randomId = rnd.Next(1, quotes.Count);
            var result = dbContext.zeldaQuotes.AsAsyncEnumerable().Where(x => x.id == randomId).FirstOrDefaultAsync();
            return result.Result;
        }

       

    }
}

/*
 
            var result = dbContext.Products.FirstOrDefaultAsync(x => x.name == Name);
            var item = result?.Result.name;
 */