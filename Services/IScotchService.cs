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
        //Get Product
        Task<List<ShowcaseProduct>> GetAllShowcaseProductsByNameAsync(CommandContext ctx, string Name);
        Task<ShowcaseProduct> GetShowcaseProductByNameAsync(CommandContext ctx, string Name);

        // Create Embeds
        DiscordEmbedBuilder createGeneralInfoEmbed(ShowcaseProduct product);
        DiscordEmbedBuilder createProductEmbedsList(ShowcaseProduct product);

        //Get Random Product
        Task<ShowcaseProduct> GetRandomshowcaseProduct();
        Task<ShowcaseProduct> GetRandomShowcaseProductFromRegion(string region);
    }
}
