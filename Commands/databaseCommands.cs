
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using MinSon.Domain.Entities;
using MinSon.Services;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Commands
{
    public class databaseCommands : BaseCommandModule
    {
        private readonly IScotchService scotchService;
        private readonly MinSonDBContext dbContext;
      

        public databaseCommands(IScotchService Scotchservice, MinSonDBContext DBContext)
    {
        dbContext = DBContext;
        
            scotchService = Scotchservice;
        }
        [Command("dbtest")]
        public async Task dbtest(CommandContext ctx, string Name)
        {
            //await context.cards.AddAsync(new Card { name = Name}).ConfigureAwait(false);
            //await context.SaveChangesAsync().ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync("Geralf wont let me add that to the database").ConfigureAwait(false);
        }

        [Command("dbget")]
        [Description("?dbget string Name Get info about these whiskies: \nClynelish\nDalmore \nGlenmorangie\nHighland Park \nOld Pulteney \nTomatin" +
            "\n\nGlendronach\nFettercairn\nGlencadam\nRoyal Lochnagar" +
            "\n\nAberlour\nBenRiach\nCardhu\nDalwhinnie\nGlenfarclas\nGlenfiddich\nMacallan" +
            "\n\nArdbeg\nBowmore\nLaphroaig\nLagavulin\nBunnahabhain\nBruichladdich")]
        public async Task dbget(CommandContext ctx, [RemainingText] string Name)
        {
            Product product = await scotchService.GetProductAsync(ctx, Name);
            postProductEmbedsinChannel(ctx, product);
        }

        [Command("db")]// ? ? ? Piplup Cheer you up
        public async Task dbgetshowcase(CommandContext ctx, int nr, [RemainingText] string showcaseName)
        {
            PartialProduct product = await scotchService.GetProductByShowcaseNameAsync(showcaseName, nr);
            var embed = new DiscordEmbedBuilder
            {
                Title = product.showcase_Item1_Name,
                Description = product.showcase_Item1_Text,
                ImageUrl = product.webbImage_Item1
            };
            await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
        }

        [Command("dbrng")]
        [Description("get one random item from db")]
        public async Task dbrandom(CommandContext ctx)
        {
           Product product = await scotchService.GetRandomProduct();
            postProductEmbedsinChannel(ctx, product);
        }
        [Command("dbrngregion")]
        [Description("get one random item from specific region\nex: ?dbrngregion regionname ")]
        public async Task dbrandomislay(CommandContext ctx, [RemainingText] string region)
        {
            Product product = await scotchService.GetRandomIslayProduct(region);
            postProductEmbedsinChannel(ctx, product);
        }


        private async void postProductEmbedsinChannel(CommandContext ctx , Product product)
        {
            if (product != null)
            {
                var embeds = scotchService.createProductEmbedsList(product);
                foreach (DiscordEmbed embed in embeds)
                {
                    await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
                }
            }
        }

    }
}