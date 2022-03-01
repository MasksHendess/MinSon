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
        public async Task<Product> GetProductAsync(CommandContext ctx, string Name)
        {
            var result = await scotchRepository.GetProductAsync(Name);
            if (result != null)
                return cast(result);
            else
                await ctx.Channel.SendMessageAsync("Service failed to find " + Name).ConfigureAwait(false);

            return null;
        }

        private Product cast(Product product)
        {
            Product result = product;
            return result;
        }

        public List<DiscordEmbed> createProductEmbedsList(Product product)
        {
            // The Big Spagett
            var result = new List<DiscordEmbed>();

            var embedGeneral = new DiscordEmbedBuilder();
            if (product.name != null && product.owners != null && product.webbUrl != null)
            {

                embedGeneral.Title = product.name;
                embedGeneral.Description = "\nGrundat: " + product.birthYear +
                          "\nÄgare:" + product.owners +
                          "\nRegion:" + product.region +
                           "\nWebb:" + product.webbUrl;

            }
            else
            {
                embedGeneral.Title = "Failed to find";
            }

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

            var embed2 = new DiscordEmbedBuilder();
            if (product.showcase_Item2_Name != null && product.showcase_Item2_Text != null)
            {
                embed2.Title = product.showcase_Item2_Name;
                   embed2.Description = product.showcase_Item2_Text;
                   embed2.ImageUrl = product.webbImage_Item2;



             //   await ctx.Channel.SendMessageAsync(embed2).ConfigureAwait(false);
            }
            else
            {
                embed2.Title = "Failed to Find"; // await ctx.Channel.SendMessageAsync("Failed to retieve information about " + Name).ConfigureAwait(false);
            }

            //Add embeds to list
            result.Add(embedGeneral);
            result.Add(embed);
            result.Add(embed2);
            return result;
        }
        public Task<PartialProduct> GetProductByShowcaseNameAsync(string showcaseName, int nr)
        {
            return scotchRepository.GetProductByShowcaseNameAsync(showcaseName, nr);
        }
        public Task<Product> GetRandomProduct()
        {
            return scotchRepository.GetRandomProduct();
        }

       public Task<Product> GetRandomIslayProduct(string region)
        {
          return  scotchRepository.GetRandomIslayProduct(region);
        }
    }
}

