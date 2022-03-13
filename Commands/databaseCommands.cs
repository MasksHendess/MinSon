
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using MinSon.Domain.Entities;
using MinSon.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MinSon.Commands
{
    public class databaseCommands : BaseCommandModule
    {
        #region props&Ctor
        private readonly IScotchService scotchService; 
        private readonly IDiscordService discordService;
        private readonly IZeldaService zeldaService;
      
        public databaseCommands(IScotchService Scotchservice, IDiscordService discordService_, IZeldaService zeldaService_)
        {
            zeldaService = zeldaService_;
            discordService =discordService_;
            scotchService = Scotchservice;
        }
        #endregion
        [Command("save")]
        [Description("save users in database")]
        public async Task hatarminbot(CommandContext ctx)
        {
            if (ctx.Guild.Owner != ctx.User)
            {
                await ctx.Channel.SendMessageAsync("You are not allowed to execute this command.").ConfigureAwait(false);
            }
            else
            { 
            var members = ctx.Channel.Users.ToList();
            discordService.saveMembers(members);
            }
        }
        #region scotch

        [Command("scotchgetall")]
        [Description("?scotchinfo string Name Get info about these whiskies: \nClynelish\nDalmore \nGlenmorangie\nHighland Park \nOld Pulteney \nTomatin" +
            "\n\nGlendronach\nFettercairn\nGlencadam\nRoyal Lochnagar" +
            "\n\nAberlour\nBenRiach\nCardhu\nDalwhinnie\nGlenfarclas\nGlenfiddich\nMacallan" +
            "\n\nArdbeg\nBowmore\nLaphroaig\nLagavulin\nBunnahabhain\nBruichladdich")]
        public async Task GetAllShowcaseProductsByNameAsync(CommandContext ctx, [RemainingText] string Name)
        {
            var products = await scotchService.GetAllShowcaseProductsByNameAsync(ctx, Name);

            await ctx.Channel.SendMessageAsync(
                createGeneralInfoEmbed(products.FirstOrDefault())
                ).ConfigureAwait(false);
            foreach (var item in products)
            {
                postProductEmbedsinChannel(ctx, item);
            }
        }
        [Command("scotchget")]
        [Description("?scotch productname \nGet info about one product")]
        public async Task dbgetShowcaseProduct(CommandContext ctx, [RemainingText] string Name)
        {
            var product = await scotchService.GetShowcaseProductByNameAsync(ctx, Name);
            await ctx.Channel.SendMessageAsync(
                createGeneralInfoEmbed(product)
                ).ConfigureAwait(false);
            postProductEmbedsinChannel(ctx, product);
        }

        [Command("randomscotch")]
        [Description("get one random scotch")]
        public async Task dbrandomshowcaseProduct(CommandContext ctx)
        {
            // Product product = await scotchService.GetRandomProduct();
            var product = await scotchService.GetRandomshowcaseProduct();
            postProductEmbedsinChannel(ctx, product);
        }
        [Command("randomscotchregion")]
        [Description("get one random scotch from specific region\nex: ?randomscotchregion regionname ")]
        public async Task dbrandomProductFromRegion(CommandContext ctx, [RemainingText] string region)
        {
            ShowcaseProduct product = await scotchService.GetRandomShowcaseProductFromRegion(region);
            postProductEmbedsinChannel(ctx, product);
        }
        #endregion
        #region zelda
        [Command("zelda")]
        public async Task zelda(CommandContext ctx)
        {
            var result = await zeldaService.GetRandomQuouteAsync();
            var embed = new DiscordEmbedBuilder
            {
                Description = result.quote +"\n" + result.character
            };
            await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
        }
        #endregion
        #region HelperFunctions
        private DiscordEmbedBuilder createGeneralInfoEmbed( ShowcaseProduct product)
        {
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            if(product!=null)
            {
                 embed = scotchService.createGeneralInfoEmbed(product);
                //await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
            }
            else
            {
                embed.Title = "Failed to Find";
            }

            return embed;
        }
        private async void postProductEmbedsinChannel(CommandContext ctx , ShowcaseProduct product)
        {
            if (product != null)
            {
                var embed = scotchService.createProductEmbedsList(product);
                await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
            }
        }
        #endregion
    }
}