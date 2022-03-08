using MinSon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Services
{
   public interface IZeldaRepository
    { 
        Task<ZeldaQuote> generateQuote();
    }
}
