using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using MinSon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Services
{
    public class ZeldaService : IZeldaService
    {
        private readonly IZeldaRepository zeldaRepository;
        public ZeldaService(IZeldaRepository zeldaRepository_)
        {
            zeldaRepository = zeldaRepository_;
        }
        public async Task<ZeldaQuote> GetRandomQuouteAsync()
        {
            var result = await generateQuote();
            return result;
        }

       private async Task<ZeldaQuote> generateQuote()
        {

            //string result =quotes[randomId];
            
            return await zeldaRepository.generateQuote(); 
        }
    }
}

