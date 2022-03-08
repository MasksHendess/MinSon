using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using MinSon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Services
{
    public class ScotchService : IScotchService
    {
        private readonly IScotchRepository scotchRepository;
        public ScotchService(IScotchRepository IscotchRepository)
        {
            scotchRepository = IscotchRepository;
        }
        public async Task<List<ShowcaseProduct>> GetAllShowcaseProductsByNameAsync(CommandContext ctx, string Name)
        {
            var result = await scotchRepository.GetAllShowcaseProductsByNameAsync(Name);
            if (result != null)
                return result;
            else
                await ctx.Channel.SendMessageAsync("Service failed to find " + Name).ConfigureAwait(false);

            return null;
        }
        public async Task<ShowcaseProduct> GetShowcaseProductByNameAsync(CommandContext ctx, string Name)
        {
            var result = await scotchRepository.GetShowcaseProductByNameAsync(Name);
            if (result != null)
                return result;
            else
                await ctx.Channel.SendMessageAsync("Service failed to find " + Name).ConfigureAwait(false);

            return null;
        }


        private Product cast(Product product)
        {
            Product result = product;
            return result;
        }

        public DiscordEmbedBuilder createGeneralInfoEmbed(ShowcaseProduct product)
        {
            var embedGeneral = new DiscordEmbedBuilder();
            //general Info
            if (product.brandName != null && product.owners != null && product.webbUrl != null)
            {

                embedGeneral.Title = product.brandName;
                embedGeneral.Description = "\nGrundat: " + product.birthYear +
                          "\nÄgare:" + product.owners +
                          "\nRegion:" + product.region +
                           "\nWebb:" + product.webbUrl;

            }
            else
            {
                embedGeneral.Title = "Failed to find";
            }
            return embedGeneral;
        }
        public  DiscordEmbedBuilder createProductEmbedsList(ShowcaseProduct product)
        {
            // The Big Spagett
            var embed = new DiscordEmbedBuilder();
            if (product.showcase_Item1_Name != null && product.showcase_Item1_Text != null)
            {
                embed.Title = product.showcase_Item1_Name;
                    embed.Description = product.showcase_Item1_Text;
                    embed.ImageUrl = product.webbImage_Item1;

            }

               // await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
            
            else
            {
                embed.Title = "Failed to Find"; // await ctx.Channel.SendMessageAsync("Failed to retieve information about " + Name).ConfigureAwait(false);
            }
            return embed;
        }
        public Task<ShowcaseProduct> GetRandomshowcaseProduct()
        {
            return scotchRepository.GetRandomshowcaseProduct();
        }


        public Task<ShowcaseProduct> GetRandomShowcaseProductFromRegion(string region)
        {
          return  scotchRepository.GetRandomShowcaseProductFromRegion(region);
        }
    }
}

