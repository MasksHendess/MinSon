using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using MinSon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Services
{
    public interface IScotchService
    {
        Task<Product> GetProductAsync(CommandContext ctx, string Name);
        Task<PartialProduct> GetProductByShowcaseNameAsync(string showcaseName, int nr);
        List<DiscordEmbed> createProductEmbedsList(Product product);

        Task<Product> GetRandomProduct();

        Task<Product> GetRandomIslayProduct(string region);
    }
}
